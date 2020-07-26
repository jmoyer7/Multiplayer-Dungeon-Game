using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print("Player Here");

        GetComponentInParent<NPCMove>().playerInRange = true;
        
        
    }
   
}
