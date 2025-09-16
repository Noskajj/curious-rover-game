using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    //Singleton setup
    public static Settings Instance { get; private set; }

    private const int CurrentSettingsVersion = 1;

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

    [SerializeField]
    private AudioMixer mixer;

    private float musicVol, sfxVol;

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
                Debug.Log("Version 0");
                PlayerPrefs.SetFloat("MusicVol", 0.5f);
                PlayerPrefs.SetFloat("SoundVol", 0.5f);

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

    private void Start()
    {
        UpdateMusicVolume(PlayerPrefs.GetFloat("MusicVol"));
        UpdateSoundVolume(PlayerPrefs.GetFloat("SoundVol"));
    }

    private void RestartScene(InputAction.CallbackContext context)
    {
        Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateMusicVolume(float value)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        musicVol = value;
    }

    public void UpdateSoundVolume(float value)
    {
        mixer.SetFloat("SfxVol", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20);
        sfxVol = value;
    }

    public void UpdateSettings()
    {
        PlayerPrefs.SetFloat("MusicVol", musicVol);
        PlayerPrefs.SetFloat("SoundVol", sfxVol);

        PlayerPrefs.Save();
    }
}
