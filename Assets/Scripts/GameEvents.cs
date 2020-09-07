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

    //public event Action onSaveInitialized;
    //public void SaveInitialized()
    //{
    //    if (onSaveInitialized != null)
    //    {
    //        onSaveInitialized.Invoke();
    //    }
    //}

    public event Action OnLoadInitialized;
    public bool Loading=false;

    public IEnumerator LoadInitialized(string fileName)
    {
        Loading = true;
        SaveData.current = (SaveData)SerializationManager.Load(fileName);
        if (OnLoadInitialized != null)
        {
            OnLoadInitialized.Invoke();
        }
        yield return null;

    }

    public event Action OnLoadTurns;
    public void LoadTurns()
    {
        if (OnLoadTurns != null)
        {
            OnLoadTurns.Invoke();
        }
        Loading = false;
    }

    public event Action OnEndUnitTurn;
    public void EndUnitTurn()
    {
        if (OnEndUnitTurn != null)
        {
            OnEndUnitTurn.Invoke();
        }
    }

    public event Action OnEndTeamTurn;
    public void EndTeamTurn()
    {
        if (OnEndTeamTurn != null)
        {
            OnEndTeamTurn.Invoke();
        }
    }

}
