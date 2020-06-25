using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviourPun
{
    GameObject target;

    private int targetInt;

    Transform myCam;

    public float distance;

    Transform cameraTransform;



    private GameObject player;

    private float y;
    private float z;
    private float x;

    public Vector3 camPos;



    private void Start()
    {



        cameraTransform = Camera.main.transform;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            
            if (PhotonView.Get(player).IsMine)
            {
                
                target = player;
                break;
            }
        }

        
        

        
      
    }


    void Update()
    {
        Vector3 pos = target.transform.position;
        pos.z += distance;
        pos.y -= distance;
        transform.position = pos;
        cameraTransform.LookAt(target.transform);

    }
}
