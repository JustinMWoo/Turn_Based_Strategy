using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButtonManager : MonoBehaviour
{
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
        if(TurnManager.playerUnitTurnStart)
        {
            TurnManager.NextUnit();
        }
    }
}
