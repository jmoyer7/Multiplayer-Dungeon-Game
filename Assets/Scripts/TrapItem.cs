using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrapItem : MonoBehaviour, IPointerDownHandler
{
    private const byte SET_TRAP_EVENT = 2;

    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;


    public void OnPointerDown(PointerEventData eventData)
    {
        clicked++;
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            setTrap(eventData);
            clicked = 0;
            clicktime = 0;
            

        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;

    }

    public void setTrap(PointerEventData eventData)
    {

        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(GameObject.Find("Player(Clone)").transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
            tile.trap = true;

            //pass coords to rpc and set trap there
            //move levelManager to this location and then send ray to find closest tile
            //add component to this tile
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;

            int playerID = GameObject.Find("Player(Clone)").GetPhotonView().ViewID;

            float[] trapCoords = new float[2];
            //trapCoords[0] = x;
           // trapCoords[1] = y;
            //trapCoords[2] = z;

            GameObject.Find("LevelManager").GetComponent<LevelManager>().SetTrap(playerID);
            //tile.gameObject.AddComponent<TrapTrigger>();
            //GameObject.Find("Player(Clone)").gameObject.transform.GetComponent<TurnManagerPun2>().SetTrap(tile.gameObject);

        }

        //PhotonNetwork.Instantiate("TrapTrigger", GameObject.Find("Player(Clone)").transform.position, Quaternion.identity);

    }

   

  
}

    

