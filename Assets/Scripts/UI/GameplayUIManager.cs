using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    public HealthBar healthbar;
    public GameObject healthCanvas;

    private void Start()
    {
        // Initialize stats panel with the current stat types (str, dex, etc.)
    }

    private void Update()
    {
        ShowUnitInfo();
    }

    public void ShowUnitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // If not in the attack animation
        if (!CameraController.instance.attackCameraActive && Physics.Raycast(ray, out hit))
        {
            // Check if unit is hovered
            Unit targetUnit = hit.collider.GetComponent<Unit>();
            if (targetUnit != null)
            {
                // Update top health bar
                healthCanvas.SetActive(true);
                healthbar.SetMaxHealth(targetUnit.maxHP);
                healthbar.SetHealth(targetUnit.currentHP);

                // Update the values of the stats panel with hovered units current stats
            }
            else
            {
                healthCanvas.SetActive(false);
            }
        }
        else
        {
            healthCanvas.SetActive(false);
        }

        // If unit is controlled by the player  
        if (!TurnManager.currentUnit.npc)
        {
            // If unit is on the attack action then show damage values on health bar
            if (TurnManager.currentUnit.actions.Peek() is TacticsAttack)
            {

            }
            // If on ability show calculated damage for ability instead
            if (TurnManager.currentUnit.usingAbility)
            {

            }
        }

    }

    public void NextAction()
    {
        // ONLY SHOULD WORK IF NOT ALREADY DOING AN ACTION AND IT IS PLAYERS TURN
        if (!TurnManager.currentUnit.npc)
        {
            TurnManager.NextAction();
        }
    }

    public void EndTurn()
    {
        // ONLY SHOULD WORK IF NOT ALREADY DOING AN ACTION AND IT IS PLAYERS TURN
        // MUST ALSO CALL DONE FOR THE UNIT
        TurnManager.EndAction(true, true);
    }

    public void NextUnit()
    {
        if (TurnManager.playerUnitTurnStart)
        {
            TurnManager.NextUnit();
        }
    }
}
