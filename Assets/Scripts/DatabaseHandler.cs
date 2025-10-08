using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class DatabaseHandler : MonoBehaviour
{
    [Header("--- Ui Objects ---")]
    [SerializeField]
    private GameObject categoryPrefab;

    [SerializeField]
    private GameObject buttonPanel, dataPanel, databasePanel;

    [SerializeField]
    private GameObject notScannedPrefab;

    [Header("--- Script Objects ---")]
    [SerializeField]
    private Database database;

    [SerializeField]
    private MouseLock mouseLock;

    [SerializeField]
    private ObjectSpin objectSpin;

    private InputAction openDatabase;

    private bool databaseOpen = false;

    /// <summary>
    /// A Dictionary of scannable Objects, sorted by the ScanZone Enum
    /// </summary>
    private Dictionary<ScanZone, List<ScannableObjectSO>> scanDictionary = new();

    private Image objectImg;
    private TMP_Text objectName;
    private TMP_Text objectDesc;

    private void Awake()
    {
        //Debug.Log("Database awake");
        //Ensures the panel is off by default
        databasePanel.SetActive(false);

        GetDataPanel();

        //Finds the input for opening the database
        openDatabase = InputSystem.actions.FindAction("OpenDatabase");

        //Subscribes the input to the function
        openDatabase.started += OpenDatabase;
    }

    private void OpenDatabase(InputAction.CallbackContext context)
    {
        if(databaseOpen)
        {
            databasePanel.SetActive(false);
            databaseOpen = false;
            DeinitializeDatabase();
            Time.timeScale = 1.0f;
            mouseLock.MouseHasLocked();
        }
        else
        {
            objectSpin.SpawnObject(notScannedPrefab);
            databasePanel.SetActive(true);
            databaseOpen = true;
            InitializeDatabase();
            Time.timeScale = 0f;
            mouseLock.MouseHasUnlocked();
        }
    }

    private void InitializeDatabase()
    {
        //Creates the dictionary
        PopulateDictionary();

        foreach (var dict in scanDictionary)
        {
            //Creates the prefab
            GameObject categoryGroupObj = Instantiate(categoryPrefab, buttonPanel.transform);
            //Gets the script
            CategoryGroup categoryGroup = categoryGroupObj.GetComponent<CategoryGroup>();
            //Sets the values in the script
            categoryGroup.Setup(dict.Key, dict.Value, this);
        }
    }

    private void DeinitializeDatabase()
    {
        foreach(Transform child in buttonPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void PopulateDictionary()
    {
        //Ensures theres no weird behaviour
        scanDictionary.Clear();

        var objects = database.GetScannableObjectList();

        foreach(var obj in objects)
        {
            //Checks to see if the category has been added yet, if not, creates a new empty list
            if(!scanDictionary.ContainsKey(obj.GetScanZone()))
            {
                scanDictionary[obj.GetScanZone()] = new List<ScannableObjectSO>();
            }

            //Adds the item into the category
            scanDictionary[obj.GetScanZone()].Add(obj);
        }
    }

    public void ShowEntry(ScannableObjectSO scannableObjectSO)
    {
        if (scannableObjectSO.hasBeenScanned)
        {
            objectSpin.SpawnObject(scannableObjectSO.GetObjectPrefab());
            objectName.text = scannableObjectSO.GetName();
            objectDesc.text = scannableObjectSO.GetDescription();
        }
        else
        {
            objectSpin.SpawnObject(notScannedPrefab);
            objectName.text = "???";
            objectDesc.text = "???";
        }
    }

    private void GetDataPanel()
    {
        GameObject[] tmpArray = new GameObject[2];
        int count = 0;
        foreach (Transform t in dataPanel.transform) 
        { 
            tmpArray[count] = t.gameObject; 
            count++;
        }

        //Gets the 2 text components in the scene, the true means that it gets them even if their component is inactive
        TMP_Text[] tmpText = tmpArray[1].GetComponentsInChildren<TMP_Text>(true);

        //Debug.Log("THIS: " + tmpArray[0].GetComponentInChildren<Image>().name);
        objectImg = tmpArray[0].transform.GetChild(0).GetComponent<Image>(); 
        objectName = tmpText[0]; 
        objectDesc = tmpText[1];
        
    }
}
