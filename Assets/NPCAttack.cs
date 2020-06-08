using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : TacticsMove
{
    public bool AttackedThisTurn = false;


    //private Tile thisTileScript;

    // public Tile thisTile;
    //List<GameObject> currentCollisions = new List<GameObject>();


    




    // Start is called before the first frame update
    void Start()
    {

    }

    public void checkForPlayer(Vector3 center, float radius)
    {
        Collider[] collider = Physics.OverlapSphere(center, radius);

        foreach (Collider PlayerObj in collider)
        {
            if (PlayerObj.gameObject.tag == "Player")
            {
                print("Attacking Now!");
                AttackedThisTurn = true;
                //Attacking = true;
            }
            else
            {
                //Attacking = false;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {

       if (!TurnManagerPun2.EndOfTurn)
       {
            AttackedThisTurn = false;
       }

        if (enemyTurn && !AttackedThisTurn)
        {
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
