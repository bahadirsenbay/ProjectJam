using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;

    [Header("Jump System")]
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;

    public Transform groundCheck;
    public LayerMask groundLayer;
    Vector2 vecGravity;

    [Header("Wall Sliding System")]
    public Transform wallCheck;
    public LayerMask wallLayer;
    [SerializeField] bool isWallTouch;
    [SerializeField] bool isSliding;
    public float wallSlidingSpeed;

    [Header("Wall Sliding System")]
    public float wallJumpDuration;
    public Vector2 wallJumpForce;
    [SerializeField] bool wallJumping;


    bool isJumping;
    float jumpCounter;

    void Start()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2 (rb.velocity.x, jumpPower);
            isJumping = true;
            jumpCounter = 0;
        }

        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;

            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpCounter = 0;

            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

        isWallTouch = Physics2D.OverlapCapsule(wallCheck.position, new Vector2(0.15f, 1f), CapsuleDirection2D.Horizontal, 0, wallLayer);

        if (isWallTouch && !isGrounded() && horizontal != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;

        }

        if (isSliding)
        {
            wallJumping = true;
            Invoke("StopWallJump", wallJumpDuration);
        }

        if (wallJumping)
        {

        }
        else
        {

        }

    }

    private void FixedUpdate()
    {
        if(isSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }

    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.1f, 0.06f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    private void StopWallJump()
    {
        wallJumping = false;
    }

}
