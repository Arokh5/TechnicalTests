using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointInfo : MonoBehaviour {

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        Transform[] wpList = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++) {
            if (wpList[i].tag == "waypoint") {
                waypoints.Add(wpList[i]);
            }
        }
        return waypoints;
    }

    public List<Transform> GetPatrolWaypoints() {
        List<Transform> patrolWaypoints = new List<Transform>();
        Transform[] patrolWpList = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < patrolWpList.Length; i++) {
            if (patrolWpList[i].tag == "patrolwaypoint") {
                patrolWaypoints.Add(patrolWpList[i]);
            }
        }
        return patrolWaypoints;
    }
}
