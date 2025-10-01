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

    private CinemachineCamera cinCamera;

    private CinemachineRotationComposer camRotation;

    private CinemachineOrbitalFollow camOrbit;

    private Material successMat;

    private GameObject scanTarget;

    public static bool overObject = false;

    private InputAction scanButton;

    private float timeToScan = 3f, pressStartTime;

    private bool isPressed;

    private Vector3 mainCamOffset, scanningOffset;

    private float rigTop, rigMid, rigBot;

    private void Start()
    {
        TestScan();
        
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
            Debug.Log(cursorAnimator.GetBool("IsScanning"));



            //UpdateCursor();
        }
    }

    private void ScanFailed(InputAction.CallbackContext context)
    {
        isPressed = false;
        cursorAnimator.SetBool("IsScanning", false);
        scanOverlayAnimator.SetBool("IsScanning", false);
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
        cursorAnimator.SetBool("IsScanning", false);
        //scanOverlayAnimator.SetBool("IsScanning", false);
    }

    private void TestScan()
    {
        //Loads in the test success material
        successMat = Resources.Load<Material>("Materials/ScanMats/ScanTestSuccess");
        
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


}
