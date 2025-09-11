using NUnit.Framework;
using System.Collections.Generic;
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

    private List<Material> materials;

    private int materialCount;

    private bool parentHasMat;

    private void Start()
    {
        materials = new List<Material>();
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
        try
        {
            ResetObjectEmission();
        }
        catch
        {
            Debug.Log("No object");
        }

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

                try
                {
                    this.parentHasMat = currentTarget.GetComponent<MeshRenderer>().material != null;
                }
                catch
                {
                    this.parentHasMat = false;
                }

                if(parentHasMat)
                {
                    this.hoverMat = currentTarget.GetComponent<MeshRenderer>().material;
                    hoverMat.EnableKeyword("_EMISSION");
                }
                else
                {
                    GetMaterials(currentTarget);
                    //Debug.Log(materials[0]);
                    foreach(Material mat in materials)
                    {
                        mat.EnableKeyword("_EMISSION");
                    }
                }
            }
        }
        
    }

    private void ResetObjectEmission()
    {
        if (parentHasMat)
        {
            hoverMat.DisableKeyword("_EMISSION");
        }
        else
        {
            foreach (Material mat in materials)
            {
                mat.DisableKeyword("_EMISSION");
            }
        }
        scannable.SetScanTarget(null);
        currentTarget = null;
    }    

    private void ScannerTest()
    {
        hoverMat = Resources.Load<Material>("Materials/ScanMats/ScanTest");
    }

    private void GetMaterials(GameObject parentObject)
    {
        bool newMat = true;
        materials.Clear();
        foreach (MeshRenderer child in parentObject.GetComponentsInChildren<MeshRenderer>())
        {
            for (int i = 0; i < materialCount; i++)
            {
                if (materials[i] == child.GetComponent<MeshRenderer>().material || newMat)
                {
                    newMat = false;
                }
            }

            if(newMat)
            {
                this.materials.Add(child.GetComponent<MeshRenderer>().material);
            }
            else
            {
                newMat = true;
            }
        }
    }
}
