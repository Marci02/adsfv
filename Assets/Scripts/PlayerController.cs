using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 7;
    [SerializeField] private float jumpHeight = 150;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool isFacingRight;
    private bool isGrounded;

    private void Start()
    {
        isFacingRight = true;
        isGrounded = false;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            rigidbody2d.AddForce(new(0, jumpHeight));
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2d.velocity = new(
            x: move * maxSpeed,
            y: rigidbody2d.velocity.y);

        animator.SetFloat("speed", Mathf.Abs(move));
        if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight)) Flip();

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, .1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", rigidbody2d.velocity.y);
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;

        transform.localScale = new(
            x: transform.localScale.x * -1,
            y: transform.localScale.y,
            z: transform.localScale.z);
    }
}
