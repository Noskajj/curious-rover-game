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

    private Material successMat;

    private GameObject scanTarget;

    public static bool overObject = false;

    private InputSystem_Actions inputActions;

    private void Start()
    {
        TestScan();
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.Player.Scan.performed += context => Scan();
    }

    private void Update()
    {
        /*CONSIDER CHANGING IT TO ONE CURSOR OBJECT AND 
         CHANGE THE VISUALS WITH ANIMATOR TO HELP
         SIMPLIFY THE CODE
        */

        /*if(overObject)
        {
            regularCursor.SetActive(false);
            scanningCursor.SetActive(true);
        }
        else
        {
            regularCursor.SetActive(true);
            scanningCursor.SetActive(false);
        }*/

        
    }

    private void Scan()
    {
        //Need to lock on to object with camera when scanning
        //Need to set a time holding E to scan
        if (scanTarget != null)
        {
            Debug.Log("You Scanned: " + scanTarget.name);
            scanTarget.transform.GetComponent<MeshRenderer>().material = successMat;
            scanTarget.GetComponent<ScannableObject>().HasBeenScanned();
        }
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
}
