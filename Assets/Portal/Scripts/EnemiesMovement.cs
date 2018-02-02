using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour {

    public GameObject portalToSpawn;
    public bool teleported;

	// Use this for initialization
	void Start () {
        portalToSpawn = GameObject.Find("PlaneToSpawn");
        teleported = false;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(this.transform.forward * Time.deltaTime * 2.0f,Space.World);
	}

    private void OnTriggerEnter(Collider other) {
        if (!teleported) {
            if (other.transform.tag == "Portal") {
                teleported = true;
                Vector3 newPos = portalToSpawn.transform.position - other.transform.position + this.transform.position;
                this.transform.position = newPos;
            }
        }
    }
}
