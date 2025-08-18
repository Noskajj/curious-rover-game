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
            Time.timeScale = 1;
        }
        else
        {
            databasePanel.SetActive(true);
            databaseOpen = true;
            InitializeDatabase();
            Time.timeScale = 0;
        }
    }

    private void InitializeDatabase()
    {
        foreach (ScannableObject scan in database.GetScannableObjectList())
        {
            if(scan.GetScannableSO().HasBeenScanned())
            {
                InitializeDatabaseObject(scan);
            }
        }
    }

    private void InitializeDatabaseObject(ScannableObject scannableObject)
    {
        //Instantiate the button
        GameObject databaseBtn = Instantiate(dataPrefab, buttonPanel.transform);
        
        DatabaseObject tmpObj = databaseBtn.GetComponent<DatabaseObject>();

        tmpObj.scannableObject = scannableObject;
        tmpObj.dataPanel = dataPanel;
        
    }
}
