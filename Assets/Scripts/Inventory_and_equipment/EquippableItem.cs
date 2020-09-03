using UnityEngine;

public enum EquipmentType
{
    Armor,
    Weapon,
    Accessory,
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
    [Space]                                 // creates space between the variables in the unity inspector

    public EquipmentType EquipmentType;

    public bool RefreshUIFlag;
    
    public EquippableItem(string name, int damage)
    {
        Name = name;
        Damage = damage;
        RefreshUIFlag = true;

    }

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
    }

    public void Unequip(Unit c)
    {
        c.Health.removeAllModifiersFromSource(this);
        c.Mana.removeAllModifiersFromSource(this);
        c.Strength.removeAllModifiersFromSource(this);
        c.Intellect.removeAllModifiersFromSource(this);
        c.Dexterity.removeAllModifiersFromSource(this);
        c.Damage.removeAllModifiersFromSource(this);
    }
}
