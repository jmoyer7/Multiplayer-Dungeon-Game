using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class NPCStatus : MonoBehaviour
{
    public GameObject healthBarUI;

    public float health;

    public float maxHealth = 50;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBarUI = transform.GetChild(1).transform.gameObject.transform.GetChild(0).transform.gameObject;
    }

    [PunRPC]
    void SetHealth(float enemyHealth)
    {
        health = enemyHealth;

        healthBarUI.GetComponent<Slider>().value = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (TacticsMove.myTurn)
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
}
