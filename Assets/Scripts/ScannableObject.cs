using UnityEngine;

public class ScannableObject : MonoBehaviour
{
    [SerializeField]
    private ScannableObjectSO scannableObjectSO;

    public ScannableObjectSO GetScannableSO()
    {
        return scannableObjectSO;
    }

}
