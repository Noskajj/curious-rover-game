using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scannable : MonoBehaviour
{
    [Header("--- Cursors ---")]
    //The main sprite used for the game
    [SerializeField]
    private Animator cursorAnimator;

    [Header("--- Variables ---")]
    [SerializeField]
    private GameObject thisObject;

    [SerializeField]
    private GameObject sceneCamera;

    [SerializeField]
    private Animator scanOverlayAnimator;

    [SerializeField]
    private CinemachineCamera scanCam;
    [SerializeField]
    private float zoomFOV, regFOV;
    [SerializeField]
    private TotalScanPopup totalScanPopup;

    private float currentFOV;

    private Coroutine zoomCoroutine;

    private GameObject scanTarget;

    public static bool overObject = false;

    private InputAction scanButton;

    private float timeToScan = 3f, pressStartTime;

    private bool isPressed;

    private void Start()
    {        
        scanButton = InputSystem.actions.FindAction("Scan");

        scanButton.started += Scan;
        scanButton.canceled += ScanFailed;

        Debug.Log(cursorAnimator.GetBool("IsScanning"));
    }

    private void OnDisable()
    {
        scanButton.started -= Scan;
        scanButton.canceled -= ScanFailed;
    }

    private void Update()
    {
        //UpdateCursor();

        if (isPressed && Time.time - pressStartTime >= timeToScan)
        {
            ScanSuccessful();
        }
        else if (isPressed)
        {
            //Debug.Log(Time.time - pressStartTime);
        }
    }

    private void Scan(InputAction.CallbackContext context)
    {
        if (scanTarget != null)
        {
            Debug.Log("Started scanning");

            isPressed = true;
            pressStartTime = Time.time;

            cursorAnimator.SetBool("IsScanning", true);
            scanOverlayAnimator.SetBool("IsScanning", true);

            if (zoomCoroutine != null)
                StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(ScanZoom(regFOV, zoomFOV, 3f));
            Debug.Log(cursorAnimator.GetBool("IsScanning"));

        }
    }

    private void ScanFailed(InputAction.CallbackContext context)
    {
        isPressed = false;
        cursorAnimator.SetBool("IsScanning", false);
        scanOverlayAnimator.SetBool("IsScanning", false);
        if(zoomCoroutine != null)
        StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(ScanZoom(currentFOV, regFOV, 1f));
    }

    private void ScanSuccessful()
    {
        isPressed = false;

        //Updates the successfully scanned variable
        scanTarget.GetComponent<ScannableObject>().SuccessfullyScanned();

        //Testing code, only changes the material 
        Debug.Log("You Scanned: " + scanTarget.name);

        //Starts the popup script
        DatabasePopup.Instance.StartPopup(scanTarget.GetComponent<ScannableObject>());

        //scanTarget.transform.GetComponent<MeshRenderer>().material = successMat;
        //cursorAnimator.SetBool("IsScanning", false);
        //scanOverlayAnimator.SetBool("IsScanning", false);

        StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(ScanZoom(currentFOV, regFOV, 1f));

        GlobalVar.totalScanned++;
        //Run Total Scan Popup
        totalScanPopup.StartPopup();
    }

    private void LateUpdate()
    {
        //resets if not looking at scannable object
        overObject = false;
    }

    public void SetScanTarget(GameObject scanTarget)
    {
        this.scanTarget = scanTarget;
    }


    IEnumerator ScanZoom(float startFOV, float endFOV, float zoomTime)
    {
        float timeElapsed = 0f;
        
        while(timeElapsed < zoomTime)
        {
            timeElapsed += Time.deltaTime;
            float timer = timeElapsed / zoomTime;

            currentFOV = Mathf.Lerp(startFOV, endFOV, timer);
            scanCam.Lens.FieldOfView = currentFOV;
            yield return null;
        }
    }

}
