using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public TMP_InputField saveName;
    public Transform loadArea;
    public GameObject loadButtonPrefab;

    public void OnSave()
    {
        if (saveName!=null) {
            SerializationManager.Save(saveName.text, SaveData.current);
            Debug.Log("Saved");
        }
    }

    public string[] saveFiles;
    public void GetLoadFiles()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves/");
        }

        saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves/");
    }

    public void ShowLoadScreen()
    {
        GetLoadFiles();

        foreach (Transform button in loadArea)
        {
            Destroy(button.gameObject);
        }

        for (int i = 0; i < saveFiles.Length; i++)
        {
            GameObject buttonObject = Instantiate(loadButtonPrefab);
            buttonObject.transform.SetParent(loadArea.transform, false);

            var index = i;
            buttonObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                StartCoroutine(LoadCoroutine(saveFiles[index]));
                AbilityManager.current.DeactivateAbilityPanel();
            });

            buttonObject.GetComponentInChildren<TextMeshProUGUI>().text = saveFiles[index].Replace(Application.persistentDataPath + "/saves/", "");
        }
    }

    IEnumerator LoadCoroutine(string saveName)
    {
        yield return StartCoroutine(GameEvents.current.LoadInitialized(saveName));
        GameEvents.current.LoadTurns();
    }
}
