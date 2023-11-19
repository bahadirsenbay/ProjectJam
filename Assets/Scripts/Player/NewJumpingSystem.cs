using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJumpingSystem : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float airMoveSpeed = 30f;
    private float XDirectionalInput;
    private bool facingRight = true;
    private bool isMoving;


    [Header("Jumping System")]
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canJump;

    [Header("Wall Sliding System")]
    [SerializeField] float wallSlideSpeed = 0;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    [SerializeField] private bool isTouchingWall;
    [SerializeField] private bool isWallSliding;

    [Header("Wall Jump System")]
    [SerializeField] float wallJumpForce = 18f;
    [SerializeField] float wallJumpDirection = -1f;
    [SerializeField] Vector2 wallJumpAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wallJumpAngle.Normalize();
    }

    private void Update()
    {
        Inputs();
        CheckWorld();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        WallSlide();
        WallJump();
        AnimationControl();
    }
    private void Inputs()
    {
        XDirectionalInput = Input.GetAxisRaw("Horizontal");
        if (isGrounded || isWallSliding)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canJump = true;
            }
        }

    }

    private void CheckWorld()
    {
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    private void Movement()
    {
        //for Animation
        if (XDirectionalInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //this is for movement
        if (isGrounded)
        {
            rb.velocity = new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);
        }
        else if (!isGrounded && !isWallSliding && XDirectionalInput != 0)
        {
            rb.AddForce(new Vector2(airMoveSpeed * XDirectionalInput, 0));
            if (Mathf.Abs(rb.velocity.x) > moveSpeed)
            {
                rb.velocity = new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);
            }
        }

        //this is to flip to player
        if (XDirectionalInput < 0 && facingRight)
        {
            Flip();
        }
        else if (XDirectionalInput > 0 && !facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (!isWallSliding)
        {
            wallJumpDirection *= -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    private void Jump()
    {
        if (canJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }

    private void WallSlide()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        //Wall Slide

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
        }

    }

    private void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && canJump)
        {
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canJump = false;

        }
    }

    void AnimationControl()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRunning", isMoving);
        animator.SetBool("isSliding", isWallSliding);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
}
