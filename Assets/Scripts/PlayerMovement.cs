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

    private void Start()
    {
        //Gets the input buttons from the input manager
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        //Gets the current value from move action in a (0,0) format
        Vector2 moveVal = moveAction.ReadValue<Vector2>();

        //Adjusts the movement to relative speed
        moveVal.y = moveVal.y * moveSpeed * Time.deltaTime;
        moveVal.x = moveVal.x * rotateSpeed * Time.deltaTime;

        //Debug.Log( moveVal)

        //Moves the player relative to rotation
        transform.position += transform.forward * moveVal.y;

        if(moveVal.y > 0)
        {
            transform.Rotate(0f, moveVal.x, 0f);
        }
        else if(moveVal.y < 0)
        {
            transform.Rotate(0f, -moveVal.x, 0f);
        }
        
    }
}
