using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust this to control the movement speed
    private Transform targetWaypoint;
    private int waypointIndex = 0;

    // Call this method to set the target waypoint
    public void SetTargetWaypoint(Transform waypoint)
    {
        targetWaypoint = waypoint;
    }

    void Update()
    {
        if (targetWaypoint == null)
        {
            // No target waypoint set, do nothing
            return;
        }

        // Calculate the direction to the target waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Move the enemy towards the waypoint
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the enemy has reached the target waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // You can implement logic here to determine the next waypoint
        // For a basic example, let's just switch between two waypoints

        waypointIndex++;

        if (waypointIndex == 1)
        {
            targetWaypoint = GameObject.Find("Waypoint1").transform;
        }
        else if (waypointIndex == 2)
        {
            targetWaypoint = GameObject.Find("Waypoint2").transform;
        }
        // Add more logic if you have more waypoints in your game

        // When you reach the last waypoint, you can destroy the enemy
        if (waypointIndex > 9) 
        {
            Destroy(gameObject);
        }
    }
}
