using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment",menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int damageModifier;
    public int armorModifier;
}

public enum EquipmentSlot { Weapon,Secondary,Armor}
