using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private const float playerSpeed = 10f;

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * playerSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
    }
}