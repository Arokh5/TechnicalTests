using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualObjectAI : MonoBehaviour
{
    [Header("Global Stats")]
    [SerializeField]
    private string m_AISubTeamID;

    List<GameObject> spawnedElements = new List<GameObject>();

    public string getAISubTeamID()
    {
        return m_AISubTeamID;
    }

    public void setAISubTeamID(string subTeamID)
    {
        m_AISubTeamID = subTeamID;
    }

    void Update()
    {
        spawnedElements = GetComponentInParent<SpawnerAI>().getSpawnedElements();
        InvokeRepeating("Clustering", 1f, 1500f);
     
    }


    public void Clustering()
    {
        int num = 0;
        GameObject element;
        for (int i = 0; i < spawnedElements.Count; i++)
        {
            element = spawnedElements[i];
            if (this.m_AISubTeamID.Equals(element.GetComponentInChildren<IndividualObjectAI>().getAISubTeamID()))
            {
                if (this.transform != element.transform && Vector3.Distance(this.transform.position,element.transform.position)>4 && Vector3.Distance(this.transform.position, element.transform.position) < 7)
                {
                    transform.position = Vector3.MoveTowards(transform.position, element.transform.position, 0.01f);
                }
            }
        }
    }
}
