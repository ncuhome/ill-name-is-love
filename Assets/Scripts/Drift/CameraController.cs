using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public float horizontalOffset = 1f;
    public float smoothTime = 0.05f;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }

    private void Update()
    {
        if (target != null)
        {
            if (transform.position.x > 43.5f)
            {
                if (target.position.x > 47f)
                {
                    GameManager.instance.EndGame(false);
                }
                return;
            }
            Vector3 targetPos = new Vector3(target.transform.position.x + horizontalOffset, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothTime * 100);
            
        }
        
    }

    public void CameraShake(float duration,float strength)
    {
        StopCoroutine(Shake(duration, strength));
        StartCoroutine(Shake(duration, strength));
    }

    IEnumerator Shake(float duration,float strength)
    {
        Vector3 startPos = transform.position;
        while (duration > 0)
        {
            transform.position = Random.insideUnitSphere * strength+startPos;
            duration -= Time.deltaTime;
            yield return null;
        }
        transform.position = startPos;
    }
}