using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroceryShelf : MonoBehaviour
{
    // Public Properties
    public string popupText = "[ placeholder popup text ]";
    public Text textObject;
    public Color textColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        // IF no text object is defined, throw a fit
        if (textObject == null)
            Debug.LogError("\t[ textObject ] of [ GroceryShelf ] script not defined!");
        // ELSE, set the text object to not show
        else
            textObject.gameObject.SetActive(false);
    }

    // OnTriggerEnter()
    private void OnTriggerEnter(Collider other)
    {
        // IF the other object is the player, show the text object
        if (other.tag == "Player")
        {
            textObject.gameObject.SetActive(true);
            textObject.text = "<b>" + popupText + "</b>";
            textObject.color = textColor;
        }
    }

    // OnTriggerStay()
    private void OnTriggerStay(Collider other)
    {
        // IF the other object is the player, reposition the text object
        if (other.tag == "Player")
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            screenPos.x = Mathf.Round(screenPos.x);
            screenPos.y = Mathf.Round(screenPos.y);
            screenPos.z = Mathf.Round(screenPos.z);
            textObject.transform.position = screenPos;
        }
    }

    // OnTriggerExit()
    private void OnTriggerExit(Collider other)
    {
        // IF the other object is the player, hide the text object
        if (other.tag == "Player")
            textObject.gameObject.SetActive(false);
    }
}
