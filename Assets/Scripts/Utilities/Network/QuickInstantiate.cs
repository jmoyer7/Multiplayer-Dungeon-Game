using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class QuickInstantiate : MonoBehaviourPun
{

    

    public static GameObject player1;
    public static GameObject player2;
    public static GameObject enemy;

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

    private void Start()
    {
        Vector2 offset = Random.insideUnitCircle * 2f;
        Vector3 position1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 player2Pos = GameObject.Find("Player2_Spawn").transform.position;
        Vector3 enemyPos = GameObject.Find("EnemySpawn").transform.position;


        if (PhotonNetwork.IsMasterClient)
        {
            /*
            player1 = PhotonNetwork.Instantiate(this._prefab.name, position1, Quaternion.identity);
            enemy = PhotonNetwork.InstantiateSceneObject(this.enemyPrefab.name, enemyPos, Quaternion.identity);
            playerUIPrefab = Instantiate(playerUI);
            playerUIPrefab.transform.SetParent(player1.transform);

            */
        }
        
    }

    private void Awake()
    {
        

    }




}
