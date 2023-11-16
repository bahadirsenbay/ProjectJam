using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionLoader : MonoBehaviour
{
    void Start()
    {
        LoadPlayerPosition();
    }

    void LoadPlayerPosition()
    {
        // Kaydedilmi� konumu PlayerPrefs'ten y�kle
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");
        float playerZ = PlayerPrefs.GetFloat("PlayerZ");

        // Y�klenen konumu karakterin pozisyonuna ata
        transform.position = new Vector3(playerX, playerY, playerZ);
    }
}
