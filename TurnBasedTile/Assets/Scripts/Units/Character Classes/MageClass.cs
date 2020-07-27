
public class MageClass : BaseCharacterClass
{
    public void mageClass()
    {
        characterClassName = "Mage";
        characterClassDescription = "Smort boi";
        strength = new characterStats(5);
        speed = new characterStats(10);
        intellect = new characterStats(20);
        health = new characterStats(30);
        mana = new characterStats(60);
        dexterity = new characterStats(10);
    }
}
