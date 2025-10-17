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
        Vector3 direction = other.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(-direction);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
    }
}
