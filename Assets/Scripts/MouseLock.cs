using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLock : MonoBehaviour
{
    private InputAction mouseLock;
    private InputAction mouseUnlock;

    private void Start()
    {
        mouseLock = InputSystem.actions.FindAction("MouseLock");
        mouseUnlock = InputSystem.actions.FindAction("MouseUnlock");
        Cursor.lockState = CursorLockMode.Locked;

        mouseLock.started += MouseLocked;
        mouseUnlock.started += MouseUnlocked;
    }

    private void Update()
    {
   
    }

    private void MouseLocked(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void MouseUnlocked(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
