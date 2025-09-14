using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseObject : MonoBehaviour
{
    public ScannableObjectSO scannableObjectSO;

    public DatabaseHandler databaseHandler;

    private Button thisBtn;

    private Image objectImg;

    private TMP_Text objectName, objectDesc, buttonName;

    public void Setup(DatabaseHandler databaseHandler)
    {
        this.databaseHandler = databaseHandler;
        thisBtn = GetComponent<Button>();

        buttonName = GetComponentInChildren<TMP_Text>();

        thisBtn.onClick.AddListener(OnButtonClick);

        GameObject[] tmpArray = new GameObject[2];

        if (scannableObjectSO.hasBeenScanned)
        {
            buttonName.text = scannableObjectSO.GetName();
        }
        else
        {
            buttonName.text = "???";
        }
    }

    public void OnButtonClick()
    {
        //Set the data on data panel to this buttons data
        //Consider changing outline or colour of selected button
        databaseHandler.ShowEntry(scannableObjectSO);
    }
}
