using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.CharacterStats;

public class Unit : MonoBehaviour
{

    public string Name;

    public bool turn = false; // True when it is this units turn
    public Queue<UnitActions> actions = new Queue<UnitActions>();
    [NonSerialized]
    public UnitActions currAction;

    public BaseCharacterClass unitClass;

    // inventory variables
    public SharedInventory SharedInventory;
    public EquippableItem Weapon;
    public EquippableItem Armor;
    public EquippableItem Accessory;

    // Stat Variables
    public CharacterStats Health;
    public CharacterStats Mana;
    public CharacterStats Strength;
    public CharacterStats Intellect;
    public CharacterStats Dexterity;
    public CharacterStats Damage;

    public bool BaseStatsLoaded = false;

    void Start()
    {
        // for inventory opening/closing

        UnitActions[] acts = GetComponents<UnitActions>();
        foreach (UnitActions action in acts)
        {
            actions.Enqueue(action);
        }

        currAction = actions.Peek();

        // Add unit to turn order (static so dont need instance of turn manager)
        TurnManager.AddUnit(this);
        
    }
   
    void Update()
    {
        currAction.Execute();
    }

    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }

    public void EquipFromInventory(EquippableItem item)
    {
        if (item.EquipmentType == EquipmentType.Weapon)
        {
            if (this.Weapon == null)
            {
                this.Weapon = item;
                item.Equip(this);
                SharedInventory.Remove(item);
                
            }
            else
            {
                UnequipToInventory(this.Weapon);
                this.Weapon = item;
                item.Equip(this);
                SharedInventory.Remove(item);
            }
        }

        if (item.EquipmentType == EquipmentType.Armor)
        {
            if (this.Armor == null)
            {
                this.Armor = item;
                item.Equip(this);
                SharedInventory.Remove(item);
            }
            else
            {
                UnequipToInventory(this.Armor);
                this.Armor = item;
                item.Equip(this);
                SharedInventory.Remove(item);
            }
        }

        if (item.EquipmentType == EquipmentType.Accessory)
        {
            if (this.Accessory == null)
            {
                this.Accessory = item;
                item.Equip(this);
                SharedInventory.Remove(item);
            }
            else
            {
                UnequipToInventory(this.Accessory);
                this.Accessory = item;
                item.Equip(this);
                SharedInventory.Remove(item);
            }
        }
    }

    public void UnequipToInventory(EquippableItem item)
    {
        item.Unequip(this);
        item.RefreshUIFlag = true;
        SharedInventory.Add(item);
        
        if (item.EquipmentType == EquipmentType.Weapon)
        {
            this.Weapon = null;
        }

        if (item.EquipmentType == EquipmentType.Armor)
        {
            this.Armor = null;
        }

        if (item.EquipmentType == EquipmentType.Accessory)
        {
            this.Accessory = null;
        }
        
    }

    public void AddBaseStats(Unit u)
    {
        u.Health.addModifier(u.unitClass.Health);
        u.Mana.addModifier(u.unitClass.Mana);
        u.Strength.addModifier(u.unitClass.Strength);
        u.Intellect.addModifier(u.unitClass.Intellect);
        u.Dexterity.addModifier(u.unitClass.Dexterity);
    }
}
