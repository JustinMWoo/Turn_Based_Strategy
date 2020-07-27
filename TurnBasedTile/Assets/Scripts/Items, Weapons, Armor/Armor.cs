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

    public void armor(string name, int defense, int magicdefense, string type)
    {
        
        Name = name;
        Defense = defense;
        MagicDefense = magicdefense;
        Type = type;

    }
}
