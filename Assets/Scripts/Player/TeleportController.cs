using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{

    private void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SavePlayerPosition();
        }
    }

    void SavePlayerPosition()
    {
        Vector3 playerPosition = transform.position;

        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        PlayerPrefs.Save();
    }
}
