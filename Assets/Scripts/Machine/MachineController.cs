using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    public PlayerController playerController;
    bool triggered = false;


    void Update()
    {
        Debug.Log(playerController.keepMachine);
        if (Input.GetKeyDown(KeyCode.R) && triggered)
        {
            playerController.keepMachine = true;
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
