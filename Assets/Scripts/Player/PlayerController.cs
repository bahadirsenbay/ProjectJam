using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb2d;
    float moveHorizontal;
    bool facingRight;
    public float JumpForce;
    public bool isGrounded;

    bool triggered = false;
    [SerializeField] public bool keepMachine;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        rb2d = GetComponent<Rigidbody2D>();



        if (keepMachine != true)
        {
            keepMachine = true; 
        }
    }

    void Update()
    {
        CharacterMove();
        CharacterAnimation();


        if (keepMachine)
        {
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
        
    }

    public void InteractWithMachine()
    {
        keepMachine = true;
        Debug.Log("Machine is kept.");
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


}
