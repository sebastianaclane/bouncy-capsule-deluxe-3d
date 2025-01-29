using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints; // this is an array
    int currentWaypointIndex = 0;

    [SerializeField] float speed = 1f;

    void Update()
    {
        // before the move towards call, we have to check what way point we need to move towards
        // this is done by checking the distance between the currently active way point and our platform in each frame and if we touch it (close enough) we switch over 
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex++; // changes the waypoint index to be 1 higher
            // we only have 2 waypoints so we need to make sure it changes back to the 1st waypoint if it was at the last (2nd) waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // moving the position of the object frame by frame
        // this transform refers to the transform component on whatever this WaypointFollower script is added to
        // Time.deltaTime helps keep your game frame rate independent, it is used a lot in Unity.
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
