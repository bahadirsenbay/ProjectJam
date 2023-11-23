using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutScene : MonoBehaviour
{
    public Animator animator;
    public static bool isCutsceneOn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCutsceneOn = true;
            animator.SetBool("cutScene1", true);
            Invoke(nameof(stopCutScene), 3f);
        }
    }

    private void stopCutScene()
    {
        isCutsceneOn = false;
        animator.SetBool("cutScene1", false);
        Destroy(gameObject);
    }
}
