using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;

    float horizontal;
    bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator; // Drag Side_0's Animator here in Inspector

    private bool isJumping = false;
    private bool isGrounded = false;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Always check if grounded and update jump state
        bool groundedNow = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (groundedNow && !isGrounded)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        isGrounded = groundedNow;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (grounded && isJumping)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        return grounded;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
