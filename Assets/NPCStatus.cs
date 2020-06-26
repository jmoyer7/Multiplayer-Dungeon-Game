using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NPCStatus : MonoBehaviour
{
    public int health = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    [PunRPC]
    void SetHealth(int enemyHealth)
    {
        health = enemyHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "enemy")
                { this.transform.GetChild(0).gameObject.SetActive(true); }
            }
        }
    }
}
