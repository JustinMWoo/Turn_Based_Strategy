using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterClass : ScriptableObject
{
    private string characterClassName;
    private string characterClassDescription;

    // Stats
<<<<<<< HEAD
    private StatModifier strength;
    private StatModifier intellect;
    private StatModifier health;
    private StatModifier mana;
    private StatModifier dexterity;

    private StatModifier movement;
    private StatModifier jumpHeight;

    // can use get to read, or set to write value of names/stats
    public string CharacterClassName
    {
        get { return characterClassName; }
        set { characterClassName = value; }
    }

    public string CharacterClassDescription
    {
        get { return characterClassDescription; }
        set { characterClassDescription = value; }
    }

    public StatModifier Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public StatModifier Intellect
    {
        get { return intellect; }
        set { intellect = value; }
    }

    public StatModifier Health
    {
        get { return health; }
        set { health = value; }
    }

    public StatModifier Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public StatModifier Dexterity
    {
        get { return dexterity; }
        set { dexterity = value; }
    }

    public StatModifier Movement
    {
        get { return movement; }
        set { movement = value; }
    }

    public StatModifier JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }

    public List<string> AbilitiesPlayer
    {
        get { return abilitiesPlayer; }
        set { abilitiesPlayer = value; }
    }
    public List<string> AbilitiesNPC
    {
        get { return abilitiesNPC; }
        set { abilitiesNPC = value; }
    }
}
