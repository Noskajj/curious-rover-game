using UnityEngine;

public class ScannableObject : MonoBehaviour
{
    [SerializeField]
    private ScannableObjectSO scannableObjectSO;

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
    }

}
