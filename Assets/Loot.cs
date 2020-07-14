using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item[] loot;
    public int[] dropRates;
    public GameObject item1;

    public GameObject blankItem;

    public int minLoot;

    public int itemsAdded;

    public int lootSlots = 9;

    public int slotCounter;


    void GetDropRates()
    {
        dropRates = new int[loot.Length];

        for (int i = 0; i < loot.Length; i++)
        {
            dropRates[i] = loot[i].dropRate;
        }
    }

    public Item GetRandomItem()
    {
        int range = 0;

        for (int i = 0; i < dropRates.Length; i++)
            range += dropRates[i];

        int rand = Random.Range(0, range);

        int cumulative = 0;

        for (int i = 0; i < dropRates.Length; i++)
        {
            cumulative += dropRates[i];
            if (rand < cumulative)
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

        slotCounter = 0;
    }

    void AddItem(Item item, GameObject chestUI)
    {


        item1 = Instantiate(blankItem, chestUI.transform.parent);
        item1.transform.position = chestUI.transform.GetChild(slotCounter).transform.position;
        item1.transform.SetParent(chestUI.transform.GetChild(slotCounter));
        item1.GetComponent<ItemDragHandler>().item = item;

        if (CheckForEquipment(item1.GetComponent<ItemDragHandler>().item))
        {
            
            var equip = item as Equipment;
            item1.GetComponent<ItemDragHandler>().equipment = equip;
        }

        slotCounter++;
    }

    bool CheckForEquipment(Item item)
    {
        if (item is Equipment)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}