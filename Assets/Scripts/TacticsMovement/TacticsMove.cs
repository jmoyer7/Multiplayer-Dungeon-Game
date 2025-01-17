﻿using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class TacticsMove : MonoBehaviourPunCallbacks
{
    public static GameObject[] players;
    public int[] viewIDs = new int[4];

    private TacticsMove tacticsMove;

    private TurnManagerPun2 turnManagerPun2;

    public static bool endingTurn = false;

    public static bool turn = false;
    public static bool enemyTurn = false;

    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    public Tile currentTile;

    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;

    public static bool myTurn = false;

    Vector3 jumpTarget;

    public Tile actualTargetTile;

    public static int playerCount;

    


    public void Start()
    {
        

        players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            viewIDs[i] = players[i].GetComponent<PhotonView>().ViewID;
        }

       
        playerCount = players.Length;
        
    }

    private void Update()
    {

       


        if (endingTurn)
        {
            
            if (!enemyTurn)
            {
                
               //Gets here
                TurnManagerPun2.EndOfTurn = true;

                

                
                //Issue may be here
                    tacticsMove = this.GetComponent<TacticsMove>();
                
                

                tacticsMove.EndTurn();

                //player.GetComponent(photonView).RPC("SendTurn", RpcTarget.Others, TurnManagerPun2.EndOfTurn);
                
            }
        }
    }

    
    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {
        //tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight, target);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ??  leave as null 

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    

                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
           
            path.Push(next);
            next = next.parent;
            
        }
    }

    public void Move(PhotonView photonView)
    {

        if (path.Count > 0)
        {
            Tile t = path.Peek();

            
            //Prevents enemy from moving on top of other enemy.
            //Works as temp. fix but still, the path chosen should not be through
            //object to begin with. Will need proper fix.
            if (t.CheckForEnemy(t, photonView))
            {
                t = path.Pop();
            }

            Vector3 target = t.transform.position;

            //Calculate the unit's position on top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;

                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizotalVelocity();
                }

                //Locomotion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;

            EndTurn();
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizotalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
    }

    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizotalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            velocity /= 5.0f;
            velocity.y = 1.5f;
        }
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list[0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }

        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {

        print("FInding end tile");
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;
        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            print("Checking end tile HERE");
            if (checkForEnemy(t.parent))
            {
                print("enemy on end tile");

            }
            
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        print("Checking end tile here");
        if (checkForEnemy(endTile))
        {
            print("enemy on end tile");

            //endTile = endTile.getAdjacentTile(endTile);
        }

        //endTile.GetComponent<Tile>().trap = true;


       
        return endTile;
    }

    public bool checkForEnemy(Tile tile)
    {
        RaycastHit hit;

        print(tile);
        if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 2))
        {
            
            //This works when the case is actually true. But the issue is that
            //the enemies are using the same path and walking through each other
            //Probably can be fixed by checking each tile in adjacency list for an enemy
            //And removing it if so BEFORE deciding on path.

            return true;
        }
        else
        {
            return false;
        }
    }

    

    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();

        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent = ??
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
        currentTile.f = currentTile.h;

        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            closedList.Add(t);

            if (t == target)
            {
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }



            foreach (Tile tile in t.adjacencyList)
            {

                if (closedList.Contains(tile))
                {
                    //Do nothing, already processed
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }
        }

        //todo - what do you do if there is no path to the target tile?
        Debug.Log("Path not found");
    }

    public static void BeginTurn()
    {
        turn = true;

        myTurn = true;

        TurnManagerPun2.EndOfTurn = false;     
    }




    




    public void EndTurn()
    {
        if (enemyTurn)
        {
            if(NPCMove.turnOrderSize > 1)
            {
                NPCMove.turnsTaken++;
            }

            //print(NPCMove.turnsTaken);
            //print(NPCMove.turnOrderSize);

            //If more than one enemy is in range and seperate turns are required
            if (NPCMove.turnOrderSize > 1 && NPCMove.turnsTaken != NPCMove.turnOrderSize)
            {
                //get component in enemy whose turn it is
                GetComponent<NPCMove>().thisTurn = false;
                

                print("back to enemy turn");
                

                NPCMove.EnemyTurn();

            }
            else
            {
                GetComponent<NPCMove>().thisTurn = false;
                print("Ending enemy turn");
                enemyTurn = false;
                NPCMove.turnsTaken = 0;
                TurnManagerPun2.SendTurnEvent();
            }
            
        }
        else
        {


 

            turnManagerPun2 = this.GetComponent<TurnManagerPun2>();

            turn = false;


            //TurnManagerPun2.turnCount++;

            //print(players.Length);
            //print("Turn Count: " + TurnManagerPun2.turnCount);

            //if (TurnManagerPun2.turnCount == players.Length)
            //{
            //    TurnManagerPun2.turnCount = 0;
            // }




            //turnManagerPun2.SyncTurnCount();


            PlayerStatus.LocalPlayerInstance.GetComponent<PlayerStatus>().turnCount++;

            
            if (!enemyTurn && endingTurn)
            {
                
                endingTurn = false;
                NPCMove.EnemyTurn();
                myTurn = false;



                //base.photonView.RPC("SendTurn", RpcTarget.Others, TurnManagerPun2.EndOfTurn);
                //GameObject.Find("EndTurn").GetComponent<EndTurnButton>().enabled = true;


            }


        }

        

    }




}
