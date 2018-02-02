using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

    public Transform playerCamera, portal, otherPortal;
	
	void Update () {
        Vector3 playerOffsetFromPortal = playerCamera.transform.position - otherPortal.transform.position;
        this.transform.position = portal.transform.position + playerOffsetFromPortal;

        float angularDifference = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);


	}
}
