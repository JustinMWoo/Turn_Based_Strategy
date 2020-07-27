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

    public void weapon(string name, int damage, string type)
    {
        Name = name;
        Damage = damage;
        Type = type;
    }
    
}
