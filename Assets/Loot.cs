using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject[] loot;
    
    public void fillChest(GameObject chestUI)
    {
        print("Looting");

        //instantiate under playerUI not chest. Then move once spawned.
        Instantiate(loot[0], chestUI.transform.GetChild(0));

        
    }

    
}
