using UnityEngine;

public class CameraController : MonoBehaviour {

    #region Public Data

    #endregion

    #region Private Serialized Fields

    [SerializeField]
    private Transform player;
    [SerializeField]
    private LayerMask camOcclusion;

    #endregion

    #region Private Non-Serialized Fields

    private const float xSpeed = 250f;
    private const float ySpeed = 120f;
    private const float yMinLimit = -20f;
    private const float yMaxLimit = 80f;
    private const int zoomRate = 50;
    private const float lerpSpeed = 0.5f;

    private float distance;
    private float x;
    private float y;
    private float t;

    #endregion

    #region Properties

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        distance = 10f;
        x = 0f;
        y = 0f;
        t = 0f;
    }

    private void Start()
    {
        Vector2 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    private void Update()
    {
        RotateCamera();
    }

    #endregion

    #region Public Methods

    #endregion

    #region Private Methods

    private void RotateCamera()
    {
        float test = 0;

        x += System.Convert.ToSingle(Input.GetAxis("Mouse X") * xSpeed * 0.02);
        y -= System.Convert.ToSingle(Input.GetAxis("Mouse Y") * ySpeed * 0.02);
        test = y;

        distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);

        if (distance < 2.5f)
        {
            distance = 2.5f;
        }
        if (distance > 20.0f)
        {
            distance = 20.0f;
        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        if (y == yMinLimit || test == yMinLimit)
        {
            // This is to allow the camera to slide across the bottom if the player is too low in the y
            distance += -(Input.GetAxis("Mouse Y") * Time.deltaTime) * 10 * Mathf.Abs(distance);
        }

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 2.0f, -distance) + player.position;

        transform.rotation = rotation;
        transform.position = position;

        SetPlayerDirection(LerpRotation(rotation.eulerAngles.y));
    }

    private float LerpRotation(float cameraRotationY)
    {
        float playerRotationY = player.rotation.eulerAngles.y;

        if (cameraRotationY != playerRotationY)
        {
            // Increate the t interpolater
            t += lerpSpeed * Time.deltaTime;

            playerRotationY = Mathf.LerpAngle(playerRotationY, cameraRotationY, t);

            if (playerRotationY == cameraRotationY)
            {
                t = 0f;
            }
        }

        return playerRotationY;
    }

    private void SetPlayerDirection(float rotation)
    {
        if (!Input.GetMouseButton(0))
        {
            // Set player rotation to define his movement direction
            player.rotation = Quaternion.Euler(player.rotation.x, rotation, player.rotation.z);
        }
        else
        {
            t = 0;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    #endregion
}