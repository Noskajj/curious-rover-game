using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("--- Sound Files ---")]
    [SerializeField]
    private AudioClip menuMusic;
    [SerializeField]
    private AudioClip gameMusic;

    [Header("--- Variables ---")]
    [SerializeField]
    private AudioSource audioSource;

    public static SoundManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void Start()
    {
        audioSource.clip = menuMusic;
        audioSource.Play();
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        switch (nextScene.name)
        {
            case "Level Greybox1":
                GameSceneLoaded(currentScene.name);
                break;
            default:
                MenuSceneLoaded(currentScene.name); 
                break;
        }


    }

    private void GameSceneLoaded(string sceneName)
    {
        if(audioSource.clip != gameMusic)
        {
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
    }

    private void MenuSceneLoaded(string sceneName)
    {
        if(audioSource.clip != menuMusic)
        {
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
    }
}
