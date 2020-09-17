﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public enum EquipmentType
{
    Armor,
    Weapon,
    Accessory,
}

public enum WeaponScaling
{
    NA,
    Str,
    Int,
    Dex
}

[CreateAssetMenu]
public class EquippableItem : Item
{

    public int Damage;
    public int StrengthBonus;
    public int IntellectBonus;
    public int DexterityBonus;
    public int HealthBonus;
    public int ManaBonus;
    public int DefenseBonus;
    public int MagicDefenseBonus;
    public int CritChanceBonus;
    public int DodgeChanceBonus;
    public int MovementBonus;
    public int JumpHeightBonus;
    [Space]                                 // creates space between the variables in the unity inspector

    [HideInInspector]
    public EquipmentType EquipmentType;
    public bool RefreshUIFlag;


    [HideInInspector]
    public WeaponScaling WeaponScaling;
    [HideInInspector]
    public int WeaponRange;
    [HideInInspector]
    public int WeaponVerticality;



    public void Equip(Unit c)
    {
        if(HealthBonus != 0)
        {
            c.Health.addModifier(new StatModifier(HealthBonus, this));
        }
        if (ManaBonus != 0)
        {
            c.Mana.addModifier(new StatModifier(ManaBonus, this));
        }
        if (StrengthBonus != 0)
        {
            c.Strength.addModifier(new StatModifier(StrengthBonus, this));
        }

        if (IntellectBonus != 0)
        {
            c.Intellect.addModifier(new StatModifier(IntellectBonus, this));
        }
        if (DexterityBonus != 0)
        {
            c.Dexterity.addModifier(new StatModifier(DexterityBonus, this));
        }
        if (Damage != 0)
        {
            c.Damage.addModifier(new StatModifier(Damage, this));
        }
        if (DefenseBonus != 0)
        {
            c.Defense.addModifier(new StatModifier(DefenseBonus, this));
        }
        if (MagicDefenseBonus != 0)
        {
            c.MagicDefense.addModifier(new StatModifier(MagicDefenseBonus, this));
        }

        if (CritChanceBonus != 0)
        {
            c.CritChance.addModifier(new StatModifier(CritChanceBonus, this));
        }
        if (DodgeChanceBonus != 0)
        {
            c.DodgeChance.addModifier(new StatModifier(DodgeChanceBonus, this));
        }
        if (MovementBonus != 0)
        {
            c.Movement.addModifier(new StatModifier(MovementBonus, this));
        }
        if (JumpHeightBonus != 0)
        {
            c.JumpHeight.addModifier(new StatModifier(JumpHeightBonus, this));
        }
    }

    public void Unequip(Unit c)
    {
        c.Health.removeAllModifiersFromSource(this);
        c.Mana.removeAllModifiersFromSource(this);
        c.Strength.removeAllModifiersFromSource(this);
        c.Intellect.removeAllModifiersFromSource(this);
        c.Dexterity.removeAllModifiersFromSource(this);
        c.Damage.removeAllModifiersFromSource(this);
        c.Defense.removeAllModifiersFromSource(this);
        c.MagicDefense.removeAllModifiersFromSource(this);
        c.CritChance.removeAllModifiersFromSource(this);
        c.DodgeChance.removeAllModifiersFromSource(this);
        c.Movement.removeAllModifiersFromSource(this);
        c.JumpHeight.removeAllModifiersFromSource(this);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EquippableItem))]
public class EquippableItem_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EquippableItem script = (EquippableItem)target;

        script.EquipmentType = (EquipmentType)EditorGUILayout.EnumPopup("Equipment Type", script.EquipmentType);
        if (script.EquipmentType == EquipmentType.Weapon)
        {
            script.WeaponScaling = (WeaponScaling)EditorGUILayout.EnumPopup("Weapon Scaling", script.WeaponScaling);
            script.WeaponRange = EditorGUILayout.IntField("Weapon Range", script.WeaponRange);
            script.WeaponVerticality = EditorGUILayout.IntField("Weapon Verticality", script.WeaponVerticality);
        }

    }
}
#endif