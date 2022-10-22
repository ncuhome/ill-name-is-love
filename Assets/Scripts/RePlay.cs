using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RePlay : MonoBehaviour
{
    [Header("获得需要被显示的ui界面")]
    [SerializeField]
    private GameObject GameOver;

    [Header("获得需要被隐藏的ui界面")]
    [SerializeField]
    private GameObject NumCount;
    public void OnClick()
    {
        SceneManager.LoadScene(1);

    }

}

