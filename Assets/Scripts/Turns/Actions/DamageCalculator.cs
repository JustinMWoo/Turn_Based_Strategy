using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DamageType
{
    Physical = 1,
    Magical = 2
}

public enum AttackDirection
{
    None = 0, // For spell casts and maybe ranged attacks
    Front = 1,
    Side = 2,
    Back = 3
}

public class DamageCalculator : MonoBehaviour
{
    #region Singleton
    private static DamageCalculator _current;
    public static DamageCalculator Current { get { return _current; } }

    // Singleton
    private void Awake()
    {
        if (_current != null && _current != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _current = this;
        }
    }
    #endregion

    public GameObject floatingTextPrefab;


    public void DealDamage(Unit attacker, Unit defender, DamageType dmgType)
    {
        int damage = CalculateDamage(attacker, defender, dmgType, true, null);

        // Deal damage to defender
        defender.TakeDamage(damage);

        GameObject textObject = Instantiate(floatingTextPrefab, defender.transform.position, Quaternion.identity, defender.transform);
        TextMeshPro textLabel = textObject.GetComponent<TextMeshPro>();
        textLabel.text = "-" + damage;
        textLabel.color = Color.red;
    }


    /* DAMAGE CALCULATION LOGIC
     * Crit: Mutiplier is always 1.5x, gained from weapons/armor
     * Dodge: Gained from armor and dex
     * Defense: Gained from armor and str
     * Magic Defense: Gained from armor and int
     * 
     * Maybe use dmgType for spells vs hits
     * 
     */
    // Bool for rolls is if crit and dodge should be rolled
    // attackerTile is if the tile is if the attack is being calculated from a tile that the attacker is not currently on (leave null otherwise)
    public int CalculateDamage(Unit attacker, Unit defender, DamageType dmgType, bool rolls, Transform attackerTile)
    {
        if (rolls)
        {
            // Nearest int for combined dodge
            float totalDodge = CalculateDodge(defender);

            // Roll for dodge
            if (Random.Range(0, 100) <= totalDodge)
            {
                // Attack dodged
                // TODO: attacker.playdodgeanimation
                //Debug.Log("Dodged: " + totalDodge);
                return 0;
            }
        }

        float damage = attacker.Damage.Value;
        float multiplier = 1;

        // 5% damage for every 10 points in stat
        if (attacker.Weapon.WeaponScaling == WeaponScaling.Str)
        {
            multiplier = Mathf.Round(attacker.Strength.Value / 10f) * 0.05f + 1;
        }
        else if (attacker.Weapon.WeaponScaling == WeaponScaling.Int)
        {
            multiplier = Mathf.Round(attacker.Intellect.Value / 10f) * 0.05f + 1;
        }
        else if (attacker.Weapon.WeaponScaling == WeaponScaling.Dex)
        {
            multiplier = Mathf.Round(attacker.Dexterity.Value / 10f) * 0.05f + 1;
        }

        damage *= multiplier;

        // Roll for crit
        if (rolls && Random.Range(0, 100) <= attacker.CritChance.Value)
        {
            //Debug.Log("Crit");
            damage *= 1.5f;
        }

        // Reduce by defenders resistances
        if (dmgType == DamageType.Physical)
        {
            damage -= defender.Defense.Value + defender.Strength.Value / 10f;
        }
        else if (dmgType == DamageType.Magical)
        {
            damage -= defender.MagicDefense.Value + defender.Intellect.Value / 10f;
        }


        AttackDirection dir;
        if (attackerTile == null)
        {
            //Determine attacking side
            dir = DetermineAttackDirection(attacker.gameObject.transform, defender.gameObject.transform);
        }
        else
        {
            dir = DetermineAttackDirection(attackerTile, defender.gameObject.transform);
        }

        if (dir == AttackDirection.Front)
        {
            //Debug.Log("Front");
            // Reduce damage by 50%
            damage *= 0.5f;
        }
        else if (dir == AttackDirection.Side)
        {
            //Debug.Log("Side");
            // Reduce damage by 25%
            damage *= 0.75f;
        }
        else if (dir == AttackDirection.Back)
        {
            // Full damage
            //Debug.Log("Back");
        }

        //Debug.Log("Final Damage: " + Mathf.RoundToInt(damage));
        return Mathf.RoundToInt(damage);
    }

    public float CalculateDodge(Unit unit)
    {
        // Diminishing returns for dodge (can tweak for better curve
        float dexDodge = Mathf.Pow(1.005f, -2 * unit.Dexterity.Value / 10f) * unit.Dexterity.Value / 10f;

        // Nearest int for combined dodge
        float totalDodge = Mathf.Round(unit.DodgeChance.Value + dexDodge);

        return totalDodge;
    }

    private AttackDirection DetermineAttackDirection(Transform attacker, Transform defender)
    {
        // Convert to 2D for angle calculation
        Vector2 attacker2D = new Vector2(attacker.position.x, attacker.position.z);
        Vector2 defender2D = new Vector2(defender.position.x, defender.position.z);
        // Forward from the defender
        Vector2 defender2DDir = new Vector2(defender.forward.x, defender.forward.z);

        Vector2 attackerToDefender = defender2D - attacker2D;

        float angle = Vector2.Angle(attackerToDefender, defender2DDir);

        if (angle < 45)
        {
            return AttackDirection.Back;
        }
        else if (angle <= 135)
        {
            return AttackDirection.Side;
        }
        else
        {
            return AttackDirection.Front;
        }
    }
}
