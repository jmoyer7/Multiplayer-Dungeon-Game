using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : TacticsMove
{
    public bool AttackedThisTurn = false;

    public int attackPower;


    //private Tile thisTileScript;

    public Tile thisTile;
    //List<GameObject> currentCollisions = new List<GameObject>();

    TacticsMove t;

    public PlayerStatus playerStatus;
    




    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<TacticsMove>();
        attackPower = 5;


    }

    public void checkForPlayer(Vector3 center, float radius)
    {
        Collider[] collider = Physics.OverlapSphere(center, radius);

        foreach (Collider PlayerObj in collider)
        {
            if (PlayerObj.gameObject.tag == "Player")
            {


                playerStatus = PlayerObj.GetComponent<PlayerStatus>();
                Attack();
                print("Attacking Now!");
                AttackedThisTurn = true;
                //Attacking = true;
            }
            
        }
        
    }


    public void Attack()
    {
        playerStatus.health -= attackPower;
        
        
    }

    // Update is called once per frame
    void Update()
    {



        thisTile = GetTargetTile(gameObject);

        if (!TurnManagerPun2.EndOfTurn)
        {
            AttackedThisTurn = false;
        }
        
        //Checks if the NPC is on the tile that they were trying to get to
        //*Clean up these Variables*
        if (enemyTurn && !AttackedThisTurn && thisTile == t.actualTargetTile)
        {
            //if so, check if there is a player around
            checkForPlayer(transform.position, 4);
        }


        //      thisTile = GetTargetTile(gameObject);

        //        thisTileScript = thisTile.GetComponent<Tile>();

        /*
        foreach (Tile t in thisTileScript.adjacencyList){
            print(t);
        }
        */

        //look at adjacency list



        //Get tiles touching this tile
        //check if player is touching those tiles
        //if so, attack one time



        //use findNeighbords function

        //thisTile = GetTargetTile(this.gameObject);

        //if(thisTile.parent == GetTargetTile(GameObject.FindWithTag("Player")))
        // {
        //  print("I AM HERE");
        // }
    }
}
