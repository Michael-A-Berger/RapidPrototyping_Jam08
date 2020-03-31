using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBehavior : MonoBehaviour
{
    /*
     * NPCs also follow social distancing rules
     * To change this distance, modify the below variable in the Unity Editor for NPC prefab
     *      Nav Mesh Agent -> Obstacle Avoidance -> Radius (currenty set to 3 units)
     */


    public int agentID;
    public int maxAgentsAtDestination = 3;
    public float stopTime = 0.0f;
    public GameObject[] destinations;
    //public float speed;

    static private int numAgents;
    static private int[] destinationTrack;
    //static private bool setDestinationTrack = false;

    
    private NavMeshAgent navAgent;
    private int index = -1;
    public bool hasStopped = true;
    public float slowedDownSince = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        numAgents++;
        agentID = numAgents;

        navAgent = GetComponent<NavMeshAgent>();
        //destinations = GameObject.FindGameObjectsWithTag("NPCTarget");
        //if(!setDestinationTrack)
        //{
        //    destinationTrack = new int[destinations.Length];
        //    setDestinationTrack = true;
        //}
        PickRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStopped)
        {
            if (Vector3.Distance(navAgent.transform.position, destinations[index].transform.position) < 1.0f)
            {
                hasStopped = true;
                Invoke("PickRandomDestination", stopTime);
            }
            else if(navAgent.velocity.magnitude < 0.5f)
            {
                if(slowedDownSince >= stopTime)
                {
                    slowedDownSince = 0.0f;
                    hasStopped = true;
                    Invoke("PickRandomDestination", 0.5f);
                }
                else
                {
                    slowedDownSince += Time.deltaTime;
                }
            }
        }
        //speed = navAgent.velocity.magnitude;
    }

    void PickRandomDestination()
    {
        //Debug.Log("Agent ID: " + agentID + " Picking Random Destination");
        int newIndex = Random.Range(0, destinations.Length);

        if(newIndex != index)
        {
            index = newIndex;
            navAgent.destination = destinations[index].transform.position;
            hasStopped = false;
        }
        else
        {
            PickRandomDestination();
        }

        //if (newIndex != index && destinationTrack[newIndex] < maxAgentsAtDestination)
        //{
        //    if(index > -1 && destinationTrack[index] > 0)
        //        destinationTrack[index]--;
        //    index = newIndex;
        //    navAgent.destination = destinations[index].transform.position;
        //    destinationTrack[index]++;
        //    hasStopped = false;
        //}
        //else
        //{
        //    PickRandomDestination();
        //}

    }
}
