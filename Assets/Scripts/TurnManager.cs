using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    // String = team, List = members of team;
    static Dictionary<string, List<Unit>> units = new Dictionary<string, List<Unit>>();
    // Key for whose turn it is
    static Queue<string> turnKey = new Queue<string>();
    // Queue for whose turn it is on the team
    static Queue<Unit> turnTeam = new Queue<Unit>();


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
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
            PanToTarget(turnTeam.Peek().transform);

            // Debug.Log(turnTeam.Peek().actions.Count);
        }
    }

    public static void EndAction(bool done)
    {
        if (done)
        {
            EndTurn();
        }
        else
        {
            turnTeam.Peek().actions.Dequeue();
        }
        // Debug.Log(currAction);
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
            //Debug.Log("Ending Team Turn");
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
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

        if(turnKey.Count < 2)
        {
            // Check win or lose
        }
    }

    // Allow player to cycle through available actions
    public static void NextAction()
    {
        // Use unitAction.done before cycling to next action
        turnTeam.Peek().actions.Peek().Done();
        UnitActions temp = turnTeam.Peek().actions.Dequeue();
        turnTeam.Peek().actions.Enqueue(temp);
    }

    // Move this into CameraController?
    static void PanToTarget(Transform target)
    {
        CameraController.instance.panTarget = target;
    }
}
