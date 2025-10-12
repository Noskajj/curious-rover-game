using UnityEngine;

/// <summary>
/// This script goes onto the trigger object 
/// </summary>
public class ObjectPositionLock : MonoBehaviour
{

    [Header("--- Target Variations ---")]
    [SerializeField]
    private Vector3 targetPos;

    [SerializeField]
    private Quaternion targetRot;

    [SerializeField]
    private float snapSpeed = 5f, rotationSpeed = 5f;

    [Header("--- Walls ---")]
    [SerializeField]
    private GameObject[] invisWallsToDisable;

    [SerializeField]
    private GameObject invisWallToEnable;

    private bool isSnapping;

    private Transform pushObject;

    private void Start()
    {
        Debug.Log(this.name + ": " + transform.position);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PushableObject"))
        {
            pushObject = other.transform;
            isSnapping = true;
        }
    }

    private void Update()
    {
        if(isSnapping && pushObject != null)
        {
            pushObject.GetComponent<Rigidbody>().isKinematic = true;

            //Moves the object to the desired position in a smooth motion
            pushObject.position = Vector3.Lerp(pushObject.position, targetPos, snapSpeed * Time.deltaTime);
            pushObject.rotation = Quaternion.Lerp(pushObject.rotation,targetRot, rotationSpeed * Time.deltaTime);

            //When the object is close to the intended pos and rot, then it sets the values to the desired one and 
            if(Vector3.Distance(pushObject.position, targetPos) < 0.5f && 
                Quaternion.Angle(pushObject.rotation, targetRot) < 0.5f)
            {
                pushObject.position = targetPos;
                pushObject.rotation = targetRot;
                isSnapping = false;
                RemoveWalls();
                invisWallToEnable.SetActive(true);
                this.enabled = false;
            }
        }
    }

    private void RemoveWalls()
    { 
        if(invisWallsToDisable != null)
        {
            foreach(var wall in invisWallsToDisable)
            {
                Destroy(wall);
            }
        }
    }
}
