using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMove : MonoBehaviour
{
    //ͨ�����[Serializable]����ȷ����ǰ����Ա�ʵ������
    [Serializable]
    //����һ�����ȡ�ƶ���·���㣬�ƶ�ʱ�䣬�ȴ�ʱ��
    public class Path
    {
        public Transform Poitn;//·����
        public int MoveTimes;  //���ϸ��㵽�õ�������ƶ�����
    }

    public Path[] path = new Path[0];
    private int Id=1;
    public Transform target;
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        //���ƶ��������λ�ø���Ϊ��һ�����λ��
        target.position = path[0].Poitn.position;
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Id < path.Length)
        {
            Path p = path[Id];
            //���ƶ���ʱ�����0ʱ����������һ�����ƶ�
            if (Move.isMove && timer < 1)
            {
                this.transform.Translate( (path[Id].Poitn.position-path[Id-1].Poitn.position)* Time.deltaTime);
                timer += Time.deltaTime;
            }
           
            
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < path.Length - 1; i++)
        {
            if (path[i].Poitn && path[i + 1].Poitn)
            {
                Gizmos.DrawLine(path[i].Poitn.position, path[i + 1].Poitn.position);
            }
        }
    }
}
