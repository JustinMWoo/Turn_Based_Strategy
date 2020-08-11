using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action onSaveInitialized;
    public void SaveInitialized()
    {
        if (onSaveInitialized != null)
        {
            onSaveInitialized.Invoke();
        }
    }

    public event Action onLoadInitialized;
    public bool Loading=false;

    public IEnumerator LoadInitialized()
    {
        Loading = true;
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/unitsave.save");
        if (onLoadInitialized != null)
        {
            onLoadInitialized.Invoke();
        }
        yield return null;

    }

    public event Action onLoadTurns;
    public void LoadTurns()
    {
        if (onLoadTurns != null)
        {
            onLoadTurns.Invoke();
        }
        Loading = false;
    }

}
