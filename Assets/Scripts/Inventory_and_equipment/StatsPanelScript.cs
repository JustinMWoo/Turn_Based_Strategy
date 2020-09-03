using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.CharacterStats;
using UnityEngine.UI;

public class StatsPanelScript : MonoBehaviour
{
    public CharacterPanel CharacterPanel;

    public void Update()
    {
        if (!CharacterPanel.SelectedCharacter.BaseStatsLoaded)
        {
            CharacterPanel.SelectedCharacter.AddBaseStats(CharacterPanel.SelectedCharacter);
            CharacterPanel.SelectedCharacter.BaseStatsLoaded = true;
        }

        GameObject.Find("Health").GetComponent<Text>().text = "Health: " + CharacterPanel.SelectedCharacter.Health.Value;
        GameObject.Find("Mana").GetComponent<Text>().text = "Mana: " + CharacterPanel.SelectedCharacter.Mana.Value;
        GameObject.Find("Strength").GetComponent<Text>().text = "Strength: " + CharacterPanel.SelectedCharacter.Strength.Value;
        GameObject.Find("Intellect").GetComponent<Text>().text = "Intellect: " + CharacterPanel.SelectedCharacter.Intellect.Value;
        GameObject.Find("Dexterity").GetComponent<Text>().text = "Dexterity: " + CharacterPanel.SelectedCharacter.Dexterity.Value;
        GameObject.Find("Damage").GetComponent<Text>().text = "Damage: " + CharacterPanel.SelectedCharacter.Damage.Value;
    }
}
