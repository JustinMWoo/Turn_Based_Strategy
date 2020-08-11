using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurnData 
{
    //public Dictionary<string, List<Unit>> units = new Dictionary<string, List<Unit>>();
    public Queue<string> turnKeyData = new Queue<string>();
    public List<string> unitsLeft = new List<string>();
}
