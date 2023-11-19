using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public float moveSpeed;
    private Rigidbody2D rb2d;
    float moveHorizontal;
    bool facingRight;
    public float JumpForce;
    public bool isGrounded;

    [SerializeField] public bool keepMachine;


    void Start()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CharacterMove();
        CharacterAnimation();

    }

    void CharacterMove()
    {
        
        moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        rb2d.velocity = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y);

    }

    void CharacterAnimation()
    {


        if (moveHorizontal > 0 && facingRight == true)
        {
            Flip();
        }
        if (moveHorizontal < 0 && facingRight == false)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }


}
