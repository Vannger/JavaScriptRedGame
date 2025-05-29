using System.Collections;
using UnityEngine;

 public class EnemyController : MonoBehaviour
 {
     [SerializeField] private float speedMove = 2f;
     [SerializeField] private float timeIdle = 2f;
     private Animator animator;
     private SpriteRenderer spriteRenderer;
     private bool lookRight = true;
     private bool idle = false;

     private void Start()
     {
         animator = GetComponent<Animator>();
         spriteRenderer = GetComponent<SpriteRenderer>();
         animator.SetTrigger("Run");
     }
     void Update()
     {
         if (!idle)
             transform.Translate(new Vector3(lookRight ? 1 : -1, 0, 0) * Time.deltaTime * speedMove);
     }

     void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.gameObject.tag == "EnemyEnv")
         {
             StartCoroutine(Idle());
         }
     }

     public IEnumerator Idle()
     {
         animator.SetTrigger("Idle");
         idle = true;
         yield return new WaitForSeconds(timeIdle);
         idle = false;
         lookRight = !lookRight;
         spriteRenderer.flipX = !spriteRenderer.flipX;
         animator.SetTrigger("Run");
     }
 }
