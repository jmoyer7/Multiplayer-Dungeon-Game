using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerStatus : MonoBehaviourPun
{
    public int maxHealth = 100;
    public int currentHealth;

    
    public GameObject healthBar;

    public GameObject playerUI;

    private GameObject playerUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //playerUIPrefab = Instantiate(playerUI);
            //playerUIPrefab.transform.SetParent(gameObject.transform);
        }
        else
        {
            //playerUIPrefab = Instantiate(playerUI);
            //playerUIPrefab.transform.SetParent(gameObject.transform);
        }
       
        currentHealth = maxHealth;
        healthBar = GameObject.Find("Health Bar");



        healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth);

        
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
