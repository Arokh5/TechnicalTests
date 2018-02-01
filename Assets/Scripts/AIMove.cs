using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIMove : MonoBehaviour
{

    private AISpawner m_AIManager;

    // Three different cuties AI behaviours
    const string MARCH_STATE = "march";
    const string PATROL_STATE = "patrol";
    const string CHASING_sTATE = "chase";

    /* patrol variables*/
    string currentState = MARCH_STATE;
    public List<Transform> patrolWaypoints = new List<Transform>();
    int currentPatrolWP = 0;
    float accuracyWP = 5.0f;
    float rotationSpeed = 0.3f;
    float speed = 0.3f;

    /* march variables*/
    private bool m_hasTarget = false;

    private Vector3 m_waypoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);

    private Animator m_animator;
    private float m_speed;

    private Vector3 m_internalWaypoint = new Vector3(0f, 0f, 0f);

    // Use this for initialization
    void Start()
    {
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        m_animator = GetComponent<Animator>();

        patrolWaypoints = m_AIManager.GetObjectsPatrolWaypoints();
        // SetUpNPC();
    }

    void SetUpNPC()
    {
        // float m_scale = Random.Range(0f, 4f);
        // transform.localScale += new Vector3(m_scale, m_scale, m_scale);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentState == MARCH_STATE)
        {
            if (!m_hasTarget)
            {
                m_hasTarget = CanFindTarget();
            }
            else
            {
                RotateNPC(m_waypoint, m_speed);
                transform.position = Vector3.MoveTowards(transform.position, m_waypoint, m_speed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, m_waypoint) < accuracyWP)
            {
                currentState = PATROL_STATE;
            }
        }
        else if (currentState == PATROL_STATE)
        {
            Patrol();
        }

    }

    bool CanFindTarget()
    {
        m_waypoint = m_AIManager.RandomWaypoint();
        if (m_lastWaypoint == m_waypoint)
        {
            return false;
        }
        else
        {
            m_lastWaypoint = m_waypoint;
            m_speed = 1f;
            m_animator.speed = m_speed;
            return true;
        }
    }

    void RotateNPC(Vector3 waypoint, float currentSpeed)
    {
        float turnSpeed = currentSpeed;

        Vector3 lookAt = waypoint - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);
    }

    public float GetIASpeed()
    {
        return m_speed;
    }

    public Vector3 GetMWayPoint()
    {
        return m_waypoint;
    }

    /*Method to patrol around specific waypoints*/
    void Patrol()
    {
        //case for weak units
        if (currentState == "patrol" && patrolWaypoints.Count > 0)
        {
            if (Vector3.Distance(patrolWaypoints[currentPatrolWP].transform.position, transform.position) < accuracyWP)
            {
                currentPatrolWP++;
                if (currentPatrolWP >= patrolWaypoints.Count)
                {
                    currentPatrolWP = 0;
                }
            }
            Vector3 direction = patrolWaypoints[currentPatrolWP].transform.position - transform.position;
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            this.transform.Translate(0, 0, Time.deltaTime * speed);
        }
    }
}
