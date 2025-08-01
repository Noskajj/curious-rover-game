using UnityEngine;

[CreateAssetMenu(fileName = "ScannableObjectSO", menuName = "Scanning/ScannableObjectSO")]
public class ScannableObjectSO : ScriptableObject
{
    //Where all the variables are declared
    [Header("---- Settings ----")]
    [SerializeField] private bool hasBeenScanned;

    [Header("---- Details ----")]
    [SerializeField] private string objectName;
    [TextArea] [SerializeField] private string objectDesc;
    [SerializeField] private Sprite objectSprite;


    public string GetName()
    {
        return objectName;
    }
}
