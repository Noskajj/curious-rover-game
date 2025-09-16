using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    //Singleton setup
    public static Settings Instance { get; private set; }

    private const int CurrentSettingsVersion = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        //Ensures settings are up to date
        InitialiseSettings();
    }


    private InputAction restartScene;

    private void OnEnable()
    {
        restartScene = InputSystem.actions.FindAction("RestartScene");

        restartScene.started += RestartScene;
    }

    private void OnDisable()
    {
        restartScene.started -= RestartScene;
    }

    private void InitialiseSettings()
    {
        int savedVersion = PlayerPrefs.GetInt("SettingsVersion", 0);

        switch(savedVersion)
        {
            case 0:
            //Initializes settings
                PlayerPrefs.SetFloat("MusicVol", 50f);
                PlayerPrefs.SetFloat("SoundVol", 50f);

                break;

            case 1:
                break;

            default:
                Debug.LogWarning("Settings version error");
                PlayerPrefs.DeleteAll();
                savedVersion = 0;
                //Goto is a way to get between case statements
                goto case 0;
        }

        PlayerPrefs.SetInt("SettingsVersion", CurrentSettingsVersion);
        PlayerPrefs.Save();
    }

    private void RestartScene(InputAction.CallbackContext context)
    {
        Restart();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
