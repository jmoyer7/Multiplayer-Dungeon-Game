using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject[] loot;
    public int[] dropRates;
    public GameObject item1;

    public int minLoot;

    public int itemsAdded;

    public int lootSlots = 9;

   
    void GetDropRates()
    {
        dropRates = new int[loot.Length];

        for(int i = 0; i < loot.Length; i++)
        {
            dropRates[i] = loot[i].GetComponent<ItemDragHandler>().item.dropRate;
        }       
    }

    public GameObject GetRandomItem()
    {       
        int range = 0;

        for (int i = 0; i < dropRates.Length; i++)
            range += dropRates[i];

        int rand = Random.Range(0, range);

        int cumulative = 0;

        for(int i = 0; i < dropRates.Length; i++)
        {
            cumulative += dropRates[i];
            if(rand < cumulative)
            {
                return loot[i];
            }            
        }

        
        return null;
    }

    public void FillChest(GameObject chestUI)
    {
        GetDropRates();

        for (int i = 0; i < lootSlots; i++)
        {
            AddItem(GetRandomItem(), chestUI);
        }
    }

    void AddItem(GameObject item,GameObject chestUI)
    {
        item1 = Instantiate(item, chestUI.transform.parent);
        item1.transform.position = chestUI.transform.GetChild(0).transform.position;
        item1.transform.SetParent(chestUI.transform.GetChild(0));
    }

    
}
