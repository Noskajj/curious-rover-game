using UnityEngine;

public class ObjectSpin : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private Camera previewCamera;

    [SerializeField]
    private float cameraPadding = 1.1f;

    private GameObject currentObject;


    public void SpawnObject(GameObject objectToSpawn)
    {
        //Ensures there is only ever 1 gameobject
        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        //Creates the new object
        currentObject = Instantiate(objectToSpawn, transform.position , transform.rotation, transform);
        StartRotation();
        FixPerspective(currentObject);
    }

    public void StartRotation()
    {
        currentObject.transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (currentObject != null)
        {   
            Debug.Log("Object Spin: current Obj " + currentObject.name);
            currentObject.transform.Rotate(Vector3.up, rotationSpeed * Time.unscaledDeltaTime, Space.Self);
        }
    }

    private void FixPerspective(GameObject targetObj)
    {
        //Ensures everything has been set up properly
        if (previewCamera == null || targetObj == null)
        {
            Debug.Log("Object Spin: Missing either camera or targetObj");
            return;
        }

        //Ensures that the target obj has renderer components
        Renderer[] renderers = targetObj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.Log("Object Spin: targetObj has no renderer components");
            return;
        }

        //Creates a bounding box
        Bounds bounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            //Expands the base bounding box to include all child bounds
            bounds.Encapsulate(renderer.bounds);
        }

        //Finds the biggest axis of the object
        float objectSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);

        //Calculates how far away the camera should be
        //Halves the object size to get the middle: /2f
        //Uses trig to get the tangent of the triangle: Mathf.Tan
        //Converts cam FOV from deg to rad and halves it
        float distance = objectSize / (2f * Mathf.Tan(previewCamera.fieldOfView * 0.5f * Mathf.Deg2Rad));


        Vector3 direction = previewCamera.transform.forward;
        Vector3 newPos = bounds.center - direction * distance * cameraPadding;
        previewCamera.transform.position = newPos;
    }

    
}
