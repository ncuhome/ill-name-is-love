using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMove : MonoBehaviour
{
    //通过添加[Serializable]特性确保当前类可以被实例化。
    [Serializable]
    //创建一个类获取移动的路径点，移动时间，等待时间
    public class Path
    {
        public Transform Poitn;//路径点
        public int MoveTimes;  //从上个点到该点所需的移动次数
    }

    public Path[] path = new Path[0];
    private int Id=1;
    public Transform target;
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        //让移动的物体的位置更变为第一个点的位置
        target.position = path[0].Poitn.position;
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Id < path.Length)
        {
            Path p = path[Id];
            //当移动的时间大于0时让物体向下一个点移动
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
