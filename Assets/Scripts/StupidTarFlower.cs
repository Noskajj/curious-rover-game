using Unity.VisualScripting;
using UnityEngine;

public class StupidTarFlower : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    private void OnTriggerEnter(Collider other)
    {
        //Opens the flower
        animator.SetBool("TarFlowerOpen", true);
    }

    private void OnTriggerExit(Collider other)
    {
        //Closes the flower
        animator.SetBool("TarFlowerOpen", false);
    }

    private void OnTriggerStay(Collider other)
    {
        //Points the flower at the player (use lerp)
        Vector3 direction = other.transform.position;
        direction.y = -direction.y;

        
        //transform.LookAt(Mathf.Lerp(transform.rotation, direction, Time.deltaTime));
    }
}
