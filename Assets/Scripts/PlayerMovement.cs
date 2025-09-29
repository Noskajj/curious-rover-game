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
    [SerializeField]
    private float moveForce;

    [SerializeField]
    Vector3 vector;

    private InputAction moveAction;

    [SerializeField]
    private Rigidbody rb;

    private InputAction cameraActive;

    private bool onGround, movePause;
    float count = 0;

    private bool isCameraActive = true;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //Gets the input buttons from the input manager
        moveAction = InputSystem.actions.FindAction("Move");
        cameraActive = InputSystem.actions.FindAction("CameraSwitch");

        cameraActive.started += CameraActive;
       
        rb.maxLinearVelocity = moveSpeed;
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

        if(!onGround)
        {
            if(count <= 0.15f)
            {
                count += Time.fixedDeltaTime;
            }
            else
            {
                movePause = true;
            }
        }
        else
        {
            movePause = false;
            count = 0;
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
        Vector3 moveDir =  transform.forward * moveVal.y * moveSpeed;

        Debug.Log(moveDir);

        if(!movePause)
        {
            rb.AddForce(moveDir * moveForce, ForceMode.Force);
        }
        //else
        //{
        //    moveDir.y = Physics.gravity.y;
        //    rb.AddForce(moveDir * moveForce, ForceMode.Force);
        //}

        

        
        //rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Debug.Log("Drawing COM");
        Gizmos.DrawSphere(rb.worldCenterOfMass, 0.1f);
    }


    private void CameraActive(InputAction.CallbackContext context)
    {
        isCameraActive = !isCameraActive;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
