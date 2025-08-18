using System.Linq;
using UnityEngine;

public class Database : MonoBehaviour
{
    [SerializeField]
    private ScannableObject[] allScannableObjects;

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

    public ScannableObject[] GetScannableObjectList()
    {
        return allScannableObjects;
    }
}
