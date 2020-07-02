using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerStatus : MonoBehaviourPun
{
    public int maxHealth = 100;
    public int currentHealth;

    public static GameObject LocalPlayerInstance;

    public GameObject healthBar;

    public GameObject playerUI;

    private GameObject playerUIPrefab;

    public int turnCount = 0;

    private GameObject inventory;

    public bool invOpen = false;




    // Start is called before the first frame update
    void Start()
    {
        //Vector3 player2Pos = GameObject.Find("Player2_Spawn").transform.position;

        if (base.photonView.IsMine)
        {
            //this.GetComponent<PhotonTransformView>().enabled = true;
            this.GetComponent<PlayerMove>().enabled = true;

        }


        if (playerUI != null)
        {
            //playerUIPrefab = Instantiate(playerUI);
            //playerUIPrefab.transform.SetParent(this.transform);
            //playerUIPrefab.SendMessage("SetTarget",this)
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }



        if (base.photonView.IsMine)
        {
            inventory = this.transform.GetChild(0).transform.GetChild(3).gameObject;
        }
        currentHealth = maxHealth;
        healthBar = GameObject.Find("Health Bar");



        healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth);





    }

    private void Awake()
    {
        //needs to go on player object
        if (photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (!invOpen)
            {
                if (photonView.IsMine)
                {
                    inventory.GetComponent<UIelementsMovement>().openInventory();
                    invOpen = true;
                }
            }
            else
            {
               
                inventory.GetComponent<UIelementsMovement>().closeInventory();
                invOpen = false;
            }
        }


    }
}

