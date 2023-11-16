using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovements : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb2d;
    float moveHorizontal;
    bool facingRight;
    public float JumpForce;
    public bool isGrounded;


    void Start()
    {
        PlayerPrefs.DeleteAll();
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CharacterMove();
        CharacterAnimation();
        CharacterJump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (SceneManager.GetActiveScene().name == "DayMap")
            {
                SceneManager.LoadScene("NightMap");

            }
            else
            {
                SceneManager.LoadScene("DayMap");

            }
        }
    }

    void CharacterMove()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
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

    void CharacterJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isGrounded)
            {
                rb2d.velocity = Vector2.up * JumpForce;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Grounded")
        {
            isGrounded = false;
        }
    }


}
