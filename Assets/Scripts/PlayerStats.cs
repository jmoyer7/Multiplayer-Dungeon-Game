using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject inventory;

    public GameObject weaponSlot;

    public GameObject secondarySlot;

    public GameObject armorSlot;

    public int attack;

    public int defense;



    void Start()
    {
        inventory = this.transform.GetChild(0).transform.GetChild(3).gameObject;
        weaponSlot = inventory.transform.GetChild(1).transform.GetChild(0).gameObject;
    }

    public void updateEquipment(Equipment newEquipment)
    {
        attack = newEquipment.damageModifier;

        defense = newEquipment.armorModifier;


    } 
}
