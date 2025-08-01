using UnityEngine;

public class ScannerRaycast : MonoBehaviour
{
    //This determines how close you need to be to scan something
    [SerializeField]
    private float rayDistance = 10f;
    private Color rayColor = Color.red;

    private Vector3 origin, direction;


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
            ScannableObject scannable = hit.collider.GetComponent<ScannableObject>();

            if (scannable != null)
            {
                Debug.Log("You Scanned: " + scannable.GetScannableSO().GetName());

                //Can get the data here
                Scannable.overObject = true;
            }
        }
    }
}
