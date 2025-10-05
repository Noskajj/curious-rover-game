using System.Collections;
using System.Drawing;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class CameraLook : MonoBehaviour
{
    [Header("--- Variables ---")]

    [SerializeField]
    private float mouseSensX;
    [SerializeField]
    private float mouseSensY;

    [SerializeField]
    private GameObject scanOverlay;

 /*   [SerializeField]
    private float clampUp, clampBottom;*/

    private float rotX, rotY;

    [SerializeField]
    private CinemachineCamera mainCamera, firstPersonCamera;

    private InputAction mouseLook;
    private InputAction cameraActive;

    public static bool isCameraActive = false;

    private Vector3 mainCamLastPos;

    private UnityEngine.Color colour = UnityEngine.Color.aliceBlue;

    private Coroutine overlayFadeRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera.Priority = 20;
        firstPersonCamera.Priority = 10;

        mouseLook = InputSystem.actions.FindAction("Look");
        cameraActive = InputSystem.actions.FindAction("CameraSwitch");

        cameraActive.started += CameraSwitched;
    }

    private void OnDisable()
    {
        cameraActive.started -= CameraSwitched;
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

        if(overlayFadeRoutine != null)
        {
            StopCoroutine(overlayFadeRoutine);
        }

        if (isCameraActive)
        {
            SetForward();
            mainCamera.Priority = 10;
            firstPersonCamera.Priority = 20;

            //Consider fade in
            
            overlayFadeRoutine = StartCoroutine(OverlayFade(0f, 1f, 1f));
        }
        else
        {
            mainCamera.Priority = 20;
            firstPersonCamera.Priority = 10;
            SetForward();

            //Consider fade out

            overlayFadeRoutine = StartCoroutine(OverlayFade(1f, 0f, 0.3f));
        }
    }

    private void SetForward()
    {
        Vector3 forward = transform.GetComponentInParent<Transform>().forward;
        Quaternion targetRotation = Quaternion.LookRotation(forward);

        firstPersonCamera.transform.rotation = targetRotation;

        Vector3 euler = targetRotation.eulerAngles;
        rotX = euler.x;
        rotY = euler.y;

    }
    

    IEnumerator OverlayFade(float startVal, float endVal, float fadeTime)
    {
        float timeElapsed = 0f;
        colour = scanOverlay.GetComponent<Image>().color;

        while (timeElapsed < fadeTime)
        {
            timeElapsed += Time.deltaTime;
            float timer = timeElapsed / fadeTime;

            colour.a = Mathf.Lerp(startVal, endVal, timer);
            scanOverlay.GetComponent<Image>().color = colour;
            yield return null;
        }

        colour.a = endVal;
        scanOverlay.GetComponent<Image>().color = colour;
    }
}
