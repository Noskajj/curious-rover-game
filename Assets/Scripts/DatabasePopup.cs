using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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

    [Header("--- Ui Objects ---")]
    [SerializeField]
    private GameObject popUpParent;
    [SerializeField]
    private Image popUpImg;
    [SerializeField]
    private TextMeshProUGUI scanName, scanDesc;

    private Color colour = Color.aliceBlue;

    InputAction databaseKey, settingsKey;
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

    private void OnEnable()
    {
        databaseKey = InputSystem.actions.FindAction("OpenDatabase");
        settingsKey = InputSystem.actions.FindAction("OpenSettings");

        databaseKey.started += EndPopupEarly;
        settingsKey.started += EndPopupEarly;
    }

    private void OnDisable()
    {
        databaseKey.started -= EndPopupEarly;
        settingsKey.started -= EndPopupEarly;
    }

    public void StartPopup(ScannableObject scanObj)
    {
        popUpParent.SetActive(true);
        popUpImg.sprite = scanObj.GetScannableSO().GetObjectSprite();
        scanName.text = scanObj.GetScannableSO().GetName();
        scanDesc.text = scanObj.GetScannableSO().GetDescription();
        //Starts the timer
        StartCoroutine(PopupRoutine());
    }

    private void EndPopup()
    {
        popUpParent.SetActive(false);
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
        colour = popUpParent.GetComponent<Image>().color;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float timer = timeElapsed / fadeTime;

            colour.a = Mathf.Lerp(startVal, endVal, timer);
            popUpParent.GetComponent<Image>().color = colour;
            yield return null;
        }

        colour.a = endVal;
        popUpImg.color = colour;
    }

    /// <summary>
    /// Ends the popup early if a menu is opened
    /// </summary>
    /// <param name="context"></param>
    private void EndPopupEarly(InputAction.CallbackContext context)
    {
        StopCoroutine("PopupRoutine");
        StopCoroutine("PopupFade");
        EndPopup();
    }

}
