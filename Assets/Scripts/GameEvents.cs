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

    public event Action onSaveData;
    public void SaveData()
    {
        if (onSaveData != null)
        {
           onSaveData.Invoke();
        }
    }

    public event Action onLoadData;
    public void LoadData()
    {
        if (onLoadData != null)
        {
            onLoadData.Invoke();
        }
    }

}
