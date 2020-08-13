using Stats.CharacterStats;
using UnityEngine;

[CreateAssetMenu(menuName = "Class/Mage")]
public class MageClass : BaseCharacterClass
{
    public MageClass()
    {
        CharacterClassName = "Mage";
        CharacterClassDescription = "Smort boi";
        Strength = new CharacterStats(5);
        Speed = new CharacterStats(10);
        Intellect = new CharacterStats(20);
        Health = new CharacterStats(30);
        Mana = new CharacterStats(60);
        Dexterity = new CharacterStats(10);

        Movement = new CharacterStats(3);
        JumpHeight = new CharacterStats(1);
    }
}
