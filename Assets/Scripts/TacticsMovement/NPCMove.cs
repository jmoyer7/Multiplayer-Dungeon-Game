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
    public bool playerInRange = false;


    //Send RPC for IsMoving,working properly for master only right now

	// Use this for initialization
	void Start () 
	{
        Init();
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
            if (playerInRange)
            {
                IsMoving = true;
                Move();
            }
            else
            {
                EndTurn();
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
