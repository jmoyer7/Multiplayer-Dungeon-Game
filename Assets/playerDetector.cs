using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("Player Here");

        GetComponentInParent<NPCMove>().playerNearMe = true;

        NPCMove.playerInRange = true;
        
    }

    private void OnCollisionStay(Collision collision)
    {
        NPCMove.playerInRange = true;
    }

}
