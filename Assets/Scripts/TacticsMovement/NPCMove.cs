using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NPCMove : TacticsMove 
{
    GameObject target;
    public static bool IsMoving = false;
    public static bool endOfEnemyTurn = false;
    public int radius;
    public int playersNearMe = 0;   //True if player is in range for THIS enemy
    public static int playersInRange = 0;  //Tracks number of players in range of enemies

    public static ArrayList turnOrder;
   


    //Send RPC for IsMoving,working properly for master only right now

	// Use this for initialization
	void Start () 
	{
        Init();

        turnOrder = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);
        

        if (!enemyTurn)
        {
            IsMoving = false;
            return;
        }

        if (!moving)
        {
            IsMoving = false;
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
            if (playersNearMe > 0)
            {
                IsMoving = true;
                Move();
            }
            else
            {
                if (playersInRange == 0)
                {
                    EndTurn();
                }
            }
        }
	}

    bool checkForPlayer()
    {
        bool playerInRange = false;

        Collider[] collider = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider PlayerObj in collider)
        {
            if (PlayerObj.gameObject.tag == "Player")
            {
                playerInRange = true;
                playerCount++;
            }

        }
        return playerInRange;
    }


    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }


    public static void EnemyTurn()
    {

        
        
        enemyTurn = true;
    }
}
