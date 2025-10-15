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
    }
}
