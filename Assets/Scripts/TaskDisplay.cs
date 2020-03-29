using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TaskDisplay : MonoBehaviour
{
    // Public Properties
    [Header("General Variables")]
    public Text goalTextObject;
    public PlayerTarget arrowTarget;
    public KeyCode interactKey;

    [Header("Task Queue")]
    [Space(10)]
    public List<TaskObjective> taskQueue;

    // Start()
    void Start()
    {
        // IF the goal text object is not defined, complain about it
        if (goalTextObject == null)
            Debug.LogError("\t[ goalTextObject ] is not defined!");

        // IF the arrow target is not defined, complain about it
        if (arrowTarget == null)
            Debug.LogError("\t[ arrowTarget ] is not defined!");

        // IF the game object does not have a collider, complain about it
        if (GetComponent<Collider>() == null)
            Debug.LogError("\tGame Object with [ TaskDisplay ] script MUST have 3D collider!");

        // IF the game object does not have a rigidbody, complain about it
        if (GetComponent<Rigidbody>() == null)
            Debug.LogError("\tGame Object with [ TaskDisplay ] script MUST have 3D rigidbody!");

        // IF the task queue is empty, throw a warning message
        if (taskQueue == null)
        {
            Debug.LogWarning("\t[ taskQueue ] is empty!");
        }
        // ELSE... (the task queue is not empty)
        else
        {
            // Updating the current task + destination
            UpdateTask();
        }
    }

    // UpdateTask()
    private void UpdateTask()
    {
        if (taskQueue.Count > 0)
        {
            goalTextObject.text = "<b>" + taskQueue[0].description + "</b>";
            arrowTarget.target = taskQueue[0].gameObject.transform;
        }
        else
        {
            goalTextObject.text = "<b>You win!</b>";
            arrowTarget.target = gameObject.transform;
        }
    }

    // ProcessTaskObjectiveCollision()
    private void ProcessTaskObjectiveCollision(TaskObjective obj)
    {
        // IF the task objective does NOT require interaction |OR| The interaction key is down...
        if (!obj.requresInteraction || Input.GetKey(interactKey))
        {
            // Trying to remove the task objective from the task queue
            int index = taskQueue.IndexOf(obj);
            if (index > -1)
                taskQueue.Remove(obj);
            else
                Debug.LogWarning("\t[ TaskObjective ] script attached to [ " + obj.gameObject.name + " ] was not found in [ taskQueue ]!");
            UpdateTask();
        }
    }

    // OnTriggerStay()
    private void OnTriggerStay(Collider other)
    {
        TaskObjective obj = other.gameObject.GetComponent<TaskObjective>();
        if (obj != null)
            ProcessTaskObjectiveCollision(obj);
    }

    // OnCollisionStay()
    private void OnCollisionStay(Collision other)
    {
        TaskObjective obj = other.gameObject.GetComponent<TaskObjective>();
        if (obj != null)
            ProcessTaskObjectiveCollision(obj);
    }
}
