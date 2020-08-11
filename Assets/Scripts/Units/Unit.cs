using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Variables
    // Save Info
    public UnitData unitData;
    public bool npc;
    public int unitType;

    public bool turn = false; // True when it is this units turn
    public bool turnDone = false;

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

    #endregion
    private void Awake()
    {
        if (npc)
        {
            gameObject.AddComponent<NPCMove>();
            //gameObject.AddComponent<NPCAttack>();
            gameObject.AddComponent<NPCFaceDir>();
        }
        else
        {
            gameObject.AddComponent<PlayerMove>();
            gameObject.AddComponent<PlayerAttack>();
            gameObject.AddComponent<PlayerFaceDir>();
        }
    }
    void Start()
    {
        // For save/load
        if (string.IsNullOrEmpty(unitData.id))
        {
            unitData.id = transform.position.sqrMagnitude + name + transform.GetSiblingIndex();
            SaveData.current.units.Add(unitData);
            //Debug.Log("ID for " + name + " is " + unitData.id);
        }
        unitData.npc = npc;
        unitData.unitType = unitType;
        GameEvents.current.onLoadInitialized += DestroyMe;

        // Add unit to turn order (static so dont need instance of turn manager)
        TurnManager.AddUnit(this);

        // If unit has not taken damage when loading or when creating new unit
        if ((unitData.health ?? 0) == 0)
        {
            // HP IN CLASSES SHOULD PROBABLY BE INT NOT FLOAT
            currentHP =(int)unitClass.Health.Value;
        }
        else
        {
            currentHP = (int)unitData.health;
        }
        maxHP = (int)unitClass.Health.Value;

        healthbar.SetMaxHealth(maxHP);
        healthbar.SetHealth(currentHP);
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

        unitData.position = transform.position;
        unitData.rotation = transform.rotation;
    }

    public void BeginTurn()
    {
        turn = true;
        //Debug.Log(unitData.id + " Turn " + turn);

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

        // Update save hp
        unitData.health = currentHP;
    }

    // Same as DestroyMe but with death animation (maybe condense into just 1 function with an if statement
    public void Die()
    {
        // Death animation

        GameEvents.current.onLoadInitialized -= DestroyMe;

        SaveData.current.units.Remove(unitData);
        // Remove from turn order
        TurnManager.RemoveUnit(this);

        // Remove unit
        Destroy(gameObject);
    }

    void DestroyMe()
    {
        GameEvents.current.onLoadInitialized -= DestroyMe;
        SaveData.current.units.Remove(unitData);
        TurnManager.RemoveUnit(this);
        Destroy(gameObject);
    }
}

