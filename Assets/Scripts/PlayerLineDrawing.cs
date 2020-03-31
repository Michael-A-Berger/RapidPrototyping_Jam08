using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLineDrawing : MonoBehaviour
{

    public float showDistance = 6.0f;
    public GameObject linePrefab;
    public Transform lineTextsContainer;
    public GameObject lineTextPrefab;

    public Material showMaterial;
    public Material warningMaterial;
    public float dangerDistance = 4.0f;
    public float escapeTime = 3.0f;
    public Slider timeSlider;

    private List<GameObject> lines = new List<GameObject>();
    private List<GameObject> lineTexts = new List<GameObject>();
    private List<Transform> targets = new List<Transform>();
    private bool isTouching;
    private float touchingTime;
    private AudioManager audioMng;

    void Start()
    {
        isTouching = false;
        audioMng = FindObjectOfType<AudioManager>();
        if (audioMng == null)
            Debug.LogError("\tObject with [ AudioManager ] script not found in scene!");
    }

    void Update()
    {
        DrawDistanceLines();
        if (isTouching)
        {
            if (!timeSlider.gameObject.activeSelf)
            {
                timeSlider.gameObject.SetActive(true);
            }
            touchingTime += Time.deltaTime;
            if(touchingTime >= escapeTime)
            {
                //Lose
                //Debug.Log("Lose");
                if (!GameManager.instance.isEnded)
                {
                    GameManager.instance.EndGame(false);
                }
            }
        }
        else
        {
            if (timeSlider.gameObject.activeSelf)
            {
                timeSlider.gameObject.SetActive(false);
            }
            touchingTime = 0;
        }
        timeSlider.value = Mathf.Clamp(touchingTime, 0, escapeTime) / escapeTime;
    }

    private void DrawDistanceLines()
    {
        targets.Clear();
        isTouching = false;

        Collider[] cos = Physics.OverlapSphere(transform.position, showDistance);
        for (int i = 0; i < cos.Length; i++)
        {
            if (cos[i].tag.Equals("NPC") && Vector3.Distance(transform.position, cos[i].transform.position) <= showDistance)
            {
                targets.Add(cos[i].transform);
            }
        }

        for (int i = 0; i < lines.Count; i++)
        {
            lines[i].SetActive(false);
            lineTexts[i].SetActive(false);
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (i >= lines.Count)
            {
                GameObject line = Instantiate(linePrefab);
                GameObject lineText = Instantiate(lineTextPrefab, lineTextsContainer);
                lines.Add(line);
                lineTexts.Add(lineText);
            }
            lines[i].SetActive(true);
            lineTexts[i].SetActive(true);
            LineRenderer lr = lines[i].GetComponent<LineRenderer>();
            Text lt = lineTexts[i].GetComponent<Text>();
            if (lr != null)
            {
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, targets[i].transform.position);
                if (Vector3.Distance(transform.position, targets[i].position) <= dangerDistance)
                {
                    lr.material = warningMaterial;
                    isTouching = true;
                    audioMng.PlaySong(1);
                }
                else
                {
                    lr.material = showMaterial;
                    audioMng.PlaySong(0);
                }

            }
            if (lt != null)
            {
                Vector3 pos = (transform.position + targets[i].position) / 2;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
                lineTexts[i].transform.position = screenPos;
                lt.text = Vector3.Distance(transform.position, targets[i].position).ToString("f2");
            }
        }

    }
}
