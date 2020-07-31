using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        GetComponentInParent<NPCMove>().playersNearMe++;

        NPCMove.playersInRange++;

        NPCMove.turnOrder.Add(gameObject.transform.parent.gameObject);
        NPCMove.turnOrderSize++;
        print("Adding to Turn Order");
    }

    private void OnCollisionStay(Collision collision)
    {
        //NPCMove.playerInRange = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponentInParent<NPCMove>().playersNearMe--;

        NPCMove.playersInRange--;

        print("EXITING COLLISION");

        NPCMove.turnOrder.Remove(gameObject.transform.parent.gameObject);
        NPCMove.turnOrderSize--;
    }

}
