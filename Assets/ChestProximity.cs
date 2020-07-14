using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestProximity : Chest
{
    
    private void OnCollisionExit(Collision collision)
    {
        print("here");
   
        if (chestOpen)
        {
            print("closing");
            closeChest(lastOpened);
        }
    }
}
