using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class PlayerStat
{
    public float BaseValue;

    protected bool isDirty = true;
    protected float lastBaseValue;

    protected float _value;
    public virtual float Value { 
        get {
            if (isDirty || BaseValue != lastBaseValue){
               lastBaseValue = BaseValue;
               _value = CalculateFinalValue();
               isDirty = false; 
            }
            return _value;
        }
    }

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public PlayerStat(){
        statModifiers = new List<StatModifier>();   
        StatModifiers = statModifiers.AsReadOnly();
    }
    public PlayerStat(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier mod){
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RmoverModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i++)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; //if (a.Order == b.Order)
    }

    protected virtual float CalculateFinalValue(){
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod =  statModifiers[i];

            if (mod.Type == StatModType.Flat){
                finalValue += mod.Value; /* <- if i use a flat number value for stats itll run thru here */
            }else if(mod.Type == StatModType.PercentAdd){
                sumPercentAdd += mod.Value;
                if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd){
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }else if (mod.Type == StatModType.PercentMult){
                finalValue *= 1 + mod.Value; /* <- if i use a percent for stat values itll run thru here */
            }
        }

        //12.0001f != 12f (doesnt round to the nearest whole number, its to the nearest 4 significant values)
        return(float)Math.Round(finalValue, 4);
    }
}