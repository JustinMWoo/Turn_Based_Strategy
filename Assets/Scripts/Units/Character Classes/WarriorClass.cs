using UnityEngine;
﻿using System.Collections;
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
        Strength = new StatModifier(30, this);
        Intellect = new StatModifier(5, this);
        Health = new StatModifier(80, this);
        Mana = new StatModifier(20, this);
        Dexterity = new StatModifier(5, this);

        Movement = new StatModifier(4, this);
        JumpHeight = new StatModifier(1, this);
    }
}
