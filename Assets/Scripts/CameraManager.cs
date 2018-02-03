using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    #region Public Data

    #endregion

    #region Private Serialized Fields

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject frontCamera;

    #endregion

    #region Private Non-Serialized Fields

	#endregion
	
	#region Properties

    #endregion
	
	#region MonoBehaviour Methods

    private void Update()
    {
        SwapCamera();
    }

	#endregion
	
	#region Public Methods
	
	#endregion
	
	#region Private Methods
	
    private void SwapCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            mainCamera.SetActive(false);
            frontCamera.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(2))
        {
            frontCamera.SetActive(false);
            mainCamera.SetActive(true);
        }
    }

	#endregion
}