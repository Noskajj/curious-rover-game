using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseObject : MonoBehaviour
{
    public ScannableObject scannableObject;

    public GameObject dataPanel;

    private Button thisBtn;

    private Image objectImg;

    private TMP_Text objectName, objectDesc;

    private void Start()
    {
        thisBtn = GetComponent<Button>();

        thisBtn.onClick.AddListener(OnButtonClick);

        GameObject[] tmpArray = new GameObject[2];
        int count = 0;

        foreach(Transform t in dataPanel.transform)
        {
            tmpArray[count] = t.gameObject;
            count++;
        }

        objectImg = tmpArray[0].GetComponentInChildren<Image>();
        objectName = tmpArray[0].GetComponentInChildren<TMP_Text>();
        objectDesc = tmpArray[1].GetComponentInChildren<TMP_Text>();
    }

    public void OnButtonClick()
    {
        //Set the data on data panel to this buttons data
        //Consider changing outline or colour of selected button

        objectImg.sprite = scannableObject.GetScannableSO().GetObjectSprite();
        objectName.text = scannableObject.GetScannableSO().GetName();
        objectDesc.text = scannableObject.GetScannableSO().GetDescription();
    }
}
