using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DialogueCamera : MonoBehaviour
{
    void Awake()
    {
        // SceneManager.sceneLoaded += Loaded;
    }

    public void Loaded()
    {
        var pauseCamera = gameObject.GetComponent<Camera>();
        Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(pauseCamera);
    }
}
