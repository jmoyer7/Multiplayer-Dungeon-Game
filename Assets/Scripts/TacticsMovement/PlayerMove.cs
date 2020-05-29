﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : TacticsMove 
{

	// Use this for initialization
	void Start () 
	{
        Init();
	}
	
	// Update is called once per frame
	void Update () 
	{

        Debug.DrawRay(transform.position, transform.forward);
        if (!turn || !base.photonView.IsMine)
        {
            return;
        }

        if (!moving && base.photonView.IsMine)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }
	}
    

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
