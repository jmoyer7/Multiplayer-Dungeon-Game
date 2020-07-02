using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public float radius;

    public GameObject playerUI;
    public GameObject chestUI;

    

    private void Start()
    {
        
        
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            print("Click");
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
                openChest(PlayerObj);
            }
           
        }
    }

    public void openChest(Collider playerObj) {
        playerUI = playerObj.transform.GetChild(0).gameObject;
        chestUI = playerUI.transform.GetChild(2).transform.gameObject;
        chestUI.SetActive(true);
    }
}
