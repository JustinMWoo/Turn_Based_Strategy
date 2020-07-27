﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool turn = false; // True when it is this units turn
    public Queue<UnitActions> actions = new Queue<UnitActions>();
    public UnitActions currAction;

    // Movement and jump height specified on classes
    public int move = 5;
    public float jumpHeight = 2;


    void Start()
    {
        UnitActions[] acts = GetComponents<UnitActions>();
        foreach (UnitActions action in acts)
        {
            actions.Enqueue(action);
        }

        currAction = actions.Peek();

        // Add unit to turn order (static so dont need instance of turn manager)
        TurnManager.AddUnit(this);
    }

    void Update()
    {
        currAction.Execute();
    }

    public void BeginTurn()
    {
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }
}
