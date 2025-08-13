using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("--- Variables ---")]

    [SerializeField]
    private float mouseSens;

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

        Debug.Log(mouseLookVal);
    }
}
