using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool turn = false; // True when it is this units turn

    //public Queue<UnitActions> actions = new Queue<UnitActions>();

    public Queue<UnitActions> actions = new Queue<UnitActions>();

    public BaseCharacterClass unitClass;
    

    public Inventory inventory;
    

    // Movement and jump height specified on classes

    // Change these into values of equipped weapon etc.
    public int weaponRange = 5;
    public int weaponVerticality = 1;
    public int weaponDamage = 50;

    public int maxHP;
    public int currentHP;

    public HealthBar healthbar;


    void Start()
    {
        // Add unit to turn order (static so dont need instance of turn manager)
        TurnManager.AddUnit(this);
        

        // HP IN CLASSES SHOULD PROBABLY BE INT NOT FLOAT
        currentHP = maxHP = (int) unitClass.Health.Value;
        healthbar.SetMaxHealth(maxHP);
        healthbar.gameObject.SetActive(false);
        
    }

    void Update()
    {
        if (turn)
        {
            //Debug.Log(TurnManager.GetCurrentAction());

            actions.Peek().Execute();
        }
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void BeginTurn()
    {
        turn = true;

        UnitActions move = GetComponent<TacticsMove>();
        UnitActions attack = GetComponent<TacticsAttack>();
        UnitActions faceDir = GetComponent<TacticsFaceDir>();

        if (move != null)
            actions.Enqueue(move);

        if (attack != null)
            actions.Enqueue(attack);

        if (faceDir != null)
            actions.Enqueue(faceDir);
    }

    public void EndTurn()
    {
        turn = false;
        actions.Clear();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        healthbar.SetHealth(currentHP);
    }

    public void Die()
    {
        // Death animation

        // Remove from turn order
        TurnManager.RemoveUnit(this);

        // Remove unit
        Destroy(gameObject);
    }
}

