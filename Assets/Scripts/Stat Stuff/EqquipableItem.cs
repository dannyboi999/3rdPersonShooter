using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Accessory1,
    Accessory2,
}

public class EqquipableItem : MonoBehaviour
{
    public int StrengthBonus;
    public int AgilityBonus;
    public int VitalityBonus;
    [Space]
    public float StrengthPercentBonus;
    public float AgilityPercentBonus;
    public float VitalityPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(Character character)
    {
        if (StrengthBonus != 0)
            character.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            character.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        if (VitalityBonus != 0)
            character.Vitality.AddModifier(new StatModifier(VitalityBonus,StatModType.Flat, this));
        
        if (StrengthPercentBonus != 0)
            character.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            character.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        if (VitalityPercentBonus != 0)
            character.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(Character character)
    {
        character.Strength.RemoveAllModifiersFromSource(this);
        character.Agility.RemoveAllModifiersFromSource(this);
        character.Vitality.RemoveAllModifiersFromSource(this);
    }
}
