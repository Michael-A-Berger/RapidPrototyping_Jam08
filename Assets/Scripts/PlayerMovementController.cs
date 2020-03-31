using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float frictionLerpValue;


    private Vector3 movementVelocity;
    private Vector3 movementDir;
    private Rigidbody rb;
    private AudioManager audioMng;

    void Start()
    {
        movementVelocity = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        audioMng = FindObjectOfType<AudioManager>();
        if (audioMng == null)
            Debug.LogError("\tObject with [ AudioManager ] script not found in scene!");
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        movementDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementDir += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementDir += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDir += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDir += new Vector3(1, 0, 0);
        }
        movementDir = movementDir.normalized;

        if (movementDir.magnitude == 0)
        {
            movementVelocity = Vector3.Lerp(movementVelocity, Vector3.zero, frictionLerpValue);
            if (rb != null)
            {
                rb.velocity = Vector3.Lerp(rb.velocity, movementVelocity, frictionLerpValue);
            }
            audioMng.StopSFX(0);
        }
        else
            audioMng.PlaySFX(0);

        movementVelocity += movementDir * acceleration * Time.fixedDeltaTime;
        movementVelocity = Vector3.ClampMagnitude(movementVelocity, speed);
        if (rb != null)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, movementVelocity, frictionLerpValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("GroceryDoor"))
        {
            if (other.GetComponent<TeleportTrigger>() != null)
            {
                ISceneManager.instance.ChangeScene(other.GetComponent<TeleportTrigger>().targetScene);
            }
        }
    }


    
}
