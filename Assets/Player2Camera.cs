using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player2Camera : MonoBehaviourPun
{
    Vector3 target;

    GameObject myCam;

    private void Start()
    {
        myCam = GameObject.Find("Camera2");

        

        if (!base.photonView.IsMine)
        {
            (myCam.GetComponent<Camera>()).enabled = false;
        }
        else
        {
            target = GameObject.Find("CameraObject2").transform.position;
        }
    }


    void Update()
    {
        transform.position = target;
    }
}
