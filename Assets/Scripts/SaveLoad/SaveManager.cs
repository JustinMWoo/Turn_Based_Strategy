using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    //public TMP_InputField saveName;
    //public void OnSave()
    //{
    //    SerializationManager.Save(saveName.text, SaveData.current);
    //}

    //public string[] saveFiles;
    //public void GetLoadFiles()
    //{
    //    if(!Directory.Exists(Application.persistentDataPath + "/saves/"))
    //    {
    //        Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
    //    }

    //    saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    //}

    //public void ShowLoadScreen()
    //{
    //    GetLoadFiles();

    //    foreach(Transform button in loadArea)
    //    {
    //        Destroy(button.gameObject);
    //    }

    //    for(int i = 0; i < saveFiles.Length; i++)
    //    {
    //        GameObject buttonObject = Instantiate(loadButtonPrefab);
    //        buttonObject.transform.SetParent(loadArea.transform, false);

    //        var index = i;
    //        buttonObject.GetComponent<Button>().onClick.AddListener(() =>
    //        {
    //            UnitManager.
    //        })
    //    }
    //}
}
