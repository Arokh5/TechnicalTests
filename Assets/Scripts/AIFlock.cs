using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlock : MonoBehaviour {

    //Vector3 groupHeadingDirection;
    //Vector3 groupAveragePosition;
    AIMove m_AIMove;
    private AISpawner m_AIManager;
    float rotationSpeed = 0.5f;
    float distanceBetweenNeighbours = 2.5f;
    float speed;

	// Use this for initialization
	void Start () {

        m_AIMove = transform.GetComponentInParent<AIMove>();
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        speed = m_AIMove.GetIASpeed();
    }

    // Update is called once per frame
    void Update () {
		
        if(Random.Range(0,5) < 1)
        {
            ApplyFlock();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
	}

    void ApplyFlock()
    {
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        float dist;
        AIObject[] groupIndividuals;
        int groupIndividualsSize = 0;

        groupIndividuals = m_AIManager.AIObjects;
        Vector3 goalPos = m_AIMove.GetMWayPoint();

        foreach (AIObject individual in groupIndividuals)
        {
            if(individual.GetGameObject() != this.gameObject)
            {
                dist = Vector3.Distance(individual.GetGameObject().transform.position, this.transform.position);
                if(dist <= distanceBetweenNeighbours)
                {
                    vcentre += individual.GetGameObject().transform.position;
                    groupIndividualsSize++;

                    if(dist < 2.0f)
                    {
                        vavoid = vavoid + (this.transform.position - individual.GetGameObject().transform.position);
                    }

                    //AIFlock anotherFlock = individual.objectPrefab.GetComponentInParent<AIFlock>();
                    gSpeed += gSpeed + 0.3f; //currently groupSpeed increment is hardcoded
                }
            }
        }

        if (groupIndividualsSize > 0)
        {
            vcentre = vcentre / groupIndividualsSize + (goalPos - this.transform.position);
            speed = gSpeed / groupIndividualsSize;

            Vector3 direction = ((vcentre + vavoid) - transform.position);
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
    
        }
    }
}
