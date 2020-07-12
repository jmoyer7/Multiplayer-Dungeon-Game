using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject playerUI;

    public GameObject healthBar;

    public PlayerStatus playerStatus;

    public int trapDamage = 10;

    public bool trapUsed = false;

    public int trapSetterID;

    public GameObject trapSetter;

    private void OnCollisionEnter(Collision collision)
    {
        
        if (!trapUsed)
        {
            if (collision.gameObject.tag == "Player")
            {
                trapSetter = PhotonView.Find(trapSetterID).gameObject;

                if (collision.gameObject != trapSetter)
                {

                    //Check viewID
                    playerUI = collision.transform.GetChild(0).gameObject;
                    healthBar = playerUI.transform.GetChild(0).gameObject;
                    playerStatus = collision.transform.GetComponent<PlayerStatus>();

                    activateTrap();
                }
            }
        }
    }
    public void activateTrap()
    {
        playerStatus.currentHealth -= trapDamage;
        healthBar.GetComponent<HealthBar>().SetHealth(playerStatus.currentHealth);
        trapUsed = true;
    }


}
