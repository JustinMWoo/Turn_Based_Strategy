using Stats.CharacterStats;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Archer")]
public class ArcherClass : BaseCharacterClass
{
    public ArcherClass()
    {
        CharacterClassName = "Archer";
        CharacterClassDescription = "shoot boi";
        Strength = new StatModifier(20, this);
        Intellect = new StatModifier(5, this);
        Health = new StatModifier(50, this);
        Mana = new StatModifier(20, this);
        Dexterity = new StatModifier(10, this);

        Movement = new StatModifier(4, this);
        JumpHeight = new StatModifier(2, this);
    }
}
