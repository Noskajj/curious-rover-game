using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SoundSystem
{
    Headphones, Speaker
}
public class Settings : MonoBehaviour
{
    //Singleton setup
    public static Settings Instance { get; private set; }

    private const int CurrentSettingsVersion = 2;

    private float headphoneVol = 0, speakerVol = 10;

    private SoundSystem currentSoundSystem;

    [SerializeField]
    private TMP_Dropdown soundDropdown;

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

                goto case 1;

            case 1:
                Debug.Log("Version 1");
                PlayerPrefs.SetString("SoundSystem", SoundSystem.Headphones.ToString());
                break;

            case 2:

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
        UpdateSoundSystem((SoundSystem)Enum.Parse(typeof(SoundSystem) ,PlayerPrefs.GetString("SoundSystem")));
        
        if(soundDropdown != null)
        soundDropdown.onValueChanged.AddListener(SoundSystemChanged);
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
        PlayerPrefs.SetString("SoundSystem", currentSoundSystem.ToString());

        PlayerPrefs.Save();
    }

    private void SoundSystemChanged(int val)
    {
        switch (val) {
            case 1:
                UpdateSoundSystem(SoundSystem.Speaker);
                break;

            default:
                UpdateSoundSystem(SoundSystem.Headphones);
                break;
        }
    }

    public void UpdateSoundSystem(SoundSystem soundSystem)
    {
        switch (soundSystem)
        {
            case SoundSystem.Headphones:
                mixer.SetFloat("MasterVol", headphoneVol);
                break;

            case SoundSystem.Speaker:
                mixer.SetFloat("MasterVol", speakerVol);
                break;
        }
    }
}
