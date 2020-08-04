
using UnityEngine;

public class BaseCharacterClass : ScriptableObject
{
    private string characterClassName;
    private string characterClassDescription;

    // Stats
    private characterStats strength;
    private characterStats speed;
    private characterStats intellect;
    private characterStats health;
    private characterStats mana;
    private characterStats dexterity;

    private characterStats movement;
    private characterStats jumpHeight;


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

    public characterStats Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public characterStats Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public characterStats Intellect
    {
        get { return intellect; }
        set { intellect = value; }
    }

    public characterStats Health
    {
        get { return health; }
        set { health = value; }
    }

    public characterStats Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public characterStats Dexterity
    {
        get { return dexterity; }
        set { dexterity = value; }
    }

    public characterStats Movement
    {
        get { return movement; }
        set { movement = value; }
    }
    public characterStats JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }
}
