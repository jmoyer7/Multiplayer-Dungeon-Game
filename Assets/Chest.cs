using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public float radius;

    public LayerMask IgnoreMe;

    public GameObject playerUI;
    public GameObject chestUI;
    public static Collider lastOpened;

    public static bool chestOpen;

    

    private void Awake()
    {

        chestOpen = false;
        
    }

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {           
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit,Mathf.Infinity,~IgnoreMe))
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
        print("Open Chest");


        lastOpened = playerObj;

        playerUI = playerObj.transform.GetChild(0).gameObject;
        chestUI = playerUI.transform.GetChild(2).transform.gameObject;

        this.GetComponent<Loot>().FillChest(chestUI);

        chestUI.SetActive(true);
        chestOpen = true;

    }
    public void closeChest(Collider playerObj)
    {
        print("Close Chest");

        playerUI = playerObj.transform.GetChild(0).gameObject;
        chestUI = playerUI.transform.GetChild(2).transform.gameObject;
        chestUI.SetActive(false);
        chestOpen = false;
    }

   
}

