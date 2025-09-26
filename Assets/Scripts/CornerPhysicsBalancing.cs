using UnityEngine;
using static UnityEngine.UI.Image;

public class CornerPhysicsBalancing : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 1f, extraForce = 50f;

    private Rigidbody cornerRb;

    private Vector3 origin, direction;

    private Vector3 vectorToApply;

    private void Start()
    {
        cornerRb = transform.GetComponent<Rigidbody>();

       // Debug.Log(cornerRb.name);
    }

    private void Update()
    {
        origin = transform.position;
        direction = -transform.up;
        ForceApplication();
        VisualiseRaycast();
    }

    private void VisualiseRaycast()
    {
        Debug.DrawRay(origin, direction * rayDistance, Color.red);
    }

    private void ForceApplication()
    {
        //Debug.Log("first check " + !Physics.Raycast(origin, direction, out RaycastHit test, rayDistance));
        Physics.Raycast(origin, direction, out RaycastHit hit, rayDistance);
        
        

        if (hit.collider == null
            || !hit.collider.CompareTag("Ground"))
        {
            this.vectorToApply = direction * extraForce * Time.fixedDeltaTime;

            cornerRb.AddForce(vectorToApply, ForceMode.Acceleration);

            Debug.Log(transform.name + " should be applying a force of " + vectorToApply);
        }
    }    
}
