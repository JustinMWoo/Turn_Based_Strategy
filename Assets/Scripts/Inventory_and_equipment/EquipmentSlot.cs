
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public EquippableItem item;
    public CharacterPanel CharacterPanel;

    private void Update()
    {

        // sets the item based on which slot it is supposed to be
        if(this.name == "Weapon")
        {
            item = CharacterPanel.SelectedCharacter.Weapon;
        }

        else if (this.name == "Armor")
        {
            item = CharacterPanel.SelectedCharacter.Armor;
        }

        else if (this.name == "Accessory")
        {
            item = CharacterPanel.SelectedCharacter.Accessory;
        }

        if (item != null)
        {
            if(item.EquipmentType == EquipmentType.Weapon)
            {
                if(CharacterPanel.SelectedCharacter.Weapon != null)
                {
                    this.GetComponentInParent<Image>().sprite = CharacterPanel.SelectedCharacter.Weapon.Icon;
                }
            }

            if (item.EquipmentType == EquipmentType.Armor)
            {
                if(CharacterPanel.SelectedCharacter.Armor != null)
                {
                    this.GetComponentInParent<Image>().sprite = CharacterPanel.SelectedCharacter.Armor.Icon;
                }
            }

            if (item.EquipmentType == EquipmentType.Accessory)
            {
                if(CharacterPanel.SelectedCharacter.Accessory!= null)
                {
                    this.GetComponentInParent<Image>().sprite = CharacterPanel.SelectedCharacter.Accessory.Icon;
                }
            }
        }

        else
        {
            this.GetComponentInParent<Image>().sprite = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if(item != null)
            {
                CharacterPanel.SelectedCharacter.UnequipToInventory(item);
                item = null;
            }
        }
    }
}
