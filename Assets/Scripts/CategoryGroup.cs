using NUnit.Framework;
using System.Collections;
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

    private float height;

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

        //Changing layout size
        StartCoroutine(MeasureHeight());

        //Ensures no doubling up of listeners
        headerBtn.onClick.RemoveAllListeners();
        headerBtn.onClick.AddListener(Toggle);
    }

    private IEnumerator MeasureHeight()
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        height = CalculateHeight();
        Debug.Log(height);
        contentLayout.preferredHeight = 0;
        expanded = false;
    }

    private float CalculateHeight()
    {
        float totalHeight = 0;
        VerticalLayoutGroup vlg = contentPnl.GetComponent<VerticalLayoutGroup>();
        float spacing = vlg ? vlg.spacing : 0;

        int count = 0;

        foreach (Transform child in contentPnl.transform)
        {
            LayoutElement layoutElement = child.GetComponent<LayoutElement>();
            if(layoutElement != null)
            {
                totalHeight += layoutElement.preferredHeight;
            }
            else
            {
                totalHeight += child.GetComponent<RectTransform>().rect.height;
            }

            if(count < contentPnl.transform.childCount - 1)
            {
                totalHeight += spacing;
            }

            count++;
        }
        

        return totalHeight;
    }

    private void Toggle()
    {
        expanded = !expanded;

        if(expanded)
        {
            contentLayout.preferredHeight = height;
        }
        else
        {
            contentLayout.preferredHeight = 0;
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.parent as RectTransform);

    }
}