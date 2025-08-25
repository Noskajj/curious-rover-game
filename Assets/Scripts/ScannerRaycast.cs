using UnityEngine;

public class ScannerRaycast : MonoBehaviour
{
    //This determines how close you need to be to scan something
    [SerializeField]
    private float rayDistance = 10f;
    [SerializeField]
    private Scannable scannable;

    private Material hoverMat;

    private Color rayColor = Color.red;

    private Vector3 origin, direction;

    private GameObject currentTarget;

    private void Start()
    {
        ScannerTest();
    }

    private void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        VisualiseRaycast();
        DetectObject();
    }

    private void VisualiseRaycast()
    {
        Debug.DrawRay(origin, direction * rayDistance, rayColor);
    }

    private void DetectObject()
    {
        if(Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        {
            ScannableObject scannableObj = hit.collider.GetComponent<ScannableObject>();

            if (scannableObj != null && !scannableObj.GetScannableSO().hasBeenScanned)
            {
                Debug.Log("You Detected: " + scannableObj.GetScannableSO().GetName());

                //Can get the data here
                currentTarget = hit.collider.gameObject;
                Debug.Log("passing " + currentTarget);
                scannable.SetScanTarget(currentTarget);
                Scannable.overObject = true;
                this.hoverMat = currentTarget.GetComponent<MeshRenderer>().material;
                hoverMat.EnableKeyword("_EMISSION");
                scannableObj = null;
            }
        }
        else
        {
            scannable.SetScanTarget(null);
            hoverMat.DisableKeyword("_EMISSION");
            currentTarget = null;
        }
    }

    private void ScannerTest()
    {
        hoverMat = Resources.Load<Material>("Materials/ScanMats/ScanTest");
    }
}
