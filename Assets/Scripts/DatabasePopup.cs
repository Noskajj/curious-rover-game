using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DatabasePopup : MonoBehaviour
{
    //Turns the script into a singleton
    public static DatabasePopup Instance { get; private set; }

    [Header("--- Variables ---")]
    [SerializeField]
    private float TimeOnScreen = 3f;
    [SerializeField]
    private float fadeOutTime = 2f, fadeInTime = 1f;

    [SerializeField]
    private Image popUpImg;

    private void Awake()
    {
        //Ensures that there is only one instance in the scene
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartPopup(ScannableObject scanObj)
    {
        popUpImg.enabled = true;
        popUpImg.sprite = scanObj.GetScannableSO().GetObjectPopup();
        //Starts the timer
        StartCoroutine(PopupRoutine());
    }

    private void EndPopup()
    {
        popUpImg.enabled = false;
    }

    IEnumerator PopupRoutine()
    {
        //Fade the image in
        yield return StartCoroutine(PopupFade(0f, 1f, fadeInTime));
        //Waits for x seconds
        yield return new WaitForSecondsRealtime(TimeOnScreen);
        
        //Fade the image out
        yield return StartCoroutine(PopupFade(1f, 0f, fadeOutTime));

        EndPopup();
    }

    IEnumerator PopupFade(float startVal,  float endVal, float fadeTime)
    {
        float timeElapsed = 0f;
        Color colour = popUpImg.color;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float timer = timeElapsed / fadeTime;

            colour.a = Mathf.Lerp(startVal, endVal, timer);
            popUpImg.color = colour;
            yield return null;
        }

        colour.a = endVal;
        popUpImg.color = colour;
    }

}
