using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Public Data

    #endregion

    #region Private Serialized Fields

    #endregion

    #region Private Non-Serialized Fields

    private const float playerSpeed = 10f;

    #endregion

    #region Properties

    #endregion

    #region MonoBehaviour Methods

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

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

    #endregion
}