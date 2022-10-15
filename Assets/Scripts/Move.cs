using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private float timer=0;
    public static bool isMove = false;
    private int direction = 0;
    public static int num = 0;
    public static bool Death=false;

    //1Ϊw��2Ϊa��-1Ϊs��-2Ϊd
    void Start()
    {
        Death = false;
        num = 0;
        isMove = false;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("����" + this.gameObject.name);
            Destroy(this.gameObject);
            Death = true;
        }
        if(other.gameObject.name =="Next")
        {
            Debug.Log("��һ��" );
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

    }

  
    // Update is called once per frame
    void Update()
    {
        // ���w�����ϼ�ͷ����ǰ�ƶ�
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && LimitScope2.wall_W==false )
        {
            if (isMove == false )
            {
                direction = 1;
                isMove = true;
                num++;
            }
        }

        // ���S�����¼�ͷ�������ƶ�
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && LimitScope2.wall_S==false)
        {
            if (isMove == false)
            {
                direction = -1;
                isMove = true;
                num++;
            }
        }

        // ���A�������ͷ�������ƶ�
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && LimitScope2.wall_A==false)
        {
            if (isMove == false)
            {
                direction = 2;
                isMove = true;
                num++;
            }
        }


        // ���D�����Ҽ�ͷ�������ƶ�
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && LimitScope2.wall_D==false)
        {
            if (isMove == false)
            {
                direction = -2;
                isMove = true;
                num++;
            }
        }
 
        if (isMove && timer < 1 )
        {
            switch (direction)
            {
                case 1:
                    this.transform.Translate(Time.deltaTime, 0, 0);
                    timer += Time.deltaTime;
                    break;
                case 2:
                    this.transform.Translate(0, 0, Time.deltaTime);
                    timer += Time.deltaTime;
                    break;
                case -1:
                    this.transform.Translate(-Time.deltaTime, 0, 0);
                    timer += Time.deltaTime;
                    break;
                case -2:
                    this.transform.Translate(0, 0, -Time.deltaTime);
                    timer += Time.deltaTime;
                    break;
                default:
                    break;
            }
        }
        if (timer > 1 )
        {
            timer = 0;
            isMove = false;
        }


    }
}
