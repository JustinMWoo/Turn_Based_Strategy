using System;
using UnityEngine;

[Serializable]
public class Armor : MonoBehaviour
{
    [SerializeField]
    protected string Name;

    [SerializeField]
    protected int Defense;

    [SerializeField]
    protected int MagicDefense;

    [SerializeField]
    protected string Type;
    public Armor()
    {
        Name = "";
        Defense = 0;
        MagicDefense = 0;
        Type = "";
    }
    public Armor(string name, int defense, int magicdefense, string type)
    {
        
        Name = name;
        Defense = defense;
        MagicDefense = magicdefense;
        Type = type;

    }
}
