using UnityEngine;
using UnityEngine.InputSystem;

public class DatabaseHandler : MonoBehaviour
{
    [Header("--- Ui Objects ---")]
    [SerializeField]
    private GameObject dataPrefab;

    [SerializeField]
    private GameObject buttonPanel, dataPanel, databasePanel;

    [Header("--- Script Objects ---")]
    [SerializeField]
    private Database database;

    [SerializeField]
    MouseLock mouseLock;

    private InputAction openDatabase;

    private bool databaseOpen = false;

    private void Start()
    {
        //Ensures the panel is off by default
        databasePanel.SetActive(false);

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
            Time.timeScale = 1;
            mouseLock.MouseHasLocked();
        }
        else
        {
            databasePanel.SetActive(true);
            databaseOpen = true;
            InitializeDatabase();
            mouseLock.MouseHasUnlocked();
            Time.timeScale = 0;
        }
    }

    private void InitializeDatabase()
    {
        foreach (ScannableObjectSO scan in database.GetScannableObjectList())
        {
                InitializeDatabaseObject(scan);
        }
    }

    private void InitializeDatabaseObject(ScannableObjectSO scannableObject)
    {
        //Instantiate the button
        GameObject databaseBtn = Instantiate(dataPrefab, buttonPanel.transform);
        
        DatabaseObject tmpObj = databaseBtn.GetComponent<DatabaseObject>();

        tmpObj.scannableObjectSO = scannableObject;
        tmpObj.dataPanel = dataPanel;
        
    }

    private void DeinitializeDatabase()
    {
        foreach(Transform child in buttonPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
