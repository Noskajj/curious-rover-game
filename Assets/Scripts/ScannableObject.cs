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
        Debug.Log("ScannableObject: " + scannableObjectSO.name + " is " + scannableObjectSO.hasBeenScanned);
        //Make the bool true
        scannableObjectSO.hasBeenScanned = true;

        Debug.Log("ScannableObject: " + scannableObjectSO.name + " is " + scannableObjectSO.hasBeenScanned);
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
