using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPanel : MonoBehaviour
{
    public GameObject CharacterButton;
    public GameObject characterSelectPanel;
    public GameObject CharacterPanel;
    // Start is called before the first frame update

    void Start()
    {
        List<Unit> party = GameObject.Find("PartySystem").GetComponent<PartySystem>().GetParty();
        for (int i = 0; i < party.Count; i++)
        {

            // generate a button for each character in the party
            GameObject characterButton = Instantiate(CharacterButton);
            characterButton.transform.SetParent(characterSelectPanel.transform, false);
            characterButton.transform.Find("Text").gameObject.GetComponent<Text>().text = party[i].Name;
            characterButton.GetComponent<CharacterButtonScript>().Character = party[i];

            // make character select panel invisible and character panel visible when you click on a character
            characterButton.GetComponent<Button>().onClick.AddListener(delegate { CharacterPanel.SetActive(true); });
            characterButton.GetComponent<Button>().onClick.AddListener(delegate { characterSelectPanel.SetActive(false); });

            //sets the selected character variable in CharacterPanel, which is used to display its stats
            characterButton.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("CharacterPanel").GetComponent<CharacterPanel>().SelectedCharacter = characterButton.GetComponent<CharacterButtonScript>().Character; });

            // Adding listeners that instantiate the selected unit's equipment
            characterButton.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("Weapon").GetComponent<EquipmentSlot>().item = characterButton.GetComponent<CharacterButtonScript>().Character.Weapon; });
            characterButton.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("Armor").GetComponent<EquipmentSlot>().item = characterButton.GetComponent<CharacterButtonScript>().Character.Armor; });
            characterButton.GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("Accessory").GetComponent<EquipmentSlot>().item = characterButton.GetComponent<CharacterButtonScript>().Character.Accessory; });
        }
    }


}
