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
        // Kaydedilmiþ konumu PlayerPrefs'ten yükle
        float playerX = PlayerPrefs.GetFloat("PlayerX");
        float playerY = PlayerPrefs.GetFloat("PlayerY");
        float playerZ = PlayerPrefs.GetFloat("PlayerZ");

        // Yüklenen konumu karakterin pozisyonuna ata
        transform.position = new Vector3(playerX, playerY, playerZ);
    }
}
