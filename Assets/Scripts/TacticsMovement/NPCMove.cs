using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NPCMove : TacticsMove 
{
    GameObject target;
    public static bool IsMoving = false;
    public static bool endOfEnemyTurn = false;

    //Send RPC for IsMoving,working properly for master only right now

	// Use this for initialization
	void Start () 
	{
        this.gameObject.SetActive(true);
        //Init();
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
            IsMoving = true;
            Move();
            
        }
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
