﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherClass : BaseCharacterClass
{
    public void archerClass()
    {
        characterClassName = "Archer";
        characterClassDescription = "shoot boi";
        strength = new characterStats(20);
        speed = new characterStats(10);
        intellect = new characterStats(5);
        health = new characterStats(50);
        mana = new characterStats(20);
        dexterity = new characterStats(10);
    }
}