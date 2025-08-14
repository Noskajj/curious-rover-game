using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("--- Variables ---")]

    [SerializeField]
    private float mouseSensX;
    [SerializeField]
    private float mouseSensY;

    [SerializeField]
    private float clampUp, clampBottom;

    [SerializeField]
    private float rotX, rotY;

    private InputAction mouseLook;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseLook = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector2 mouseLookVal = mouseLook.ReadValue<Vector2>();

        //Debug.Log(mouseLookVal);

        rotX = mouseLookVal.x * mouseSensX *Time.deltaTime;
        rotY = mouseLookVal.y * mouseSensY * Time.deltaTime;

        
    }

    private void ClampRotation(float valToClamp)
    {

    }
}
