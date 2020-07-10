using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject playerUI;

    public GameObject healthBar;

    public PlayerStatus playerStatus;

    public int trapDamage = 10;

    private void OnCollisionEnter(Collision collision)
    {      
            if (collision.gameObject.tag == "Player")
            {
                playerUI = collision.transform.GetChild(0).gameObject;
                healthBar = playerUI.transform.GetChild(0).gameObject;
                playerStatus = collision.transform.GetComponent<PlayerStatus>();

            activateTrap();
        }       
    }
    public void activateTrap()
    {
        playerStatus.currentHealth -= trapDamage;
        healthBar.GetComponent<HealthBar>().SetHealth(playerStatus.currentHealth);
    }


}
