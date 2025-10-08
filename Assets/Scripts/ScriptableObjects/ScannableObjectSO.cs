using UnityEngine;

public enum ScanZone
{
    Zone1, Zone2, Zone3
}

[CreateAssetMenu(fileName = "ScannableObjectSO", menuName = "Scanning/ScannableObjectSO")]
public class ScannableObjectSO : ScriptableObject
{
    //Where all the variables are declared
    [Header("---- Settings ----")]
    [SerializeField] public bool hasBeenScanned;

    [Header("---- Details ----")]
    [SerializeField] private string objectName;
    [TextArea] [SerializeField] private string objectDesc;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ScanZone scanZone;

    public string GetName()
    {
        return objectName;
    }

    public string GetDescription()
    {
        return objectDesc;
    }

    public bool HasBeenScanned()
    {
        return hasBeenScanned;
    }

    public GameObject GetObjectPrefab()
    {
        return prefab;
    }


    public ScanZone GetScanZone()
    {
        return scanZone;
    }
}
