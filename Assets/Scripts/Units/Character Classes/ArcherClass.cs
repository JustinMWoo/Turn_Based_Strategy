﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Archer")]
public class ArcherClass : BaseCharacterClass
{
    public ArcherClass()
    {
        CharacterClassName = "Archer";
        CharacterClassDescription = "shoot boi";
        Strength = new characterStats(20);
        Speed = new characterStats(10);
        Intellect = new characterStats(5);
        Health = new characterStats(50);
        Mana = new characterStats(20);
        Dexterity = new characterStats(10);

        Movement = new characterStats(4);
        JumpHeight = new characterStats(2);
    }
}