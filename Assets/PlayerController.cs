using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool playing = true;
    private bool lookRight;
    private SpriteRenderer spriteRenderer;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Animator animator;
    public bool Playing { get => playing; set => playing = value; }
    public bool LookRight { get => lookRight; set => lookRight = value; }
    public bool IsFacingLeft => spriteRenderer.flipX;
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lookRight = true;
    }

    void Update()
    {

        if (playing) {
        // Проверка, стоит ли игрок на земле
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Движение влево-вправо
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        animator.SetFloat("HSpeed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("VSpeed", rb.linearVelocity.y);
        animator.SetBool("isGround", isGrounded);
        {
            if (moveX > 0)
                {
                transform.localScale = new Vector3(1, 1, 1);
                Flip();
                }
            else if (moveX < 0)
                {
                transform.localScale = new Vector3(-1, 1, 1);
                Flip();
                }
        }
        }
    }
private void Flip()
    {
        lookRight = !lookRight;
    }


    // Отображение зоны проверки "земли" в сцене
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Money"))
            {
                Destroy(other.gameObject, 0.3f);
            }
        }
}
