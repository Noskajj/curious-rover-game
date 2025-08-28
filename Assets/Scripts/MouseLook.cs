using Unity.Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [Header("--- Variables ---")]

    [SerializeField]
    private float mouseSensX;
    [SerializeField]
    private float mouseSensY;

 /*   [SerializeField]
    private float clampUp, clampBottom;*/

    private float rotX, rotY;

    [SerializeField]
    private CinemachineCamera mainCamera, firstPersonCamera;

    private InputAction mouseLook;
    private InputAction cameraActive;

    private bool isCameraActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseLook = InputSystem.actions.FindAction("Look");
        cameraActive = InputSystem.actions.FindAction("CameraSwitch");

        cameraActive.started += CameraSwitched;

        Vector3 forward = transform.GetComponentInParent<Transform>().forward;
        Quaternion targetRotation = Quaternion.LookRotation(forward);

        firstPersonCamera.transform.rotation = targetRotation;

        Vector3 euler = targetRotation.eulerAngles;
        rotX = euler.x;
        rotY = euler.y;

      
    }


    // Update is called once per frame
    void Update()
    {
        if (isCameraActive)
        {
            MoveCamera();
        }
        
    }

    private void MoveCamera()
    {
        Vector2 mouseLookVal = mouseLook.ReadValue<Vector2>();

        //Debug.Log(mouseLookVal);

        rotX -= mouseLookVal.y * mouseSensX *Time.deltaTime;
        rotY += mouseLookVal.x * mouseSensY * Time.deltaTime;

       /* rotX = Mathf.Clamp(rotX, -90f, 90f);
        rotY = Mathf.Clamp(rotY, -90f, 90f);*/

        transform.rotation = Quaternion.Euler(rotX, rotY, 0f);
    }

    private void CameraSwitched(InputAction.CallbackContext context)
    {
        isCameraActive = !isCameraActive;
        Debug.Log("switch cameras");
        if (isCameraActive)
        {
            mainCamera.enabled = false;
            firstPersonCamera.enabled = true;
        }
        else
        {
            mainCamera.enabled = true;
            firstPersonCamera.enabled = false;
        }
    }
}
