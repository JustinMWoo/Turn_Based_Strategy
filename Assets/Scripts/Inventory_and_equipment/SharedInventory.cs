﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedInventory : MonoBehaviour
{
    public List<EquippableItem> equippables;
    public CharacterPanel CharacterPanel;
    public GameObject InventoryEntry;
    public GameObject Content;
    public GameObject Stat;

    private void Start()
    {
        for(int i=0; i < equippables.Count; i++)
        {
            EquippableItem itemEntry = equippables[i];
            if (itemEntry != null)
            {
                GameObject inventoryEntry = Instantiate(InventoryEntry);
                itemEntry.RefreshUIFlag = false;
                inventoryEntry.transform.SetParent(Content.transform, false);
                inventoryEntry.GetComponent<InventoryEntryScript>().Item = itemEntry;
                inventoryEntry.GetComponent<InventoryEntryScript>().CharacterPanel = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
                inventoryEntry.transform.Find("Name").gameObject.GetComponent<Text>().text = itemEntry.name;
                inventoryEntry.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = itemEntry.Icon;

                if (equippables[i].Damage != 0)
                {
                    GameObject damage = Instantiate(Stat);
                    damage.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    damage.GetComponent<Text>().text = "Damage + " + itemEntry.Damage.ToString();
                }

                if (equippables[i].HealthBonus != 0)
                {
                    GameObject healthBonus = Instantiate(Stat);
                    healthBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    healthBonus.GetComponent<Text>().text = "Health + " + itemEntry.HealthBonus.ToString();
                }

                if (equippables[i].ManaBonus != 0)
                {
                    GameObject manaBonus = Instantiate(Stat);
                    manaBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    manaBonus.GetComponent<Text>().text = "Mana + " + itemEntry.ManaBonus.ToString();
                }

                if (equippables[i].StrengthBonus != 0)
                {
                    GameObject strengthBonus = Instantiate(Stat);
                    strengthBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    strengthBonus.GetComponent<Text>().text = "Strength + " + itemEntry.StrengthBonus.ToString();
                }

                if (equippables[i].IntellectBonus != 0)
                {
                    GameObject intellectBonus = Instantiate(Stat);
                    intellectBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    intellectBonus.GetComponent<Text>().text = "Intellect + " + itemEntry.IntellectBonus.ToString();
                }

                if (equippables[i].DexterityBonus != 0)
                {
                    GameObject dexteritytBonus = Instantiate(Stat);
                    dexteritytBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    dexteritytBonus.GetComponent<Text>().text = "Dexterity + " + itemEntry.DexterityBonus.ToString();
                }
                if (equippables[i].DefenseBonus != 0)
                {
                    GameObject defenseBonus = Instantiate(Stat);
                    defenseBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    defenseBonus.GetComponent<Text>().text = "Defense + " + itemEntry.DefenseBonus.ToString();
                }
                if (equippables[i].MagicDefenseBonus != 0)
                {
                    GameObject magicDefenseBonus = Instantiate(Stat);
                    magicDefenseBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    magicDefenseBonus.GetComponent<Text>().text = "Magic Defense + " + itemEntry.MagicDefenseBonus.ToString();
                }
                if (equippables[i].CritChanceBonus != 0)
                {
                    GameObject critChanceBonus = Instantiate(Stat);
                    critChanceBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    critChanceBonus.GetComponent<Text>().text = "Crit % + " + itemEntry.CritChanceBonus.ToString();
                }
                if (equippables[i].DodgeChanceBonus != 0)
                {
                    GameObject dodgeChanceBonus = Instantiate(Stat);
                    dodgeChanceBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    dodgeChanceBonus.GetComponent<Text>().text = "Dodge % + " + itemEntry.DodgeChanceBonus.ToString();
                }
                if (equippables[i].MovementBonus != 0)
                {
                    GameObject movementBonus = Instantiate(Stat);
                    movementBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    movementBonus.GetComponent<Text>().text = "Movement + " + itemEntry.MovementBonus.ToString();
                }
                if (equippables[i].JumpHeightBonus != 0)
                {
                    GameObject jumpHeightBonus = Instantiate(Stat);
                    jumpHeightBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                    jumpHeightBonus.GetComponent<Text>().text = "Jump Height + " + itemEntry.JumpHeightBonus.ToString();
                }
            }
        }
        
    }

    public void UpdateUI()
    {
        for (int i = 0; i < equippables.Count; i++)
        {
            EquippableItem itemEntry = equippables[i];
            if (itemEntry != null)
            {
                if (itemEntry.RefreshUIFlag)
                {
                    GameObject inventoryEntry = Instantiate(InventoryEntry);
                    itemEntry.RefreshUIFlag = false;
                    inventoryEntry.transform.SetParent(Content.transform, false);
                    inventoryEntry.GetComponent<InventoryEntryScript>().Item = itemEntry;
                    inventoryEntry.GetComponent<InventoryEntryScript>().CharacterPanel = GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>();
                    inventoryEntry.transform.Find("Name").gameObject.GetComponent<Text>().text = itemEntry.name;
                    inventoryEntry.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = itemEntry.Icon;

                    if (equippables[i].Damage != 0)
                    {
                        GameObject damage = Instantiate(Stat);
                        damage.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        damage.GetComponent<Text>().text = "Damage + " + itemEntry.Damage.ToString();
                    }

                    if (equippables[i].HealthBonus != 0)
                    {
                        GameObject healthBonus = Instantiate(Stat);
                        healthBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        healthBonus.GetComponent<Text>().text = "Health + " + itemEntry.HealthBonus.ToString();
                    }

                    if (equippables[i].ManaBonus != 0)
                    {
                        GameObject manaBonus = Instantiate(Stat);
                        manaBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        manaBonus.GetComponent<Text>().text = "Mana + " + itemEntry.ManaBonus.ToString();
                    }

                    if (equippables[i].StrengthBonus != 0)
                    {
                        GameObject strengthBonus = Instantiate(Stat);
                        strengthBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        strengthBonus.GetComponent<Text>().text = "Strength + " + itemEntry.StrengthBonus.ToString();
                    }

                    if (equippables[i].IntellectBonus != 0)
                    {
                        GameObject intellectBonus = Instantiate(Stat);
                        intellectBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        intellectBonus.GetComponent<Text>().text = "Intellect + " + itemEntry.IntellectBonus.ToString();
                    }

                    if (equippables[i].DexterityBonus != 0)
                    {
                        GameObject dexteritytBonus = Instantiate(Stat);
                        dexteritytBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        dexteritytBonus.GetComponent<Text>().text = "Dexterity + " + itemEntry.DexterityBonus.ToString();
                    }
                    if (equippables[i].DefenseBonus != 0)
                    {
                        GameObject defenseBonus = Instantiate(Stat);
                        defenseBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        defenseBonus.GetComponent<Text>().text = "Defense + " + itemEntry.DefenseBonus.ToString();
                    }
                    if (equippables[i].MagicDefenseBonus != 0)
                    {
                        GameObject magicDefenseBonus = Instantiate(Stat);
                        magicDefenseBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        magicDefenseBonus.GetComponent<Text>().text = "Magic Defense + " + itemEntry.MagicDefenseBonus.ToString();
                    }
                    if (equippables[i].CritChanceBonus != 0)
                    {
                        GameObject critChanceBonus = Instantiate(Stat);
                        critChanceBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        critChanceBonus.GetComponent<Text>().text = "Crit % + " + itemEntry.CritChanceBonus.ToString();
                    }
                    if (equippables[i].DodgeChanceBonus != 0)
                    {
                        GameObject dodgeChanceBonus = Instantiate(Stat);
                        dodgeChanceBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        dodgeChanceBonus.GetComponent<Text>().text = "Dodge % + " + itemEntry.DodgeChanceBonus.ToString();
                    }
                    if (equippables[i].MovementBonus != 0)
                    {
                        GameObject movementBonus = Instantiate(Stat);
                        movementBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        movementBonus.GetComponent<Text>().text = "Dodge % + " + itemEntry.MovementBonus.ToString();
                    }
                    if (equippables[i].JumpHeightBonus != 0)
                    {
                        GameObject jumpHeightBonus = Instantiate(Stat);
                        jumpHeightBonus.transform.SetParent(inventoryEntry.transform.Find("Stats"), false);
                        jumpHeightBonus.GetComponent<Text>().text = "Dodge % + " + itemEntry.JumpHeightBonus.ToString();
                    }
                }
            }
        }
    }

    public void Add(EquippableItem item)
    {
        equippables.Add(item);
        UpdateUI();
    }
    public void Remove(EquippableItem item)
    {
        equippables.Remove(item);
        UpdateUI();
    }


}
