using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        GetComponentInParent<NPCMove>().playerNearMe = true;

        NPCMove.playersInRange++;
        
    }

    private void OnCollisionStay(Collision collision)
    {
        //NPCMove.playerInRange = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponentInParent<NPCMove>().playerNearMe = false;

        NPCMove.playersInRange--;
    }

}
