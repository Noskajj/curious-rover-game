using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryGroup : MonoBehaviour
{
    [Header("--- Variables ---")]
    [SerializeField] private Button headerBtn;
    [SerializeField] private GameObject contentPnl;
    [SerializeField] private GameObject entryButtonPrefab;
    [SerializeField] private LayoutElement contentLayout;

    private bool expanded = false;
    private DatabaseHandler databaseHandler;

    public void Setup(ScanZone scanZone, List<ScannableObjectSO> items, DatabaseHandler handler)
    {
        databaseHandler = handler;

        //Sets button label
        headerBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = scanZone.ToString();

        //Gets rid of old children
        foreach (Transform child in contentPnl.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(var item in items)
        {
            var obj = Instantiate(entryButtonPrefab, contentPnl.transform);
            obj.GetComponent<DatabaseObject>().scannableObjectSO = item;
            obj.GetComponent<DatabaseObject>().Setup(handler);
        }

        contentPnl.SetActive(false);

        //Ensures no doubling up of listeners
        headerBtn.onClick.RemoveAllListeners();
        headerBtn.onClick.AddListener(Toggle);
    }

    private void Toggle()
    {
        expanded = !expanded;

        contentPnl.SetActive(expanded);

        Canvas.ForceUpdateCanvases();
    }
}