using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : PlayerCamera
{
    public Transform cameraJig;
    public float rotateSpeed;
    private Vector3 velocity = Vector3.zero;
    




    

    void LateUpdate()
    {
        

        //PROBLEM WITH ROTATION IS HERE. FIND ANOTHER WAY TO MOVE CAMERA TO PLAYER LOCATION.

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
