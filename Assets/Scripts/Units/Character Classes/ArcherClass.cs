using Stats.CharacterStats;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Archer")]
public class ArcherClass : BaseCharacterClass
{
    public ArcherClass()
    {
        CharacterClassName = "Archer";
        CharacterClassDescription = "shoot boi";
        Strength = new CharacterStats(20);
        Speed = new CharacterStats(10);
        Intellect = new CharacterStats(5);
        Health = new CharacterStats(50);
        Mana = new CharacterStats(20);
        Dexterity = new CharacterStats(10);

        Movement = new CharacterStats(4);
        JumpHeight = new CharacterStats(2);
    }
}
