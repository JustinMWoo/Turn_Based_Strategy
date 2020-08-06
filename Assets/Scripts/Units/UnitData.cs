using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum UnitTeam
{
    Player,
    NPC
}

[System.Serializable]
public class UnitData 
{
    public string id;

    // Do I need this for the players units?
    // public UnitTeam unitTeam;

    public Vector3 position;
    public Quaternion rotation;
    public bool npc;
    public int unitType;
}
