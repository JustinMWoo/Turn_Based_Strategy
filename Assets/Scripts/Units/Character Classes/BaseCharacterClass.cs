
using Stats.CharacterStats;
using UnityEngine;

public class BaseCharacterClass : ScriptableObject
{
    private string characterClassName;
    private string characterClassDescription;

    // Stats
    private CharacterStats strength;
    private CharacterStats speed;
    private CharacterStats intellect;
    private CharacterStats health;
    private CharacterStats mana;
    private CharacterStats dexterity;

    private CharacterStats movement;
    private CharacterStats jumpHeight;


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

    public CharacterStats Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public CharacterStats Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public CharacterStats Intellect
    {
        get { return intellect; }
        set { intellect = value; }
    }

    public CharacterStats Health
    {
        get { return health; }
        set { health = value; }
    }

    public CharacterStats Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public CharacterStats Dexterity
    {
        get { return dexterity; }
        set { dexterity = value; }
    }

    public CharacterStats Movement
    {
        get { return movement; }
        set { movement = value; }
    }
    public CharacterStats JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }
}
