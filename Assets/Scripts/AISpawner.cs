using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

    public List<Transform> waypoints = new List<Transform>();

    public float spawnTimer { get { return m_spawnTimer; } }
    public Vector3 spawnArea { get { return m_spawnArea; } }

    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_spawnTimer;
    [SerializeField]
    private Color m_spawnColor = new Color(1.0f, 0.0f, 0.0f, 0.3f);
    [SerializeField]
    private Vector3 m_spawnArea;

    [Header("AI Group Settings")]
    public AIObject[] AIObjects = new AIObject[3];

    // Use this for initialization
    void Start() {
        GetWaypoints();
        CreateAIGroups();

        for (int i = 0; i < AIObjects.Length; i++)
        {
            StartCoroutine(SpawnNPC(i));
        }
       }

    IEnumerator SpawnNPC(int objectID) {
        while (true)
        {
            if (AIObjects[objectID].enableSpawner && AIObjects[objectID].objectPrefab != null)
            {
                GameObject tempGroup = GameObject.Find(AIObjects[objectID].AIGroupName);
                int a = tempGroup.GetComponentInChildren<Transform>().childCount;
                if (tempGroup.GetComponentInChildren<Transform>().childCount < AIObjects[objectID].maxAI)
                {
                    for (int n = 0; n < AIObjects[objectID].spawnAmount; n++)
                    {
                        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        GameObject tempSpawn;
                        tempSpawn = Instantiate(AIObjects[objectID].objectPrefab, RandomPosition(), randomRotation);
                        tempSpawn.transform.parent = tempGroup.transform;
                        tempSpawn.AddComponent<AIMove>();
                    }
                }
            }

            yield return new WaitForSeconds(AIObjects[objectID].spawnRate);
        }
    }

    public Vector3 RandomPosition() {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
            );
        randomPosition = transform.TransformPoint(randomPosition * 0.5f);
        return randomPosition;
    }

    public Vector3 RandomWaypoint() {
        int randomWP = Random.Range(0, (waypoints.Count - 1));
        Vector3 randomWaypoint = waypoints[randomWP].transform.position;
        return randomWaypoint;
    }

    void CreateAIGroups() {
        for (int i = 0; i < AIObjects.Length; i++)
        {
            GameObject m_AIGroupSpawn;

            m_AIGroupSpawn = new GameObject(AIObjects[i].AIGroupName);
            m_AIGroupSpawn.transform.parent = gameObject.transform;
        }
    }

    void GetWaypoints() {
        Transform[] wpList = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++)
        {
            if (wpList[i].tag == "waypoint")
            {
                waypoints.Add(wpList[i]);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = m_spawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
