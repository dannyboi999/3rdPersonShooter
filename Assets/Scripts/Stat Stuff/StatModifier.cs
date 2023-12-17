public enum StatModType {
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatModifier 
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;
    public readonly object Source;

    public StatModifier(float value, StatModType type, int order, object source){
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    public StatModifier(float value, StatModType type) : this (value, type, (int)type, null) { }// doesnt need the order or source 

    public StatModifier(float value, StatModType type, int order) : this (value, type, order, null) { }//doesnt need source but needs object 

    public StatModifier(float value, StatModType type, object source) : this (value, type, (int)type, source) { }//doesnt need object but needs source 
}
