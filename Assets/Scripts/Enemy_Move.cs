using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Move : MonoBehaviour
{
    //通过添加[Serializable]特性确保当前类可以被实例化。
    [Serializable]
    //创建一个类获取移动的移动方向和移动次数
    public class Path
    {
        public int direction = 0;//移动方向
        //1为w，2为a，-1为s，-2为d
        public int MoveTimes;  //从上个点到该点所需的移动次数
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
                //当移动的次数大于0时让物体向下一个点移动
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

        //倒叙赋值
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

        //运动
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
