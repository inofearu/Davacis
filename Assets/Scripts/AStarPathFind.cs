using UnityEngine;
//* This line should always be present at the top of scripts which use pathfinding else 'Path' class will not be known 
using Pathfinding;
public class AStarPathFind : MonoBehaviour {
    public Transform targetPosition;

    private Seeker seeker;
    private CharacterController controller;

    public Path path;

    public float speed = 2;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;

    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();

        // start new path to targetPosition, call OnPathComplete on calculation
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public void OnPathComplete (Path p) {
        if (!p.error) {
            path = p;
            // reset count and move to first waypoint 
            currentWaypoint = 0;
        }
    }

    public void Update () {
        if (path == null) {
            // no path to return
            return;
        }

        // check using loop if we are close enough to the current waypoint to switch to the next.
        reachedEndOfPath = false;
        // dist to the next waypoint in the path
        float distanceToWaypoint;
        while (true) {
            //todo check the squared distance instead to get rid of a square root calculation for performance
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) {
                // check if end of path
                if (currentWaypoint + 1 < path.vectorPath.Count) {
                    currentWaypoint++;
                } else {
                    // status var to show end of path
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }

        // scale from 1-0 when approaching end of path to slow smoothly
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;

        // normalized direction to next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // turn dir and speed into velocity
        Vector3 velocity = dir * speed * speedFactor;

        //* SimpleMove takes a velocity in meters/second, do not multiply by Time.deltaTime
        controller.SimpleMove(velocity);
    }
}