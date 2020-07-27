using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // String = team, List = members of team;
    static Dictionary<string, List<Unit>> units = new Dictionary<string, List<Unit>>();
    // Key for whose turn it is
    static Queue<string> turnKey = new Queue<string>();
    // Queue for whose turn it is on the team
    static Queue<Unit> turnTeam = new Queue<Unit>();

    static int actionsRem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        // Get units from player whose turn it is
        List<Unit> teamList = units[turnKey.Peek()];

        foreach (Unit unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
        StartTurn();
    }

    // Start the turn for the next unit in the queue;
    public static void StartTurn()
    {
        if(turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();

            // Debug.Log(turnTeam.Peek().actions.Count);

            // Total actions available to the player
            actionsRem = turnTeam.Peek().actions.Count;
        }
    }
    public static void EndAction()
    {
        actionsRem -= 1;
        turnTeam.Peek().actions.Enqueue(turnTeam.Peek().actions.Dequeue());

        // Set the action to the next 
        turnTeam.Peek().currAction = turnTeam.Peek().actions.Peek();

        if (actionsRem < 1)
        {
            EndTurn();
        }
    }

    public static void EndTurn()
    {
        Unit unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0) // Start the next units turn
        {
            StartTurn();
        }
        else // End the teams turn
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }
    
    public static void AddUnit (Unit unit)
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

    }
}
