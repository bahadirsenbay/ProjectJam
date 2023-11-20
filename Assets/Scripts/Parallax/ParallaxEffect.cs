using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    public float ParallaxEf;
    public float offset;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float temp = (cam.transform.position.x * (1 - ParallaxEf));
        float dist = (cam.transform.position.x * ParallaxEf);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + (length - offset))
        {
            startPos += length;
        }
        else if (temp < startPos - (length - offset))
        {
            startPos -= length;
        }
    }
}
