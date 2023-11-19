using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueTrigger.TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
