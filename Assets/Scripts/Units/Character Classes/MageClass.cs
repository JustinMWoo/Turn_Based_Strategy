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
        Strength = new characterStats(5);
        Speed = new characterStats(10);
        Intellect = new characterStats(20);
        Health = new characterStats(30);
        Mana = new characterStats(60);
        Dexterity = new characterStats(10);

        Movement = new characterStats(3);
        JumpHeight = new characterStats(1);

        AbilitiesPlayer = new List<string>()
        {
            "Fireball"
        };
    }
}
