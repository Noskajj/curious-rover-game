using System;
using UnityEngine;

public class ScannableObject : MonoBehaviour
{
    [SerializeField]
    private ScannableObjectSO scannableObjectSO;

    [SerializeField]
    private bool isTrigger;

    public event Action onTriggered;

    private void Start()
    {
        scannableObjectSO.hasBeenScanned = false;
    }

    public ScannableObjectSO GetScannableSO()
    {
        return scannableObjectSO;
    }

    public void SuccessfullyScanned()
    {
        //Make the bool true
        scannableObjectSO.hasBeenScanned = true;

        if (isTrigger)
        {
            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        //Makes it so that any scripts subscribed to this will run
        onTriggered?.Invoke();
    }

}
