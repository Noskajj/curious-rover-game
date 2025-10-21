using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnterZone : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textBox;

    [SerializeField]
    private ScanZone thisZone;

    private static ScanZone scanZone = ScanZone.Clearing;

    [Header("--- Ui Objects ---")]
    [SerializeField]
    private GameObject popUpParent;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private float clipVol;

    private CanvasGroup canvasGroup;

    private float currentFade = 0f;

    private Coroutine currentTextRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (scanZone != thisZone)
            {
                scanZone = thisZone;
                switch(thisZone)
                {
                    case ScanZone.CrashSite:
                        textBox.text = "Crash Site";
                        break;
                    case ScanZone.BrambleForest:
                        textBox.text = "Bramble Forest";
                        break;
                    case ScanZone.SparseWoodland:
                        textBox.text = "Sparse Woodland";
                        break;
                    case ScanZone.Clearing:
                        textBox.text = "Clearing";
                        break;

                }

                if(currentTextRoutine != null)
                    StopCoroutine(currentTextRoutine);

                currentTextRoutine = StartCoroutine(EnterZonePopup());
            }
        }
    }


    IEnumerator EnterZonePopup()
    {
        if(clip != null)
            SoundManager.instance.SetAudioClip(clip, clipVol);

        yield return StartCoroutine(FadePopup(0f,1f,1f));

        yield return new WaitForSecondsRealtime(2f);

        yield return StartCoroutine(FadePopup(1f,0f,1f));
    }

    IEnumerator FadePopup(float startVal, float endVal, float fadeTime)
    {
        float timeElapsed = 0f;
        canvasGroup = popUpParent.GetComponent<CanvasGroup>();

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float timer = timeElapsed / fadeTime;

            currentFade = Mathf.Lerp(startVal, endVal, timer);
            canvasGroup.alpha = currentFade;
            popUpParent.GetComponent<CanvasGroup>().alpha = canvasGroup.alpha;
            yield return null;
        }

        canvasGroup.alpha = endVal;
        popUpParent.GetComponent<CanvasGroup>().alpha = canvasGroup.alpha;
    }

    
}
