using System;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string Name;
    [SerializeField]
    protected int Damage;
    [SerializeField]
    protected string Type;

    public Weapon()
    {
        Name = "";
        Damage = 0;
        Type = "";
    }
    public Weapon(string name, int damage, string type)
    {
        Name = name;
        Damage = damage;
        Type = type;
    }
    
}
