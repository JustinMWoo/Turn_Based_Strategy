
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStats
{
    public float BaseValue; // base value stats

    protected readonly List<StatModifier> statModifiers; // list containing stat modifiers. The readonly declaration just means that you cant change what the variable is pointing to after it is declared

    public readonly ReadOnlyCollection<StatModifier> StatModifiers; // readonly list that lets you view the stats. In this readonlycollection, you cannot change the values inside the list

    protected bool isDirty = true;    // indicates if we need to recalculate the value or not 

    protected float _value;           // holds the most recent calculation for final value

    protected float lastBaseValue = float.MinValue;   // stores the previous base values. Initially it is the min value (the smallest number possible in C#)

    // constructor that initializes the lists 
    public CharacterStats()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();  // this references the statModifiers list, but prohibits making changes to it. Also any changes to statModifiers will be reflected in StatModifiers
    }


    // constructor that calls the above one, and also takes the parameter baseValue to initialize BaseValue attribute
    public CharacterStats(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    // pushes stat modifier into the list
    public virtual void addModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
    }

    // removes a stat modifier from the list
    public virtual bool removeModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    // removes all modifiers from a certain source
    public virtual bool removeAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        // traverse the statmodifiers list backwards, and remove any modifiers of the given source
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                statModifiers.RemoveAt(i);
                didRemove = true;
            }
        }
        return didRemove;
    }

    //calculates final stat value using basevalue and stat modifiers
    protected virtual float calculateFinalValue()
    {
        float finalValue = BaseValue;   // final value starts as atleast the base value

        // adds each statModifier value to the final value (initially just the base value)

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            finalValue += mod.Value;
        }

        // round the final value to 4 decimal places
        return (float)Math.Round(finalValue, 4);
    }

    // get method for the final value
    public float Value
    {
        get
        {
            if (isDirty || BaseValue != lastBaseValue)                     // only calculate value when changes are made or if the base values changed
            {
                lastBaseValue = BaseValue;
                _value = calculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }
}

