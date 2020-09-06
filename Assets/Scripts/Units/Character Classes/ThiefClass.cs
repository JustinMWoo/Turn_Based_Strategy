
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Thief")]
public class ThiefClass : BaseCharacterClass
{
    public ThiefClass()
    {
        CharacterClassName = "Thief";
        CharacterClassDescription = "Sneaky boi";
        Strength = new StatModifier(20, this);
        Intellect = new StatModifier(5, this);
        Health = new StatModifier(50, this);
        Mana = new StatModifier(20, this);
        Dexterity = new StatModifier(10, this);

        Movement = new StatModifier(5, this);
        JumpHeight = new StatModifier(3, this);
    }
}
