using UnityEngine;

[CreateAssetMenu(fileName = "ScannableObjectSO", menuName = "Scanning/ScannableObjectSO")]
public class ScannableObjectSO : ScriptableObject
{
    //Where all the variables are declared
    [Header("---- Settings ----")]
    [SerializeField] public bool hasBeenScanned;

    [Header("---- Details ----")]
    [SerializeField] private string objectName;
    [TextArea] [SerializeField] private string objectDesc;
    [SerializeField] private Sprite objectSprite;
    [SerializeField] private Sprite objectPopup;

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

    public Sprite GetObjectSprite()
    {
        return objectSprite;
    }

    public Sprite GetObjectPopup()
    {
        return objectPopup;
    }
}
