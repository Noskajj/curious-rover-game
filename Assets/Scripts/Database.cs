using System.Linq;
using UnityEngine;

public class Database : MonoBehaviour
{
    private ScannableObjectSO[] allScannableObjectsSO;

    private void Start()
    {
        GetAllObjects();
    }

    private void GetAllObjects()
    {
        allScannableObjectsSO = Resources.LoadAll<ScannableObjectSO>("ScriptableObjects");
        Debug.Log(allScannableObjectsSO.Length);
    }

    public ScannableObjectSO[] GetScannableObjectList()
    {
        return allScannableObjectsSO;
    }
}
