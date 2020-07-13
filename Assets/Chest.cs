﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public float radius;

    public GameObject playerUI;
    public GameObject chestUI;
    public Collider lastOpened;

    private bool chestOpen = false;

    

    private void Start()
    {
        
        
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {           
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.tag == "chest")
                    { checkDistance(); }
                } 
        }

        

        

        
    }

    private void checkDistance()
    {
        

        Collider[] collider = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider PlayerObj in collider)
        {
            
            if (PlayerObj.gameObject.tag == "Player")
            {
                if (!chestOpen)
                {
                    
                    openChest(PlayerObj);
                }
                else
                {
                    closeChest(PlayerObj);
                }
               
            }
            
            
            
           
        }
    }

    public void openChest(Collider playerObj) {

        lastOpened = playerObj;

        playerUI = playerObj.transform.GetChild(0).gameObject;
        chestUI = playerUI.transform.GetChild(2).transform.gameObject;

        this.GetComponent<Loot>().FillChest(chestUI);

        chestUI.SetActive(true);
        chestOpen = true;

    }
    public void closeChest(Collider playerObj)
    {
        playerUI = playerObj.transform.GetChild(0).gameObject;
        chestUI = playerUI.transform.GetChild(2).transform.gameObject;
        chestUI.SetActive(false);
        chestOpen = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (chestOpen)
        {
            closeChest(lastOpened);
        }
    }
}

