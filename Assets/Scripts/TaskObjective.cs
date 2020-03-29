using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskObjective : MonoBehaviour
{
    // Public Properties
    [Header("General Variables")]
    public string description = "[ placeholder ]";
    public bool requresInteraction = false;

    [Header("Derived Variables")]
    [Space(10)]
    public Collider taskCollider;

    // Start()
    void Start()
    {
        // Checking if this game object has a collider
        taskCollider = GetComponent<Collider>();
        if (taskCollider == null)
            Debug.LogError("\tGameObject with [ TaskObjective ] script MUST have collider!");
        
    }
}
