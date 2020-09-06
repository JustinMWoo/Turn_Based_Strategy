using UnityEngine;


public class CharacterPanel : MonoBehaviour
{
    public GameObject CharacterSelectPanel;
    public EquipmentSlot Weapon;
    public EquipmentSlot Armor;
    public EquipmentSlot Accessory;
    public SharedInventory SharedInventory;
    
    public Unit SelectedCharacter;



    void Update()
    {
        // open inventory with i
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CharacterSelectPanel.SetActive(true);
            GameObject.Find("CharacterPanel").SetActive(false);
        }
    }
}
