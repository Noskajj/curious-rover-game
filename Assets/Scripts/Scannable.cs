using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scannable : MonoBehaviour
{
    [Header("--- Cursors ---")]
    //The main sprite used for the game
    [SerializeField]
    private GameObject regularCursor;

    //The sprite used when hovering over a scannable object
    [SerializeField]
    private GameObject scanningCursor;


    [Header("--- Variables ---")]
    [SerializeField]
    private GameObject thisObject;

    [SerializeField]
    private GameObject sceneCamera;

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
        CameraLockSetUp();
    }

    private void CameraLockSetUp()
    {
        cinCamera = sceneCamera.GetComponent<CinemachineCamera>();
        camRotation = sceneCamera.GetComponent<CinemachineRotationComposer>();
        camOrbit = sceneCamera.GetComponent<CinemachineOrbitalFollow>();

        mainCamOffset = camRotation.TargetOffset;
        scanningOffset = new Vector3(0, 0, 0);

        camOrbit.enabled = true;

        rigTop = camOrbit.Orbits.Top.Radius;
        rigMid = camOrbit.Orbits.Center.Radius;
        rigBot = camOrbit.Orbits.Bottom.Radius;
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
            Debug.Log(Time.time - pressStartTime);
        }
    }

    private void Scan(InputAction.CallbackContext context)
    {
        if (scanTarget != null)
        {
            Debug.Log("Started scanning");
            isPressed = true;
            pressStartTime = Time.time;

            //Code for locking the camera to the target object
            cinCamera.LookAt = scanTarget.transform;
            camRotation.TargetOffset = scanningOffset;
            SetRigRadii(1, 1, 1);
        }
    }

    private void ScanFailed(InputAction.CallbackContext context)
    {
        isPressed = false;
        cinCamera.LookAt = thisObject.transform;
        camRotation.TargetOffset = mainCamOffset;
        SetRigRadii(rigTop, rigMid, rigBot);
    }

    private void ScanSuccessful()
    {
        isPressed = false;
        scanTarget.GetComponent<ScannableObject>().SuccessfullyScanned();
        cinCamera.LookAt = thisObject.transform;
        camRotation.TargetOffset = mainCamOffset;
        SetRigRadii(rigTop, rigMid, rigBot);

        //Updates the successfully scanned variable
        scanTarget.GetComponent<ScannableObject>().SuccessfullyScanned();

        //Testing code, only changes the material 
        Debug.Log("You Scanned: " + scanTarget.name);
        scanTarget.transform.GetComponent<MeshRenderer>().material = successMat;
    }

    private void TestScan()
    {
        successMat = Resources.Load<Material>("Materials/ScanTestSuccess");
        
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

    private void UpdateCursor()
    {
        /*CONSIDER CHANGING IT TO ONE CURSOR OBJECT AND 
         CHANGE THE VISUALS WITH ANIMATOR TO HELP
         SIMPLIFY THE CODE
        */

        if(overObject)
        {
            regularCursor.SetActive(false);
            scanningCursor.SetActive(true);
        }
        else
        {
            regularCursor.SetActive(true);
            scanningCursor.SetActive(false);
        }
    }

    private void SetRigRadii(float top, float mid, float bot)
    {
        camOrbit.Orbits.Top.Radius = top;
        camOrbit.Orbits.Center.Radius = mid;
        camOrbit.Orbits.Bottom.Radius = bot;
    }
}
