using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewJumpingSystem : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    private TrailRenderer trailRenderer;
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
    [SerializeField] public Vector2 wallJumpAngle;

    [Header("Dashing System")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    private Vector2 dashingDir;
    [SerializeField] bool isDashing;
    [SerializeField] bool canDash = true;

    [SerializeField] bool doubleJump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
        wallJumpAngle.Normalize();
    }

    private void Update()
    {
        Inputs();
        CheckWorld();


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            float orginalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing(orginalGravity));
        }

        if (isDashing)
        {
            rb.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        if (isGrounded || isWallSliding)
        {
            canJump = true;
            canDash = true;
        }else if (!isGrounded || !isWallSliding)
        {
            canJump = false;
        }

    }

    private IEnumerator StopDashing(float orginalGravity)
    {
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = orginalGravity;
        trailRenderer.emitting = false;
        isDashing = false;
    }
    private void FixedUpdate()
    {
        Movement();
        WallSlide();
        AnimationControl();
    }
    private void Inputs()
    {
        XDirectionalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
        else if (!isGrounded && !isWallSliding && !isDashing && XDirectionalInput != 0)
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
        if (canJump)
        {
            doubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (doubleJump)
        {
            doubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if ((isWallSliding || isTouchingWall) && canJump)
        {
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
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