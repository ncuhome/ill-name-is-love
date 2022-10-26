using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeGameObjectManager : MonoBehaviour
{
    #region Singleton

    public static FadeGameObjectManager instance;

    private void Awake()
    {
        instance = this;
        if (fadeCanvas == null)
        {
            fadeCanvas = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<FadeManager>();
        }
    }

    #endregion;

    public FadeManager fadeCanvas;
}
