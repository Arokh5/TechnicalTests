using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    public static SceneManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

	private void Update()
    {
        ChangeScene();
    }

    private void ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UnityEngine.SceneManagement.SceneManager.LoadScene("CameraTest", LoadSceneMode.Single);

        else if (Input.GetKeyDown(KeyCode.Alpha2))
            UnityEngine.SceneManagement.SceneManager.LoadScene("SpawningIATest", LoadSceneMode.Single);

        else if (Input.GetKeyDown(KeyCode.Alpha3))
            UnityEngine.SceneManagement.SceneManager.LoadScene("PortalsTest", LoadSceneMode.Single);

        else if (Input.GetKeyDown(KeyCode.Alpha4))
            UnityEngine.SceneManagement.SceneManager.LoadScene("ShadersTest", LoadSceneMode.Single);
    }
}