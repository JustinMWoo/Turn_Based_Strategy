
// warrior class base stats and info
public class WarriorClass : BaseCharacterClass 
{
    public void warriorClass()
    {
        characterClassName = "Warrior";
        characterClassDescription = "Stronk boi";
        strength = new characterStats(20);
        speed = new characterStats(10);
        intellect = new characterStats(5);
        health = new characterStats(50);
        mana = new characterStats(20);
        dexterity = new characterStats(10);
    }
}
