using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float horizontalOffset = 1f;
    public float smoothTime = 0.05f;

    private void Update()
    {
        Vector3 targetPos = new Vector3(target.transform.position.x + horizontalOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothTime * 100);
    }
}