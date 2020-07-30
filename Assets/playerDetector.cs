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
    }

    private void OnCollisionStay(Collision collision)
    {
        //NPCMove.playerInRange = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponentInParent<NPCMove>().playersNearMe--;

        NPCMove.playersInRange--;

        NPCMove.turnOrder.Remove(gameObject.transform.parent.gameObject);
    }

}
