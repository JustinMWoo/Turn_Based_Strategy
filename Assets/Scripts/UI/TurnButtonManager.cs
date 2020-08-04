using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButtonManager : MonoBehaviour
{
    public void NextAction()
    {
        // ONLY SHOULD WORK IF NOT ALREADY DOING AN ACTION AND IT IS PLAYERS TURN
        TurnManager.NextAction();
    }

    public void EndTurn()
    {

    }
}
