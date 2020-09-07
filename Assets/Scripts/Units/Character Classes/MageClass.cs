using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Mage")]
public class MageClass : BaseCharacterClass
{
    public MageClass()
    {
        CharacterClassName = "Mage";
        CharacterClassDescription = "Smort boi";
        Strength = new StatModifier(5, this);
        Intellect = new StatModifier(20, this);
        Health = new StatModifier(30, this);
        Mana = new StatModifier(60, this);
        Dexterity = new StatModifier(10, this);

        Movement = new StatModifier(3, this);
        JumpHeight = new StatModifier(1, this);

        AbilitiesPlayer = new List<string>()
        {
            "Fireball",
            "ChainLightning"
        };
    }
}
