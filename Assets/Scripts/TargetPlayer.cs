using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{
    public GameObject target;
    private Vector3 directionToTarget; // getting the direction of the target
    private float angleToTarget; // this will get the angle to the target using trigonometry


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        directionToTarget = target.transform.position - transform.position; // works out distence between target and attached game object
        // to calc angle to target this calc uses Atan this gets the x and z directionToTarget axis this gives the adjacent and opposite and then multiply using .Rad2Deg this converts radious to degrees to get num which is a bit more familiar
        angleToTarget = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
    }

    public void trackPlayer()
    {
        // this gets the euler angles of the attached object and calcs a new vector using the angles of x and z but also the angle to target on the y... this will allow the object to rotate to face the target
        // this will set the euler angles of the attached object to the current x & z but will change the y to be consistent with the player allowing it to "track" the player
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angleToTarget, transform.eulerAngles.z);
        GetComponent<ZombiePatrol>().ChasePlayer();
    }

    public void stopTracking()
    {
        // this will set all euler angle as it own so will no longer track the player
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
