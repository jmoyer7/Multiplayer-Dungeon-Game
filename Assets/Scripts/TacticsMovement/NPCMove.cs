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

    public bool thisTurn = false; //whether or not it is THIS enemy's turn

    public static List<GameObject> turnOrder;

    public static int turnOrderSize = 0;

    public static int turnsTaken = 0;

    public bool turnOver = false;
    public static GameObject temp;
   


    //Send RPC for IsMoving,working properly for master only right now

	// Use this for initialization
	void Start () 
	{
        Init();

        turnOrder = new List<GameObject>();
        
    }

    //CLEAN UP THIS MESS WITH SWITCH STATEMENTS AND VARS
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);
        

        if (!enemyTurn)
        {
            IsMoving = false;
            return;
        }

        if (!moving && enemyTurn && playersNearMe > 0)
        {
            IsMoving = false;
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
            actualTargetTile.target = true;
        }
        else
        {
          if(turnOrderSize > 1)
            {
                if (thisTurn)
                {
                    IsMoving = true;
                    Move();

                }
                
            }
          
          else if(playersNearMe > 0 && turnOrderSize < 2)
            {

                
                IsMoving = true;
                Move();
            }
                else
                {
                    if (playersInRange == 0 )
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
        if (turnOrderSize > 1 && turnsTaken != turnOrderSize)
        {
            print("turn order > 1");
            //Might need more work here for when it's the last enemy's turn so that turn passes to player
                turnOrder[0].GetComponent<NPCMove>().thisTurn = true;
            print(turnOrder[0].transform.position + "Is taking turn!");
            //for some reason not moving on its turn and other enemy is moving. Fix.
                temp = turnOrder[0];
                turnOrder[0] = turnOrder[1];
                turnOrder[turnOrderSize - 1] = temp;
                

                

            

                
        }
        
        
        
    }
}
