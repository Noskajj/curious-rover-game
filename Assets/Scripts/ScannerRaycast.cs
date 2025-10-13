using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
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

    private MeshRenderer[] renderers;



    private void Update()
    {
        origin = transform.position;
        direction = transform.forward;

        VisualiseRaycast();

        if(CameraLook.isCameraActive)
        {
            DetectObject();
        }
        else
        {
            DisableGlow(renderers);
        }
        
    }

    private void VisualiseRaycast()
    {
        Debug.DrawRay(origin, direction * rayDistance, rayColor);
    }

    private void DetectObject()
    {

        if (Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance))
        {

            ScannableObject scannableObj = hit.collider.GetComponent<ScannableObject>();

            if (scannableObj != null && !scannableObj.GetScannableSO().hasBeenScanned)
            {
                //Debug.Log("You Detected: " + scannableObj.GetScannableSO().GetName());
                
                //Can get the data here
                currentTarget = hit.collider.gameObject;
                //Debug.Log("passing " + currentTarget);
                scannable.SetScanTarget(currentTarget);
                Scannable.overObject = true;
                
                scannableObj = null;


                GetScanTextures(hit.collider.gameObject);

                EnableGlow(renderers);
            }
        }
        else
        {
            DisableGlow(renderers);
            scannable.SetScanTarget(null);
        }
    }


    private void GetScanTextures(GameObject scanObj)
    {
        if (scanObj == null)
        {
            return;
        }

        renderers = scanObj.GetComponentsInChildren<MeshRenderer>();

    }

    private void EnableGlow(MeshRenderer[] renders)
    {
        if (renders == null)
        {
            return;
        }

        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        foreach (Renderer renderer in renders)
        {
            propertyBlock.SetFloat("_HighlightVisible", 1f);

            renderer.SetPropertyBlock(propertyBlock, 0);
        }
    }

    private void DisableGlow(MeshRenderer[] renders)
    {
        if (renders == null)
        {
            return;
        }

        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        foreach (Renderer renderer in renders)
        {
            propertyBlock.SetFloat("_HighlightVisible", 0f);

            renderer.SetPropertyBlock(propertyBlock, 0);
        }
    }

    public void ExtDisableGlow()
    {
        DisableGlow(renderers);
    }
}
