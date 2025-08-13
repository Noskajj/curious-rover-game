using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("--- Variables ---")]

    [SerializeField]
    private float moveSpeed;

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
        moveVal = moveVal * moveSpeed * Time.deltaTime;

        Debug.Log( moveVal);

        //Moves the player
        transform.position += new Vector3(moveVal.x, 0, moveVal.y);
    }
}
