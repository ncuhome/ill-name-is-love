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
    public int Id = 1;
    private float timer = 0;
    public int moveTimes = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Path p = path[Id];
        
        while (Id < path.Length)
        {
            p = path[Id];
            moveTimes = p.MoveTimes;
            
            while (moveTimes > 0)
            {
                //���ƶ���ʱ�����0ʱ����������һ�����ƶ�
                if (Move.isMove && timer < 1 )
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
                else if (timer > 1)
                {
                    timer = 0;
                    moveTimes--;
                }
            } 
            Id++;
            
        }
       
            
        for(;Id>path.Length/2;Id--)
        {
            path[0] = path[Id-1];         
            path[0].direction = - path[0].direction;        
            path[Id-1] = path[path.Length - Id +1 ];   
            path[Id-1].direction = - path[Id-1].direction;  
            path[path.Length - Id+1] = path[0];
        }
        Id = 1; 
       
    }
}
