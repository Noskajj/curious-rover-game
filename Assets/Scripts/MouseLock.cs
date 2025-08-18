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


    public void MouseLocked(InputAction.CallbackContext context)
    {
        MouseHasLocked();
    }

    public void MouseHasLocked()
    {
        if (Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MouseUnlocked(InputAction.CallbackContext context)
    {
        MouseHasUnlocked();
    }

    public void MouseHasUnlocked()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
