
using UnityEngine;

public class BaseCharacterClass : MonoBehaviour
{
    private string CharacterClassName;
    private string CharacterClassDescription;

    // Stats
    private characterStats Strength;
    private characterStats Speed;
    private characterStats Intellect;
    private characterStats Health;
    private characterStats Mana;
    private characterStats Dexterity;


    // can use get to read, or set to write value of names/stats
    public string characterClassName
    {
        get { return CharacterClassName; }
        set { CharacterClassName = value; }
    }

    public string characterClassDescription
    {
        get { return CharacterClassDescription; }
        set { CharacterClassDescription = value; }
    }

    public characterStats strength
    {
        get { return Strength; }
        set { Strength = value; }
    }

    public characterStats speed
    {
        get { return Speed; }
        set { Speed = value; }
    }

    public characterStats intellect
    {
        get { return Intellect; }
        set { Intellect = value; }
    }

    public characterStats health
    {
        get { return Health; }
        set { Health = value; }
    }

    public characterStats mana
    {
        get { return Mana; }
        set { Mana = value; }
    }

    public characterStats dexterity
    {
        get { return Dexterity; }
        set { Dexterity = value; }
    }
}
