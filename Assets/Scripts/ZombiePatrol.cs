using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePatrol : MonoBehaviour
{
    public GameObject zombie;
    GameObject currentWaypoint; // great a game object to store current and previous waypoints
    GameObject previousWaypoint;
    GameObject[] allWaypoints; // place all the way points into an array
    GameObject player;
    bool travelling; // once travelling is finished will set to false to look for new waypoint
    public float timer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        zombie.GetComponent<Zombie1AI>().agent = GetComponent<NavMeshAgent>(); // set up nav mesh component to get the nav mesh agent from the object this script is attached to
        allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint"); // this will set the value of allWaypoints to any gameobjects from with the tag "waypoint" 
        currentWaypoint = GetRandomWaypoint(); // this will set current way point to a random waypoint using the created function
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (travelling && zombie.GetComponent<Zombie1AI>().agent.remainingDistance <= 1f) // if travelling is true and remaining distence of nav mesh is <= 1 set travelling to false and get a new destination
        {
            travelling = false;
            SetDestination();
        }
    }

    GameObject GetRandomWaypoint() // this sets the function to a game object cant use void as it is not as it is typed out above must have a return script
    {
        if (allWaypoints.Length == 0)
        {
            return null; // if there are no waypoints in the array return null
        }
        else
        {
            int index = Random.Range(0, allWaypoints.Length); // create a random number between 0 and the length of the array
            return allWaypoints[index]; // this returns the index number to set which waypoint should go to
        }
    }

    void SetDestination()
    {
        previousWaypoint = currentWaypoint; // sets the value of the previous waypoint to be the current one 
        currentWaypoint = GetRandomWaypoint(); // sets the new current waypoint value using the getrandomwaypoint function

        Vector3 targetVector = currentWaypoint.transform.position; // sets the target vector to be the position of the current way point current waypoint is not where it is and the terget vector is the vector3 for it rather than the game objects
        zombie.GetComponent<Zombie1AI>().agent.SetDestination(targetVector);
        travelling = true;
    }

    public void ChasePlayer()
    {
        RaycastHit hitObject;
        previousWaypoint = currentWaypoint; // sets the value of the previous waypoint to be the current one 
        currentWaypoint = GameObject.FindGameObjectWithTag("PlayerWaypoint"); // sets the new current waypoint value using the getrandomwaypoint function
        Vector3 targetVector = currentWaypoint.transform.position; // sets the target vector to be the position of the current way point current waypoint is not where it is and the terget vector is the vector3 for it rather than the game objects
        zombie.GetComponent<Zombie1AI>().agent.SetDestination(targetVector);
        travelling = true;
    }

    public void StopChasing()
    {
        SetDestination();
    }
}
