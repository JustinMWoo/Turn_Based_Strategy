using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    public HealthBar healthbar;
    public GameObject healthUI;

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
                // If unit is controlled by the player  
                // If unit is on the attack action then show damage values on health bar
                if (!TurnManager.currentUnit.npc && TurnManager.currentUnit.actions.Peek() is TacticsAttack)
                {
                    healthUI.SetActive(true);
                    healthbar.SetMaxHealth(targetUnit.maxHP);
                    RaycastHit selectableTile;
                    if(Physics.Raycast(targetUnit.transform.position, Vector3.down, out selectableTile, 1f))
                    {
                        Tile tile = selectableTile.collider.GetComponent<Tile>();
                        if (tile!=null && tile.selectable)
                        {
                            int damage = DamageCalculator.Current.CalculateDamage(TurnManager.currentUnit, targetUnit, DamageType.Physical, false, null);
                            healthbar.SetDamagedHealth(targetUnit.currentHP, damage);
                        }
                        else
                        {
                            // TODO: remove repeated code?
                            healthbar.SetHealth(targetUnit.currentHP);
                        }
                    }
                    else
                    {
                        healthbar.SetHealth(targetUnit.currentHP);
                    } 
                }
                // If on ability show calculated damage for ability instead
                else if (!TurnManager.currentUnit.npc && TurnManager.currentUnit.usingAbility)
                {
                    healthUI.SetActive(true);
                    healthbar.SetMaxHealth(targetUnit.maxHP);
                }
                else // Not the players turn
                {
                    // Update top health bar
                    healthUI.SetActive(true);
                    healthbar.SetMaxHealth(targetUnit.maxHP);
                    healthbar.SetHealth(targetUnit.currentHP);

                    // TODO: Update the values of the stats panel with hovered units current stats
                }
            }
            else
            {
                healthUI.SetActive(false);
            }
        }
        else
        {
            healthUI.SetActive(false);
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
