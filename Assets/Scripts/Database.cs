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

        GlobalVar.allScannedCount = allScannableObjectsSO.Length;
        Debug.Log(allScannableObjectsSO.Length + "all scanned: " + GlobalVar.allScannedCount);
    }

    public ScannableObjectSO[] GetScannableObjectList()
    {
        return allScannableObjectsSO;
    }
}
