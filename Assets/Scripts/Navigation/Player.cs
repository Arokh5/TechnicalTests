using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public SpawnerAI spawner;

    public GameObject playerPrefab;
    public GameObject orbitPrefab;

    private bool playerInSight = false;
    private Vector3 playerPos = Vector3.zero;
    private Vector3 playerCenter = Vector3.zero;
    private int playerRadius = 5;
    private float radians;
    private float initialRadians;
    private int range = 20;
    private bool direction = false;
    
    // Update is called once per frame
    void Update() {
        playerPrefab.SetActive(playerInSight);
        orbitPrefab.SetActive(playerInSight);

        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                playerCenter = hit.point;
                playerPrefab.transform.position = playerCenter;
                playerPos = Vector3.zero;
                GetCirclePoint(spawner.GetAveragePosition());
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
            playerInSight = !playerInSight;

        orbitPrefab.transform.position = playerPos;
    }

    void GetCirclePoint(Vector3 average) {
        Vector3 dir = (average - playerCenter).normalized;
        playerPos.x = playerCenter.x + dir.x * playerRadius;
        playerPos.y = playerCenter.y;
        playerPos.z = playerCenter.z + dir.z * playerRadius;
        initialRadians = radians;
    }

    public bool PlayerInSight() {
        return playerInSight;
    }

    public Vector3 GetPosition() {
        return playerPos;
    }
}
