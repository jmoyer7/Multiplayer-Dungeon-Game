using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCamera : MonoBehaviourPun
{
    public GameObject target;

    public float distance;

    Transform cameraTransform;

    private GameObject player;

    public Vector3 camPos;


    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

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


    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + offset, ref velocity, smoothSpeed * Time.deltaTime);
        //cameraTransform.LookAt(target.transform);

        //Vector3 camPos = new Vector3(target.transform.position.x, target.transform.position.y + distance, target.transform.position.z + distance);

        // cameraTransform.position = camPos;

        // cameraTransform.LookAt(target.transform);


    }
}
