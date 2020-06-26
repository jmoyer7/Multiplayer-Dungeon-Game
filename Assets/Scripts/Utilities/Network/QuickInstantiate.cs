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

    private void Start()
    {
        Vector2 offset = Random.insideUnitCircle * 2f;
        Vector3 position1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        Vector3 enemyPos = GameObject.Find("EnemySpawn").transform.position;


        if (PlayerStatus.LocalPlayerInstance == null)
        {
            playersInGame = GameObject.FindGameObjectsWithTag("Player");

            print(playersInGame.Length);

            if (playersInGame.Length == 0)
            {
                spawnPos = GameObject.Find("PlayerSpawn1").transform.position;
            }
            else if (playersInGame.Length == 1)
            {
                spawnPos = GameObject.Find("PlayerSpawn2").transform.position;
            }
            else if (playersInGame.Length == 2)
            {
                spawnPos = GameObject.Find("PlayerSpawn3").transform.position;
            }
           

            PhotonNetwork.Instantiate(this._player.name, spawnPos, Quaternion.identity);
            playerUIPrefab = Instantiate(playerUI);
            playerUIPrefab.transform.SetParent(PlayerStatus.LocalPlayerInstance.transform);
            enemy = PhotonNetwork.InstantiateSceneObject(this.enemyPrefab.name, enemyPos, Quaternion.identity);

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
