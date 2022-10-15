using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Move : MonoBehaviour
{
    //ͨ�����[Serializable]����ȷ����ǰ����Ա�ʵ������
    [Serializable]
    //����һ�����ȡ�ƶ����ƶ�������ƶ�����
    public class Path
    {
        public int direction = 0;//�ƶ�����
        //1Ϊw��2Ϊa��-1Ϊs��-2Ϊd
        public int MoveTimes;  //���ϸ��㵽�õ�������ƶ�����
    }

    public Path[] path = new Path[0];
    public int Id = 0;
    private float timer = 0f;
    public int moveTimes = 0;
    public static bool IsMove =false;
    Path p ;

    // Start is called before the first frame update
    void Start()
    {
        IsMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Id < path.Length)
        {
            if (moveTimes == 0) 
            {
                p = path[Id];
                moveTimes = p.MoveTimes;
            }
            if (moveTimes > 0)
            {
                //���ƶ��Ĵ�������0ʱ����������һ�����ƶ�
                if (Move.isMove && timer < 1)
                {
                    IsMove = true;
                }
                 else if (timer > 1)
                {
                    IsMove = false;
                    timer = 0;
                    --moveTimes;
                }

            }
            if (moveTimes == 0)
            {
                Id++;
            }
        }

        //����ֵ
        if (Id == path.Length)
        {
            for (Id--; Id > 0; Id--)
            {
                path[Id].direction = -path[Id].direction;
            }
            for (int i=0; i<path.Length/2; i++)
            {
                path[0] = path[i];
                path[i] = path[path.Length - 1 - i];
                path[path.Length - 1 - i] = path[i];
            }
            Id = 1;
        }

        //�˶�
        if (IsMove == true)
        {
            switch (p.direction)
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

       
    }
}
