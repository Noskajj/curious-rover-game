using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class MissionInfo : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string missionInfo;

    private string endText = "\n\n Press any button to continue";

    [SerializeField]
    private float textSpeed = 0.1f;

    [SerializeField]
    private TMP_Text textBox;

    [SerializeField]
    private Navigation nav;

    private bool logComplete;

    [SerializeField]
    private VideoPlayer vPlayer;

    [SerializeField]
    private VideoClip loopClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textBox.text = "";
        StartCoroutine(PrintMissionInfo());
    }

    private void Update()
    {
        if (logComplete)
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                nav.OpenGameScene();
            }
        }
    }

    IEnumerator PrintMissionInfo()
    {
        yield return new WaitForSeconds(19f);


        vPlayer.Stop();
        vPlayer.clip = loopClip;
        vPlayer.Play();
        vPlayer.isLooping = true;

        logComplete = true;

    }
}
