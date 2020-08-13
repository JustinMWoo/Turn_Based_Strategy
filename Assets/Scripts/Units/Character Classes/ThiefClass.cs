using Stats.CharacterStats;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Thief")]
public class ThiefClass : BaseCharacterClass
{
    public ThiefClass()
    {
        CharacterClassName = "Thief";
        CharacterClassDescription = "Sneaky boi";
        Strength = new CharacterStats(20);
        Speed = new CharacterStats(10);
        Intellect = new CharacterStats(5);
        Health = new CharacterStats(50);
        Mana = new CharacterStats(20);
        Dexterity = new CharacterStats(10);

        Movement = new CharacterStats(5);
        JumpHeight = new CharacterStats(3);
    }
}
