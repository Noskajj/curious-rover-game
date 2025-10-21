using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    [SerializeField]
    private MouseLock mouseLock;

    private InputAction openSettings, qKey;

    private bool settingsOpen;

    private void Awake()
    {
        openSettings = InputSystem.actions.FindAction("OpenSettings");

        qKey = InputSystem.actions.FindAction("OpenDatabase");

        openSettings.started += OpenSettings;

        qKey.started += QKeyPressed;
    }

    private void OnDisable()
    {
        openSettings.started -= OpenSettings;

        qKey.started -= QKeyPressed;
    }

    private void OpenSettings(InputAction.CallbackContext context)
    {
        if(settingsOpen)
        {
            settingsPanel.SetActive(false);
            Time.timeScale = 1.0f;
            settingsOpen = false;
            mouseLock.MouseHasLocked();
            Settings.Instance.UpdateSettings();
        }
        else
        {
            settingsPanel.SetActive(true);
            Time.timeScale = 0f;
            settingsOpen = true;
            mouseLock.MouseHasUnlocked();
        }
    }

    private void QKeyPressed(InputAction.CallbackContext context)
    {
        if (settingsOpen)
        {
            settingsPanel.SetActive(false);
            settingsOpen = false;
            mouseLock.MouseHasLocked();
            Settings.Instance.UpdateSettings();
        }
    }
}
