using UnityEngine;

public enum EquipmentType
{
    Helmet,
    Chest,
    Gloves,
    Boots,
    Weapon1,
    Weapon2,
    Accessory1,
    Accessory2,
}
[CreateAssetMenu]
public class EquippableItem : Item
{
    public int StrengthBonus;
    public int SpeedBonus;
    public int IntellectBonus;
    public int DexterityBonus;
    public int HealthBonus;
    public int ManaBonus;
    [Space]                                 // creates space between the variables in the unity inspector
    public float StrengthPercentBonus;
    public float SpeedPercentBonus;
    public float IntellectPercentBonus;
    public float DexterityPercentBonus;
    public float HealthPercentBonus;
    public float ManaPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(Character c)
    {
        if(HealthBonus != 0)
        {
            c.Health.addModifier(new StatModifier(HealthBonus, StatModType.Flat, this));
        }
        if (ManaBonus != 0)
        {
            c.Mana.addModifier(new StatModifier(ManaBonus, StatModType.Flat, this));
        }
        if (StrengthBonus != 0)
        {
            c.Strength.addModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        }
        if (SpeedBonus != 0)
        {
            c.Speed.addModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));
        }
        if (IntellectBonus != 0)
        {
            c.Intellect.addModifier(new StatModifier(IntellectBonus, StatModType.Flat, this));
        }
        if (DexterityBonus != 0)
        {
            c.Dexterity.addModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));
        }

        if (HealthPercentBonus != 0)
        {
            c.Health.addModifier(new StatModifier(HealthPercentBonus, StatModType.PercentMult, this));
        }

        if (ManaPercentBonus != 0)
        {
            c.Mana.addModifier(new StatModifier(ManaPercentBonus, StatModType.PercentMult, this));
        }

        if (StrengthPercentBonus != 0)
        {
            c.Strength.addModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        }

        if (SpeedPercentBonus != 0)
        {
            c.Speed.addModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
        }

        if (IntellectPercentBonus != 0)
        {
            c.Intellect.addModifier(new StatModifier(IntellectPercentBonus, StatModType.PercentMult, this));
        }

        if (DexterityPercentBonus != 0)
        {
            c.Dexterity.addModifier(new StatModifier(DexterityPercentBonus, StatModType.PercentMult, this));
        }
    }

    public void Unequip(Character c)
    {
        c.Health.removeAllModifiersFromSource(this);
        c.Mana.removeAllModifiersFromSource(this);
        c.Strength.removeAllModifiersFromSource(this);
        c.Speed.removeAllModifiersFromSource(this);
        c.Intellect.removeAllModifiersFromSource(this);
        c.Dexterity.removeAllModifiersFromSource(this);
    }
}
