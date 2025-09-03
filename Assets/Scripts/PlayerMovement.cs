using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("--- Variables ---")]

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;

    private InputAction moveAction;

    private Rigidbody rb;

    private InputAction cameraActive;

    private bool isCameraActive = true;

    private void Start()
    {
        //Gets the input buttons from the input manager
        moveAction = InputSystem.actions.FindAction("Move");
        cameraActive = InputSystem.actions.FindAction("CameraSwitch");

        cameraActive.started += CameraActive;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(Time.timeScale == 1)
        {
            if (isCameraActive)
            {
                Move();
            }
            
        }
    }

    public void Move()
    {
        //Gets the current value from move action in a (0,0) format
        Vector2 moveVal = moveAction.ReadValue<Vector2>();

        //Debug.Log( moveVal)
        
        float rotateAmount = moveVal.x * rotateSpeed * Time.fixedDeltaTime;

        if (moveVal.y >= 0)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotateAmount, 0f));
        }
        else if(moveVal.y < 0)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, -rotateAmount, 0f));
        }

        //Moves the player relative to rotation
        //Updated to work for physics
        Vector3 moveDir = rb.rotation * Vector3.forward * moveVal.y * moveSpeed;
        

        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);

    }

    private void CameraActive(InputAction.CallbackContext context)
    {
        isCameraActive = !isCameraActive;
    }
}
