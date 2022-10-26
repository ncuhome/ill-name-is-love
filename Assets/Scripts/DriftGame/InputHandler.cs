using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public int HandleJoyStickInput()
    {   
        if (Input.GetButtonDown("Up"))
        {
            return 1;
        }
        else if (Input.GetButtonDown("Down"))
        {
            return -1;
        }
        return 0;
    }
}
