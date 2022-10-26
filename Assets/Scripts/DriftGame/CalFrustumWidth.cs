using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalFrustumWidth : MonoBehaviour
{
    public float z;
    public float ans;

    // Update is called once per frame
    void Update()
    {
        float distance = z - Camera.main.transform.position.z;
        float frustumHeight = 2.0f * distance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        ans = frustumHeight * Camera.main.aspect;
    }
}
