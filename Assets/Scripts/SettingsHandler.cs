using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private MouseLock mouseLock;

    private InputAction openSettings;

    private bool settingsOpen;

    private void Awake()
    {
        openSettings = InputSystem.actions.FindAction("OpenSettings");

        openSettings.started += OpenSettings;
    }

    private void OpenSettings(InputAction.CallbackContext context)
    {
        if(settingsOpen)
        {
            settingsPanel.SetActive(false);
            Time.timeScale = 1.0f;
            settingsOpen = false;
            mouseLock.MouseHasLocked();
        }
        else
        {
            settingsPanel.SetActive(true);
            Time.timeScale = 0f;
            settingsOpen = true;
            mouseLock.MouseHasUnlocked();
        }
    }
}
