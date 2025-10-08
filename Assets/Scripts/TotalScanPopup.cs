using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TotalScanPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject popupObject;

    [SerializeField]
    private float xPos, offScreen, onScreen, timer, timeOnScreen;

    private TMP_Text objectsToScanTxt, totalScanTxt;

    private float currentPos;

    private Coroutine currentRoutine;

    private void Start()
    {
        TMP_Text[] text = popupObject.GetComponentsInChildren<TMP_Text>();
        objectsToScanTxt = text[0];
        totalScanTxt = text[1];
        currentPos = offScreen;
    }

    public void StartPopup()
    {
        objectsToScanTxt.text = GlobalVar.totalScanned.ToString();
        totalScanTxt.text = "/" + GlobalVar.allScannedCount.ToString();

        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(PopupRoutine(currentPos));
    }

    IEnumerator PopupRoutine(float startPos)
    {
        yield return StartCoroutine(PopupMove(startPos, onScreen, timer));

        //Waits for x seconds
        yield return new WaitForSecondsRealtime(timeOnScreen);

        yield return StartCoroutine(PopupMove(onScreen, offScreen, timer));
    }

    IEnumerator PopupMove(float startPos, float endPos, float popupTimer)
    {
        float timeElapsed = 0f;

        while (timeElapsed < popupTimer)
        {
            timeElapsed += Time.deltaTime;
            float timer = timeElapsed / popupTimer;

            currentPos = Mathf.Lerp(startPos, endPos, timer);
            popupObject.transform.localPosition = new Vector3(xPos, currentPos, 0f);
            yield return null;
        }

        popupObject.transform.localPosition = new Vector3(xPos, endPos, 0f);
    }

}
