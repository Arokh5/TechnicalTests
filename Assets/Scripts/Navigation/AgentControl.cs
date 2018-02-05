using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State { MARCH, PATROL }

public class AgentControl : MonoBehaviour {

    public Player player;
    public WaypointInfo waypointInfo;
    NavMeshAgent agent;
    private State agentState;

    public List<Transform> waypoints = new List<Transform>();
    public List<Transform> patrolWaypoints = new List<Transform>();

    private int marchPoint = 0;
    private int patrolPoint = 0;
    private Vector3 previousDest = Vector3.zero;

    // Use this for initialization
    void Start() {
        agentState = State.MARCH;

        waypoints = waypointInfo.GetWaypoints();
        patrolWaypoints = waypointInfo.GetPatrolWaypoints();

        agent = this.GetComponent<NavMeshAgent>();
        agent.destination = waypoints[marchPoint].position;
    }

    void Update() {
        if (player.PlayerInSight()) {
            if (previousDest == Vector3.zero)
                previousDest = agent.destination;

            if (patrolWaypoints.Count > 0)
                agent.destination = player.GetPosition();
        } else {
            if (previousDest != Vector3.zero) {
                agent.destination = previousDest;
                previousDest = Vector3.zero;
            }
            if (!agent.pathPending && agent.remainingDistance < 2.0f) {
                if (agentState == State.MARCH) {
                    marchPoint++;
                    agentState = State.PATROL;
                }
                
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint() {
        if (agentState == State.MARCH) {
            if (waypoints.Count == 0)
                return;

            agent.destination = waypoints[marchPoint].position;
        }

        if (agentState == State.PATROL) {
            if (patrolWaypoints.Count == 0)
                return;

            agent.destination = patrolWaypoints[patrolPoint].position;

            patrolPoint = (patrolPoint + 1) % patrolWaypoints.Count;
        }


    }

}
