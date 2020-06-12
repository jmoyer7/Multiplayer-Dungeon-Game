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

    [SerializeField]
    public GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 player2Pos = GameObject.Find("Player2_Spawn").transform.position;


        //have to instantiate in quickinst since player cant inst itself
        if (LocalPlayerInstance == null)
        {
            print("gjjgy");

            PhotonNetwork.Instantiate(this._player.name, player2Pos, Quaternion.identity);
            playerUIPrefab = Instantiate(playerUI);
            playerUIPrefab.transform.SetParent(this.transform);

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
        
    }
}
