using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    static TurnData turnData = new TurnData();
    public static bool playerUnitTurnStart;
    static bool buttonPressed;

    // String = team, List = members of team;
    static Dictionary<string, List<Unit>> units = new Dictionary<string, List<Unit>>();
    // Key for whose turn it is
    static Queue<string> turnKey = new Queue<string>();
    // Queue for whose turn it is on the team
    static Queue<Unit> turnTeam = new Queue<Unit>();

    public static Unit currentUnit;


    // Start is called before the first frame update
    void Start()
    {
        SaveData.current.turn = turnData;
        GameEvents.current.OnLoadInitialized += OnLoadStart;
        GameEvents.current.OnLoadTurns += OnLoadTurns;
    }

    // Update is called once per frame
    void Update()
    {
        // Initialize the first team
        if (!GameEvents.current.Loading && turnTeam.Count == 0 && units.Count > 1)
        {
            // Make the player's turn first (Cannot save on opponents turn so this is okay when loading)
            while (turnKey.Peek() != "Player")
            {
                String temp = turnKey.Dequeue();
                turnKey.Enqueue(temp);

            }
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        // Debug.Log("Initializing team");
        // Get units from player whose turn it is
        List<Unit> teamList = units[turnKey.Peek()];

        foreach (Unit unit in teamList)
        {
            turnTeam.Enqueue(unit);
            turnData.unitsLeft.Add(unit.unitData.id);
        }
        StartTurn();
    }

    // Start the turn for the next unit in the queue;
    public static void StartTurn()
    {
        turnData.turnKeyData = turnKey;
        if (turnTeam.Count > 0)
        {
            currentUnit = turnTeam.Peek();
            //Debug.Log(turnTeam.Peek().unitData.id);
            currentUnit.BeginTurn();
            CameraController.instance.SetPanTarget(currentUnit.transform.position);
        }

        if (!currentUnit.npc)
        {
            playerUnitTurnStart = true;
            //Debug.Log(turnStart);
        }
    }

    public static void EndAction(bool done, bool updatePlayerUnitTurnStart)
    {
        // If cycling actions with button then dont change status of turn
        if (updatePlayerUnitTurnStart)
        {
            playerUnitTurnStart = false;
        }

        if (done)
        {
            EndTurn();
        }
        else
        {
            currentUnit.actions.Dequeue();
        }
    }

    public static void EndTurn()
    {
        Unit unit = turnTeam.Dequeue();
        unit.EndTurn();


        // Remove the unit from the save data and reduce its cooldowns if it is not called from the button being pressed
        if (!buttonPressed)
        {
            turnData.unitsLeft.Remove(unit.unitData.id);
            currentUnit.ReduceCooldowns();
        }
        //Debug.Log(turnData.unitsLeft.Count);

        if (turnTeam.Count > 0) // Start the next units turn
        {
            GameEvents.current.EndUnitTurn();
            StartTurn();
        }
        else // End the teams turn
        {
            //Debug.Log("Ending Team Turn");
            GameEvents.current.EndUnitTurn();
            GameEvents.current.EndTeamTurn();

            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            //Debug.Log("Ending " + team + " turn");
            //Debug.Log("Starting " + turnKey.Peek() + " turn");

            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(Unit unit)
    {
        List<Unit> list;

        // Make sure units tag has been added to the dictionary
        if (!units.ContainsKey(unit.tag))
        {
            // Add the new team
            list = new List<Unit>();
            units[unit.tag] = list;

            // Add the new team to the queue
            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            // Select the correct team that is assigned to the unit (via tag)
            list = units[unit.tag];
        }

        // Add the unit to the correct team
        list.Add(unit);
    }

    public static void RemoveUnit(Unit unit)
    {
        // Remove the unit from its team
        units[unit.tag].Remove(unit);

        // If there are no more units on the team
        if (!units[unit.tag].Any())
        {
            // Remove team from turnKey
            for (int i = 0; i < turnKey.Count; i++)
            {
                String team = turnKey.Dequeue();

                // If the team is not eliminated then re-add them to the queue
                if (!unit.CompareTag(team))
                    turnKey.Enqueue(team);
            }

            // Remove team from dictionary
            units.Remove(unit.tag);
        }

        if (turnKey.Count < 2)
        {
            // Check win or lose
        }
    }

    // Allow player to cycle through available actions
    public static void NextAction()
    {
        // Use unitAction.done before cycling to next action
        currentUnit.actions.Peek().Done();
        UnitActions temp = currentUnit.actions.Dequeue();
        currentUnit.actions.Enqueue(temp);
    }

    // Allow player to cycle through available units
    public static void NextUnit()
    {
        if (turnTeam.Count > 1)
        {
            buttonPressed = true;
            // End the units turn but re-add them to the queue after
            Unit temp = turnTeam.Peek();
            EndAction(true, false);
            turnTeam.Enqueue(temp);
            //Debug.Log(turnData.unitsLeft.Count);

            buttonPressed = false;
        }
    }

    private void OnLoadStart()
    {
        turnData = SaveData.current.turn;

        int count = turnData.turnKeyData.Count;
        for (int i = 0; i < count; i++)
        {
            string key = turnData.turnKeyData.Dequeue();
            turnKey.Enqueue(key);
            turnData.turnKeyData.Enqueue(key);
        }

        turnTeam.Clear();
    }
    private void OnLoadTurns()
    {
        // Add the units that still had their turn available
        Unit[] currUnits = FindObjectsOfType<Unit>();

        // Debug.Log("# of units: " + currUnits.Length);
        // Debug.Log(turnData.unitsLeft.Count);

        // Maybe add a "continue;" here 
        foreach (string unitID in turnData.unitsLeft)
        {
            foreach (Unit unit in currUnits)
            {
                if (unit.unitData.id.Equals(unitID))
                {
                    //Debug.Log("Adding Unit: " + unitID);
                    turnTeam.Enqueue(unit);
                }
            }
        }
        StartTurn();
    }

}
