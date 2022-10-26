using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("地图瓦片格坐标(是二维数组坐标)")]
    public int x;
    public int y;
    public int nextX;
    public int nextY;
    [Header("Movement Attribute")]
    public int TIME_SET; //移动一格需要的时间（除以每秒的物理帧数是秒数）
    [Header("移动速度")]
    public float defaultSpeed = 1f;
    [Header("Movement Timer")]
    public int timer; //剩余移动物理帧数

    private Vector2 pos; //人物下一个FixedUpdate要走到的地方，pos.x表示x轴移动距离，pos.y表示z轴移动距离

    public void HeroFixedUpdate(Map map, Direction dir)
    {
        //完成本次移动再进行下次移动
        if(timer > 0)
        {
            // Debug.Log(pos);
            timer--;
            Move(pos);
            return;
        }

        if (map.WillAgainstTheWall(dir, x, y, ref nextX, ref nextY))
        {
            return;
        }

        pos.x = 0;
        pos.y = 0;
        switch (dir)
        {
            case Direction.Up:
                pos.x = defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Down:
                pos.x = -defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Left:
                pos.y = defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Right:
                pos.y = -defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
        }
    }

    public void Move(Vector3 pos)
    {
        Vector3 newPos = new(transform.position.x + pos.x, transform.position.y, transform.position.z + pos.y);
        transform.position = newPos;
    }

    public void TransformXY()
    {
        x = nextX;
        y = nextY;
    }
}
