using UnityEngine;

public class HPAnimationController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void DestroyAnim()
    {
        animator.SetTrigger("Destroy");
    }
}