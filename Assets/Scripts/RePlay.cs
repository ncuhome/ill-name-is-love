using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RePlay : MonoBehaviour
{
    [Header("�����Ҫ����ʾ��ui����")]
    [SerializeField]
    private GameObject GameOver;

    [Header("�����Ҫ�����ص�ui����")]
    [SerializeField]
    private GameObject NumCount;
    public void OnClick()
    {
        SceneManager.LoadScene(1);

    }

}

