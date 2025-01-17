﻿using System;
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

            int playerID = GameObject.Find("Player(Clone)").GetPhotonView().ViewID;

            GameObject.Find("LevelManager").GetComponent<LevelManager>().SetTrap(playerID);
        }

       

    }

   

  
}

    

