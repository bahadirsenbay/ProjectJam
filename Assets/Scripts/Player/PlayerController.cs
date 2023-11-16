using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        float moveX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(moveX, rb.velocity.y).normalized;

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

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

}
