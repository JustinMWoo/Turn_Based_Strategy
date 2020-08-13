using UnityEngine;
using Stats.CharacterStats;
public class Character : MonoBehaviour
{

    public CharacterStats Health;
    public CharacterStats Mana;
    public CharacterStats Strength;
    public CharacterStats Speed;
    public CharacterStats Intellect;
    public CharacterStats Dexterity;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;

    private void Awake()
    {
        statPanel.SetStats(Health, Mana, Strength, Speed, Intellect, Dexterity);
        statPanel.UpdateStatValues();

        inventory.OnItemRightClickedEvent += EquipFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }

    // checks if an item is equippable and if so then equips it
    private void EquipFromInventory(Item item)
    {
        if(item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnequipFromEquipPanel(Item item)
    {
        if(item is EquippableItem)
        {
            Unequip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        // remove item from inventory
        if (inventory.RemoveItem(item))
        {
            // add the item to the equipment panel
            EquippableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                // if there was a previously equipped item, then add it back to the inventory
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);        // if cannot equip item for some reason, add it back to inventory
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        // if the inventory is not full, add to inventory
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}
