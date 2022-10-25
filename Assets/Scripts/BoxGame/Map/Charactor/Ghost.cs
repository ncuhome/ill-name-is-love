using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("地图瓦片格坐标")]
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

    private bool ghostWillMove;
    private Vector2 pos; //人物下一个FixedUpdate要走到的地方，pos.x表示x轴移动距离，pos.y表示z轴移动距离
    private int dX;
    private int dY;

    public void GhostFixedUpdate(Map map, Direction dir)
    {
        //完成本次移动再进行下次移动
        if(timer > 0)
        {
            timer--;
            Move(pos);
            return;
        }

        // if (map.WillAgainstTheWall(dir, ref x, ref y, "4"))
        // {
        //     return;
        // }

        dX = x - map.hero.x;
        dY = y - map.hero.y;
        // Debug.Log("x" + dX + "y" + dY);
        if (!(Mathf.Abs(dX) == 1 || Mathf.Abs(dY) == 1))
        {
            return;
        }
        if (!(Mathf.Abs(dX) == 0 || Mathf.Abs(dY) == 0))
        {
            return;
        }
        // Debug.Log(map.charactor[x + dX, y + dY]);

        if (map.WillAgainstTheWall(dir, map.hero.x, map.hero.y))
        {
            // Debug.Log(dir + "x" + map.hero.x + "y" + map.hero.y);
            return;
        }
        if (map.HaveTheWarrior(x + dX, y + dY))
        {
            return;
        }

        nextX = map.hero.x;
        nextY = map.hero.y;

        pos.x = 0;
        pos.y = 0;
        if (dX == 1 && dY == 0)
        {
            pos.x = defaultSpeed / TIME_SET;
            timer = TIME_SET;
        }
        else if (dX == -1 && dY == 0)
        {
            pos.x = -defaultSpeed / TIME_SET;
            timer = TIME_SET;
        }
        else if (dX == 0 && dY == -1)
        {
            pos.y = -defaultSpeed / TIME_SET;
            timer = TIME_SET;
        }
        else if (dX == 0 && dY == 1)
        {
            pos.y = defaultSpeed / TIME_SET;
            timer = TIME_SET;
        }
        // Debug.Log(pos);
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
