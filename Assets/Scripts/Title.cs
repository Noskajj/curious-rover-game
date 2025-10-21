using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Title : MonoBehaviour
{
    [SerializeField]
    private GameObject title, titleLoop;

    private Coroutine coroutine;
    private void Start()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(StartMenu());
    }

    IEnumerator StartMenu()
    {
        yield return new WaitForSecondsRealtime(15f);

        title.SetActive(false);
        titleLoop.SetActive(true);
    }
}
