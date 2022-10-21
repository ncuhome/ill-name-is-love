using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleComponent : MonoBehaviour
{
    private Camera cam;
    private float length;
    private float startGap;

    private void Start()
    {
        cam = Camera.main;
        startGap = cam.transform.position.x - transform.position.x;
        float distance = transform.position.z - cam.transform.position.z;
        float frustumHeight = 2.0f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        length = frustumHeight * cam.aspect;
    }

    private void FixedUpdate()
    {
        // float temp = (cam.transform.position.x * (1 - parallaxEffect));
        // float dist = (cam.transform.position.x * parallaxEffect);

        if (cam.transform.position.x - transform.position.x > startGap + length)
        {
            transform.position = new Vector3(cam.transform.position.x - startGap, transform.position.y, transform.position.z);
        }
        else if (transform.position.x - cam.transform.position.x > startGap + length)
        {
            transform.position = new Vector3(cam.transform.position.x - startGap, transform.position.y, transform.position.z);
        }
    }

}
