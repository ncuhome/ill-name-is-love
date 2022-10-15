using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GameManger : MonoBehaviour
{
    [Header("�����Ҫ����ʾ��ui����")]
    [SerializeField]
    private GameObject GameOver;

    [Header("�����Ҫ�����ص�ui����")]
    [SerializeField]
    private GameObject NumCount;


    // Start is called before the first frame update
    void Start()
    {
        GameOver.SetActive(false);
        NumCount.SetActive(true );
        Move.Death = false;
        Enemy_Move.IsMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Move.Death == true)
        {
            GameOver.SetActive(true);
            NumCount.SetActive(false);
        }

    }
}
