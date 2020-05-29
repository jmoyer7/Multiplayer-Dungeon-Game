using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviourPun
{
    Vector3 target;

    GameObject myCam;

   


    private void Start()
    {
        myCam = GameObject.Find("Camera");
       

        if (!base.photonView.IsMine)
        {
            (myCam.GetComponent<Camera>()).enabled = false;
        }
        else
        {
            target = GameObject.Find("CameraObject").transform.position;
        }

        
      
    }


    void Update()
    {
        transform.position = target;
    }
}
