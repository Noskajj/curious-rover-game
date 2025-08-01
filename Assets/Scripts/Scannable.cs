using UnityEngine;

public class Scannable : MonoBehaviour
{
    //The main sprite used for the game
    [SerializeField]
    private GameObject regularCursor;

    //The sprite used when hovering over a scannable object
    [SerializeField]
    private GameObject scanningCursor;

    public static bool overObject = false;

    private void Update()
    {
        /*CONSIDER CHANGING IT TO ONE CURSOR OBJECT AND 
         CHANGE THE VISUALS WITH ANIMATOR TO HELP
         SIMPLIFY THE CODE
        */

        if(overObject)
        {
            regularCursor.SetActive(false);
            scanningCursor.SetActive(true);
        }
        else
        {
            regularCursor.SetActive(true);
            scanningCursor.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        //resets if not looking at scannable object
        overObject = false;
    }
}
