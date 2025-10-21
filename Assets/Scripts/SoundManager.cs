using System.Collections;
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
    [SerializeField]
    private float fadeDuration = 1f;

    public static SoundManager instance { get; private set; }

    private Coroutine coroutine;

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
            if (coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(FadeAudioClip(gameMusic));
        }
    }

    private void MenuSceneLoaded(string sceneName)
    {
        if(audioSource.clip != menuMusic)
        {
            if(coroutine !=null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(FadeAudioClip(menuMusic));
        }
    }

    private IEnumerator FadeAudioClip(AudioClip nextClip, float targetVolume = 1f)
    {
        float startVol = audioSource.volume;

        if(audioSource.isPlaying)
        {
            for(float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(startVol, 0f, t / fadeDuration);
                yield return null;
            }

            audioSource.clip = nextClip;
            audioSource.Play();

            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }

    public void SetAudioClip(AudioClip audioClip, float targetVol = 1f)
    {
        if (audioSource.clip != audioClip)
        {
            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(FadeAudioClip(audioClip, targetVol));
        }
    }
}
