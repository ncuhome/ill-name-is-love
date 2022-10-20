using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public RiverController riverController;
    public HealthBar healthBar;
    public float endY = -5f;
    public float zBuffer = 1.5f;

    private float startY = 0;
    private float startZ = 0;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        startY = pos.y;
        startZ = pos.z;   
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        float percent = (startY - pos.y) / (startY - endY);
        percent = Mathf.Clamp(percent, 0, 1);
        pos.z = startZ - percent * zBuffer;
        transform.position = pos;
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         riverController.MoveAnother();
    //     }    
    //     if (other.gameObject.tag == "HealthBar")
    //     {
    //         riverController.MoveAnother();
    //         healthBar.
    //     }
    // }
}
