using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool turn = false; // True when it is this units turn
    public Queue<UnitActions> actions = new Queue<UnitActions>();
    [NonSerialized]
    public UnitActions currAction;

    public BaseCharacterClass unitClass;
    

    public Inventory inventory;
    

    // Movement and jump height specified on classes


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
