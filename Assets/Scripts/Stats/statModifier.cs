
public class StatModifier 
{
    public readonly float Value; // value of the stat modifier
    public readonly object Source;      // source of the modifier

    // constructor 
    public StatModifier(float value, object source)
    {
        Value = value;
        Source = source;
    }
}
