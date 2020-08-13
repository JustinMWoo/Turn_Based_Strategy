using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    // Display available abilities, set TurnManager.currentUnit.usingSkill to true then use similar code as SaveManager to create buttons for each ability 
    // and set current skill on unit to the one selected

    public Transform abilityArea;
    public GameObject abilityPanel;
    public GameObject abilityButtonPrefab;
    public GameObject cooldownPrefab;

    bool abilityPanelActive = false;
    private void Start()
    {
        GameEvents.current.OnEndUnitTurn+=DeactivateAbilityPanel;
    }
    public void ShowAvailableAbilities()
    {
        Unit currentUnit = TurnManager.currentUnit;
        if (!currentUnit.npc && currentUnit.AvailableAbilitites.Count>0)
        {
            if (!abilityPanelActive)
            {
                abilityPanelActive = true;

                // End the current action
                currentUnit.actions.Peek().Done();

                // Remove existing buttons
                foreach (Transform button in abilityArea)
                {
                    Destroy(button.gameObject);
                }

                // Generate buttons for each skill on the unit
                for (int i = 0; i < currentUnit.AvailableAbilitites.Count; i++)
                {
                    GameObject buttonObject = Instantiate(abilityButtonPrefab);
                    buttonObject.transform.SetParent(abilityArea.transform, false);

                    // Add onClick actions for each button
                    var index = i;
                    buttonObject.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        currentUnit.currentAbility = currentUnit.AvailableAbilitites[index];
                    });

                    // Change button text
                    buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = currentUnit.currentAbility.id;

                    // Greyout button if on cooldown
                    if (currentUnit.abilityCooldowns[currentUnit.AvailableAbilitites[index].id] != 0){
                        GameObject cooldownObject = Instantiate(cooldownPrefab);
                        cooldownObject.transform.SetParent(buttonObject.transform, false);
                        cooldownObject.GetComponentInChildren<TextMeshProUGUI>().text = currentUnit.abilityCooldowns[currentUnit.AvailableAbilitites[index].id].ToString();
                    }
                }
                abilityPanel.SetActive(true);
                currentUnit.usingAbility = true;
            }
            else
            {
                // Turn off panel
                DeactivateAbilityPanel();
                currentUnit.usingAbility = false;
                currentUnit.currentAbility.Done();
            }
        }
    }

    public void DeactivateAbilityPanel()
    {
        Unit currentUnit = TurnManager.currentUnit;
        currentUnit.usingAbility = false;
        abilityPanelActive = false;
        abilityPanel.SetActive(false);
    }
}
