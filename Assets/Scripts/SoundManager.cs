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

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
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
        if(sceneName != "Level Greybox1")
        {

        }
    }

    private void MenuSceneLoaded(string sceneName)
    {

    }
}
