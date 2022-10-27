using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyControl : MonoBehaviour
{
    public static DontDestroyControl instance;

    void Awake()
    {
        if (DontDestroyControl.instance == null)
        {
            DontDestroyControl.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}