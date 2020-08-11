using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    GameObject[] npcPrefabs;
    GameObject[] playerPrefabs;

    void Start()
    {
        npcPrefabs = Resources.LoadAll<GameObject>("Prefabs/NPC");
        playerPrefabs = Resources.LoadAll<GameObject>("Prefabs/Player");
        //Debug.Log(npcTypes.Length);

        //GameEvents.current.onSaveInitialized += OnSave;
        GameEvents.current.onLoadInitialized += OnLoad;
    }

    //public void OnSave()
    //{
    //    SerializationManager.Save("unitsave", SaveData.current);
    //}

    public void OnLoad()
    {
       for (int i = 0; i < SaveData.current.units.Count; i++)
        {
            UnitData currentUnit = SaveData.current.units[i];
            if (currentUnit.npc)
            {
                GameObject obj = Instantiate(npcPrefabs[currentUnit.unitType]);
                //Debug.Log(npcTypes[currentUnit.npcType]);

                Unit unit = obj.GetComponent<Unit>();
                unit.unitData = currentUnit;
                unit.transform.position = currentUnit.position;
                unit.transform.rotation = currentUnit.rotation;
            }
            else
            {
                GameObject obj = Instantiate(playerPrefabs[currentUnit.unitType]);
                //Debug.Log(npcTypes[currentUnit.npcType]);

                Unit unit = obj.GetComponent<Unit>();
                unit.unitData = currentUnit;
                unit.transform.position = currentUnit.position;
                unit.transform.rotation = currentUnit.rotation;
            }
        }
    }
}
