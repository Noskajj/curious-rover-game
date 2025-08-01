using UnityEngine;

public class MouseLock : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Locks the mouse when the mouse is clicked
        if(Input.GetMouseButtonDown(0) && Time.timeScale > 0.0f)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        //Unlocks the mouse when the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState= CursorLockMode.None;
        }
    }
}
