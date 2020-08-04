using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected string Name;
    protected string Description;

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
