
using System.Diagnostics;

// stores different stat types. 
public enum StatModType
{
    Flat = 100,                     // using 100, 200 and 300 as base values, because if we want to add another stat type in between 2 types, then we can assign a value for it that is between the 2 types
    PercentAdd = 200,               // example: If i want to add a new type between Flat and PercentAdd, I would assign it a value between 101 and 199;
    PercentMult = 300,
}


public class StatModifier 
{
    public readonly float Value; // value of the stat modifier
    public readonly StatModType Type;   // the type of the modifier, percent/flat
    public readonly int Order;          // identifies the order in which to apply modifiers
    public readonly object Source;      // source of the modifier

    // constructor 
    public StatModifier(float value, StatModType type, int order, object source)
    {
        Value = value;
        Type = type;
        Order = order;
        source = Source;
    }

    // below of constructors that take less parameters than above contructor, and then defaults values the missing parameters
    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }   // the contructor automatically calls the first constructor and casts the type parameter as an int for the order parameter

    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
