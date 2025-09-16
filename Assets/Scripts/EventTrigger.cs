using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [Header("--- Objects ---")]
    [SerializeField]
    private ScannableObject triggerObject;

    /*[Header("--- Name of Animation ---")]
    [SerializeField]*/
    private string animName = "ObjectMoved";

    private Animator animator;

    private void Awake()
    {
        if(triggerObject != null)
        {
            triggerObject.onTriggered += TriggerHandle;
        }
        animator = transform.GetComponent<Animator>();
    }

    private void TriggerHandle()
    {
        //Animate the object to move
        animator.SetBool(animName, true);
    }
}
