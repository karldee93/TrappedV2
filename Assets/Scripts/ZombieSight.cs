using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSight : MonoBehaviour
{
    public GameObject player; // create a public variable on the attached game object hierarchy
    public RaycastHit hitObject;
    public float distence;
    public float angle = 120f;
    public float radius = 10f;
    bool playerInView;
    float timer = 8f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // creates a public vector which is slightly different to a function it has a getter but no setter
    public Vector3 fromVector
    {
        get
        {
            // this will find the left angle by taking the negative of the angle (120 will be -120) / 2 so becomes -60 which is the left most position on the arc
            // then it will get the euler angle for that which gives the degrees
            float leftAngle = -angle / 2;
            leftAngle += transform.eulerAngles.y;
            // this will return a vector3 with the y pos of 0 and the x is calced using sine and the z using Cosine and using degrees to radions because maths functions in unity deal with radions over degrees
            return new Vector3(Mathf.Sin(leftAngle * Mathf.Deg2Rad), 0, Mathf.Cos(leftAngle * Mathf.Deg2Rad));
        }
    }
    void Update()
    {
        Debug.Log("Update " + playerInView);
        SeePlayer();
        if (playerInView)
        {
            Debug.Log("see player");
            Physics.Raycast(transform.position, player.transform.position - transform.position, out hitObject, 20);
            if (hitObject.collider.tag != "Player")
            {
                Debug.Log("Timer had started" + timer);
                timer -= 1f * Time.deltaTime;
                GetComponent<TargetPlayer>().trackPlayer();
                if (timer <= 0f)
                {
                    timer = 0f;
                    Debug.Log("StopChasing");
                    Debug.Log(playerInView);
                    GetComponent<ZombiePatrol>().StopChasing();
                    playerInView = false;
                    timer = 8f;
                }
            }
        }
    }

    void SeePlayer()
    {
        Vector3 directionVector = (transform.position - player.transform.position).normalized;
        Vector3 direction = transform.TransformDirection(Vector3.forward); // sets the direction of the variable to vector3 forward to travel along the z axis
        // if raycast is shot on the transform position  in the direction of travel (sets hit object as peraminter) distance of 20 units
        // draws line from current position (forwards) to the value of 20 units if something is hit in that period then period will be output as a hit object else object will be NULL
        float dotProduct = Vector3.Dot(directionVector, transform.forward);
        distence = Vector3.Distance(transform.position, player.transform.position);
        if (distence <= 10 && dotProduct < -0.5)
        {
            Physics.Raycast(transform.position, player.transform.position - transform.position, out hitObject, 20);
            Debug.Log(hitObject.collider.tag);
            if (hitObject.collider.tag == "Player")
            {
                Debug.DrawLine(transform.position, hitObject.point, Color.magenta); // draws line to hit object
                Debug.Log("test: " + hitObject.distance.ToString()); // updates console with distance to hit object
                playerInView = true;
                Debug.Log(": IN FIELD OF VIEW. Distence = " + distence);
                Debug.Log(playerInView);
                GetComponent<TargetPlayer>().trackPlayer();
            }
        }
    }
}
