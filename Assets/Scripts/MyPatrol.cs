using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPatrol : MonoBehaviour
{
    public Waypoint waypoint;
    public float patrolSpeed = 1f; // The walking speed between Waypoints
    public bool loop = true;        // Do you want to keep repeating the Waypoints and if enemies are seen then loop = false;
    public float dampingLook = 6.0f; // How slowly to turn
    public float pauseDuration = 5f; // How long to pause at a Waypoint

    private void Update()
    {
        patrol();
    }
    void patrol()
    {
        {
            // if loopis true and waypoint is not equal to null then move
            if (loop && waypoint != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, patrolSpeed * Time.deltaTime);
            }
        }
    }
}
