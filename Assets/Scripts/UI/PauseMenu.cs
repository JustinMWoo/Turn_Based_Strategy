using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject pauseMenuUI;
    [SerializeField]
    public GameObject mainScreen;
    [SerializeField]
    public GameObject saveScreen;
    [SerializeField]
    public GameObject loadScreen;

    private bool isPaused = false;

    // For setting the menu when starting a pause
    private bool startPause = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);

        if (startPause)
        {
            mainScreen.SetActive(true);
            saveScreen.SetActive(false);
            loadScreen.SetActive(false);
            startPause = false;
        }
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        startPause = true;
    }

    public void SaveMenu()
    {
        if (TurnManager.playerUnitTurnStart)
        {
            // Enable save menu
            saveScreen.SetActive(true);
            mainScreen.SetActive(false);

            //GameEvents.current.SaveInitialized();
        }
    }

    public void LoadMenu()
    {
        loadScreen.SetActive(true);
        mainScreen.SetActive(false);
    }

    public void ReturnToMain()
    {
        mainScreen.SetActive(true);
        loadScreen.SetActive(false);
        saveScreen.SetActive(false);
    }
}
