using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator animator;
    private bool activated = false;
    public global::System.Boolean Activated { get => activated; set => activated = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Activated && collision.CompareTag("Player"))
        {
            Activated = true;
            Debug.Log(animator.GetBool("activated"));
            animator.SetBool("activated", true);
            gameObject.GetComponent<CheckpointManager>().LastCheckpoint = gameObject;
            gameObject.GetComponent<CheckpointManager>().DeactivateAll();
        }
    }

      public void Deactivate()
    {
           if (animator == null)
        animator = GetComponent<Animator>();

        Activated = false;
        animator.SetBool("activated", false);
    }


}
