using UnityEngine;

public class EvenTrigger : MonoBehaviour
{
    [SerializeField]
    private ScannableObject triggerObject;

    [SerializeField]
    private GameObject objectToDestroy;

    private void Start()
    {
        if(triggerObject != null)
        {
            triggerObject.onTriggered += TriggerHandle;
        }
    }

    private void TriggerHandle()
    {
        Destroy(objectToDestroy);
    }
}
