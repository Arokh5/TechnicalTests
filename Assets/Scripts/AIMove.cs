using UnityEngine;
using System.Collections;

public class AIMove : MonoBehaviour {

    private AISpawner m_AIManager;

    private bool m_hasTarget = false;

    private Vector3 m_waypoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);

    private Animator m_animator;
    private float m_speed;

    // Use this for initialization
    void Start() {
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        m_animator = GetComponent<Animator>();

       // SetUpNPC();
    }

    void SetUpNPC() {
       // float m_scale = Random.Range(0f, 4f);
       // transform.localScale += new Vector3(m_scale, m_scale, m_scale);
    }

    // Update is called once per frame
    void Update() {
        if (!m_hasTarget)
        {
            m_hasTarget = CanFindTarget();
        } else
        {
            RotateNPC(m_waypoint, m_speed);
            transform.position = Vector3.MoveTowards(transform.position, m_waypoint, m_speed * Time.deltaTime);
        }

        if (transform.position == m_waypoint)
        {
            m_hasTarget = false;
        }
    }

    bool CanFindTarget() {
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

    void RotateNPC(Vector3 waypoint, float currentSpeed) {
        float turnSpeed = currentSpeed;

        Vector3 lookAt = waypoint - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAt), turnSpeed * Time.deltaTime);
    }
}
