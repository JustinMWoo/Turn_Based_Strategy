using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Variables
    // Save Info
    public UnitData unitData;
    public bool npc;
    public int unitType;

    public bool turn = false; // True when it is this units turn
    //public bool turnDone = false;
    public bool usingAbility = false;

    public List<TacticsAbility> AvailableAbilitites = new List<TacticsAbility>();
    public TacticsAbility currentAbility;

    public Queue<UnitActions> actions = new Queue<UnitActions>();

    public BaseCharacterClass unitClass;

    public Inventory inventory;

    public Dictionary<string, int> abilityCooldowns = new Dictionary<string, int>();


    // Movement and jump height specified on classes

    // Change these into values of equipped weapon etc.
    public int weaponRange = 5;
    public int weaponVerticality = 1;
    public int weaponDamage = 50;

    public int maxHP;
    public int currentHP;
    public int level;

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
            gameObject.AddComponent<InteractAction>();
            gameObject.AddComponent<PlayerFaceDir>();

            foreach (string ability in unitClass.AbilitiesPlayer)
            {
                AddAbility(ability);
            }
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
        GameEvents.current.OnLoadInitialized += DestroyMe;

        // Add unit to turn order (static so dont need instance of turn manager)
        TurnManager.AddUnit(this);

        // If unit has not taken damage when loading or when creating new unit
        if ((unitData.health ?? 0) == 0)
        {
            // HP IN CLASSES SHOULD PROBABLY BE INT NOT FLOAT
            currentHP = (int)unitClass.Health.Value;
        }
        else
        {
            currentHP = (int)unitData.health;
        }
        maxHP = (int)unitClass.Health.Value;

        // Create ability cooldown dictionary
        if (unitData.abilityCooldowns == null)
        {
            TacticsAbility[] abilitites = GetComponents<TacticsAbility>();
            foreach (TacticsAbility ability in abilitites)
            {
                abilityCooldowns.Add(ability.id, 0);
            }
        }
        else
        {
            abilityCooldowns = unitData.abilityCooldowns;
        }
        unitData.abilityCooldowns = abilityCooldowns;

        healthbar.SetMaxHealth(maxHP);
        healthbar.SetHealth(currentHP);
        healthbar.gameObject.SetActive(false);
    }

    void Update()
    {
        if (turn && usingAbility)
        {
            if (abilityCooldowns[currentAbility.id] == 0)
            {
                currentAbility.Execute();
            }
        }
        else if (turn)
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

        UnitActions[] availableActions = GetComponents<UnitActions>();
        foreach (UnitActions action in availableActions)
        {
            if (!(action is TacticsAbility))
            {
                actions.Enqueue(action);
            }
        }

        //UnitActions move = GetComponent<TacticsMove>();
        //UnitActions attack = GetComponent<TacticsAttack>();
        //UnitActions faceDir = GetComponent<TacticsFaceDir>();

        //if (move != null)
        //    actions.Enqueue(move);

        //if (attack != null)
        //    actions.Enqueue(attack);

        //if (faceDir != null)
        //    actions.Enqueue(faceDir);


        // Initialize abilities
        TacticsAbility[] abilities = GetComponents<TacticsAbility>();

        foreach (TacticsAbility ability in abilities)
        {
            // Check for cooldown here too or on the ability itself (so ability still gets put on list but it shows that its on cooldown)
            // Only add class abilities if the unit can access
            if (ability.levelRequirement <= level)
            {
                AvailableAbilitites.Add(ability);
            }
        }
        if (AvailableAbilitites.Count > 0)
        {
            currentAbility = AvailableAbilitites[0];
        }
    }

    public void EndTurn()
    {
        turn = false;
        actions.Clear();
        AvailableAbilitites.Clear();
        currentAbility = null;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        healthbar.SetHealth(currentHP);

        // Update save hp
        unitData.health = currentHP;
    }

    public void ReduceCooldowns()
    {
        // Debug.Log("Reducing cooldowns");
        foreach (string ability in abilityCooldowns.Keys.ToList())
        {
            if (abilityCooldowns[ability] != 0)
            {
                abilityCooldowns[ability]--;
            }
        }
    }

    void AddAbility(string id)
    {
        string tag;
        if (npc)
        {
            tag = "NPC";
        }
        else
        {
            tag = "Player";
        }
        //Debug.Log("Adding Ability: " + tag + id);
        gameObject.AddComponent(Type.GetType(tag + id));
    }

    // Same as DestroyMe but with death animation (maybe condense into just 1 function with an if statement
    public void Die()
    {
        // Death animation

        GameEvents.current.OnLoadInitialized -= DestroyMe;

        SaveData.current.units.Remove(unitData);
        // Remove from turn order
        TurnManager.RemoveUnit(this);

        // Remove unit
        Destroy(gameObject);
    }

    void DestroyMe()
    {
        GameEvents.current.OnLoadInitialized -= DestroyMe;
        SaveData.current.units.Remove(unitData);
        TurnManager.RemoveUnit(this);
        Destroy(gameObject);
    }
}

