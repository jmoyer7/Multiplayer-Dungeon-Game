using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttack : TacticsMove
{

    public Tile thisTile;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      

        thisTile = GetTargetTile(gameObject);
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
