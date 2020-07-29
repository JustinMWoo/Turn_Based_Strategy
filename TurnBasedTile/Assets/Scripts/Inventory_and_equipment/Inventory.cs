using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public Weapon onHand;
    public Weapon offHand;
    public Armor armor;
    readonly List<Item> backpack;

    public Inventory()
    {
        onHand = new Weapon();
        offHand = new Weapon();
        armor = new Armor();
        backpack = new List<Item>();
    }

    public void EquipOnHand(Weapon equipOnHand)
    {
        onHand = equipOnHand;
    }

    public void EquipOffHand(Weapon equipOffHand)
    {
        offHand = equipOffHand;
    }

    public void EquipArmor(Armor equipArmor)
    {
        armor = equipArmor;
    }

    public void AddToInventory(Item item)
    {
        backpack.Add(item);
    }
}
