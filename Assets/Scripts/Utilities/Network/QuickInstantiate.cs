using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickInstantiate : MonoBehaviourPun
{

    private const byte DESTROY_SPAWN_EVENT = 1;


    public static GameObject player1;
    public static GameObject player2;
    public static GameObject enemy;
    public static GameObject[] playersInGame;
    public static Vector3 spawnPos;

    private int randomSpawn;


    [SerializeField]
    public GameObject _prefab;

    [SerializeField]
    public GameObject _prefab2;

    [SerializeField]
    public GameObject _prefab3;


    [SerializeField]
    public GameObject enemyPrefab;

    [SerializeField]
    public GameObject playerUI;

    [SerializeField]
    private GameObject playerUIPrefab;

    [SerializeField]
    public GameObject _player;

    public static GameObject[] playerSpawns;

    



    private void Start()
    {
        Vector2 offset = UnityEngine.Random.insideUnitCircle * 2f;
        Vector3 position1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        Vector3 enemyPos = GameObject.Find("EnemySpawn").transform.position;


        if (PlayerStatus.LocalPlayerInstance == null)
        {
            //Account for player count in game

            playerSpawns = GameObject.FindGameObjectsWithTag("spawn");

            randomSpawn = UnityEngine.Random.Range(0, playerSpawns.Length);

            spawnPos = playerSpawns[randomSpawn].transform.position;
           
            GameObject myPlayer = PhotonNetwork.Instantiate(this._player.name, spawnPos, Quaternion.identity);
            playerUIPrefab = Instantiate(playerUI);
            playerUIPrefab.transform.SetParent(PlayerStatus.LocalPlayerInstance.transform);
            enemy = PhotonNetwork.InstantiateSceneObject(this.enemyPrefab.name, enemyPos, Quaternion.identity);

            /*
            object[] datas = new object[] { playerSpawns[randomSpawn] };
            

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All
            };

            PhotonNetwork.RaiseEvent(DESTROY_SPAWN_EVENT, playerSpawns[randomSpawn], raiseEventOptions, SendOptions.SendReliable);

            */
            Destroy(playerSpawns[randomSpawn]);
        }
    }

    /*
    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    //Not working as is
    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == DESTROY_SPAWN_EVENT)
        {
            
            GameObject toDestroy = (GameObject)obj[0];

            
            Destroy(toDestroy);
            


        }
    }
    */
}
