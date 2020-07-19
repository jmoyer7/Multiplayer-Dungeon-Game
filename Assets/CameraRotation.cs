using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform cameraJig;
    public float rotateSpeed;
    private Vector3 velocity = Vector3.zero;
    

    void LateUpdate()
    {

        //PROBLEM WITH ROTATION IS HERE. FIND ANOTHER WAY TO MOVE CAMERA TO PLAYER LOCATION.
        //transform.position = Vector3.SmoothDamp(transform.position, cameraJig.transform.position + offset, ref velocity, smoothSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(cameraJig.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(cameraJig.position, -Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}
