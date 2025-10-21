using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 1f;

    [SerializeField]
    private Image fadeImg;

    public void OpenGameScene()
    {
        StartCoroutine(FadeOut("Level Greybox1"));
    }

    public void OpenMissionInfoScene()
    {
        StartCoroutine(FadeOut("MissionObjective"));
    }

    public void OpenTitleScreen()
    {
        StartCoroutine(FadeOut("TitleScreen"));
    }

    public void OpenCreditsScreen()
    {
        StartCoroutine(FadeOut("CreditsScreen"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeOut(string levelToLoad)
    {
        if (fadeImg != null)
        {
            
            float timeElapsed = 0f;
            Color colour = fadeImg.color;

            while (timeElapsed < fadeTime)
            {
                Debug.Log("Navigation: time for fade" + timeElapsed);
                timeElapsed += Time.deltaTime;
                float timer = timeElapsed / fadeTime;

                colour.a = Mathf.Lerp(0f, 1f, timer);

                fadeImg.color = colour;
                yield return null;
            }
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
