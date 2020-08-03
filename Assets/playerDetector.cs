using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<NPCMove>().playersNearMe++;

            NPCMove.playersInRange++;

            NPCMove.turnOrder.Add(gameObject.transform.parent.gameObject);
            NPCMove.turnOrderSize++;
            print("Adding to Turn Order");
        }
    }

  

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<NPCMove>().playersNearMe--;

            NPCMove.playersInRange--;

            print("EXITING COLLISION");

            NPCMove.turnOrder.Remove(gameObject.transform.parent.gameObject);
            NPCMove.turnOrderSize--;
        }
    }

}
