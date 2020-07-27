using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// warrior class base stats and info
[CreateAssetMenu(menuName = "Class/Warrior")]
public class WarriorClass : BaseCharacterClass 
{
    public WarriorClass()
    {
        CharacterClassName = "Warrior";
        CharacterClassDescription = "Stronk boi";
        Strength = new characterStats(20);
        Speed = new characterStats(10);
        Intellect = new characterStats(5);
        Health = new characterStats(50);
        Mana = new characterStats(20);
        Dexterity = new characterStats(10);

        Movement = new characterStats(4);
        JumpHeight = new characterStats(1);

    }
}
