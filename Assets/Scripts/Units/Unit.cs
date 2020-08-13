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

    public GameObject InventoryMenu;

    // for inventory opening/closing
    bool isActive = true;

    // Movement and jump height specified on classes

    void Start()
    {
        // for inventory opening/closing
        InventoryMenu = GameObject.Find("Character Panel");

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

        // open inventory with i
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(isActive == true)
            {
                InventoryMenu.SetActive(false);
                isActive = false;
            }
            else if(isActive == false){
                InventoryMenu.SetActive(true);
                isActive = true;
            }
        }
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
