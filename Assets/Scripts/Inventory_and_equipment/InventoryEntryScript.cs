using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryEntryScript : MonoBehaviour, IPointerClickHandler
{
    public EquippableItem Item;
    public CharacterPanel CharacterPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Item != null)
            {
                CharacterPanel.SelectedCharacter.EquipFromInventory(Item);
                this.transform.gameObject.SetActive(false);
            }
        }
    }
}
