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

        GameEvents.current.onSaveData += OnSave;
        GameEvents.current.onLoadData += OnLoad;
    }

    public void OnSave()
    {
        SerializationManager.Save("unitsave", SaveData.current);
    }

    public void OnLoad()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/unitsave.save");

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
