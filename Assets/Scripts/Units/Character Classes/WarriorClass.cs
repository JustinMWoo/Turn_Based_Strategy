using Stats.CharacterStats;
using UnityEngine;

// warrior class base stats and info
[CreateAssetMenu(menuName = "Class/Warrior")]
public class WarriorClass : BaseCharacterClass 
{
    public WarriorClass()
    {
        CharacterClassName = "Warrior";
        CharacterClassDescription = "Stronk boi";
        Strength = new CharacterStats(20);
        Speed = new CharacterStats(10);
        Intellect = new CharacterStats(5);
        Health = new CharacterStats(50);
        Mana = new CharacterStats(20);
        Dexterity = new CharacterStats(10);

        Movement = new CharacterStats(4);
        JumpHeight = new CharacterStats(1);

    }
}
