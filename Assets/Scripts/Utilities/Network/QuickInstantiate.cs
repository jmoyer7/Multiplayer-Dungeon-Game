using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class QuickInstantiate : MonoBehaviourPun
{


    
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

    public GameObject[] playerSpawns;

    



    private void Start()
    {
        Vector2 offset = Random.insideUnitCircle * 2f;
        Vector3 position1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        Vector3 enemyPos = GameObject.Find("EnemySpawn").transform.position;


        if (PlayerStatus.LocalPlayerInstance == null)
        {
            //Account for player count in game

            playerSpawns = GameObject.FindGameObjectsWithTag("spawn");

            randomSpawn = Random.Range(0, playerSpawns.Length);

            spawnPos = playerSpawns[randomSpawn].transform.position;
           
            GameObject myPlayer = PhotonNetwork.Instantiate(this._player.name, spawnPos, Quaternion.identity);
            playerUIPrefab = Instantiate(playerUI);
            playerUIPrefab.transform.SetParent(PlayerStatus.LocalPlayerInstance.transform);
            enemy = PhotonNetwork.InstantiateSceneObject(this.enemyPrefab.name, enemyPos, Quaternion.identity);

            Destroy(playerSpawns[randomSpawn]);

           
        }
        else
        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            /*
            player1 = PhotonNetwork.Instantiate(this._prefab.name, position1, Quaternion.identity);
            
            playerUIPrefab = Instantiate(playerUI);
            playerUIPrefab.transform.SetParent(player1.transform);

            */
        }
        
    }

    private void Awake()
    {
        

    }




}
