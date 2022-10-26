using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour
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

    private Vector2 pos; //人物下一个FixedUpdate要走到的地方，pos.x表示x轴移动距离，pos.y表示z轴移动距离
    private int dX;
    private int dY;

    public void TigerFixedUpdate(Map map, Direction dir)
    {
        //完成本次移动再进行下次移动
        if(timer > 0)
        {
            // Debug.Log(pos);
            timer--;
            Move(pos);
            return;
        }

        dX = x - map.hero.x;
        dY = y - map.hero.y;

        if (!(dX == 0 || dY == 0))
        {
            return;
        }
        // Debug.Log("dY" + dY);

        int tX = x - map.hero.nextX;
        int tY = y - map.hero.nextY;
        int moveTimes = 0;
        if (tX == 0 || tY == 0)
        {
            if (tX == 0 && tY == 0)
            {
                return;
            }
            if (tX == 0)
            {
                int temp = Mathf.Abs(tY);
                if (tY > 0)
                {
                    dir = Direction.Left;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (moveTimes == temp)
                        {
                            break;
                        }
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextY--;
                        moveTimes++;
                    }
                }
                else
                {
                    dir = Direction.Right;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (moveTimes == temp)
                        {
                            break;
                        }
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextY++;
                        moveTimes++;
                    }
                }
            }
            if (tY == 0)
            {
                int temp = Mathf.Abs(tX);
                if (tX > 0)
                {
                    dir = Direction.Up;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (moveTimes == temp)
                        {
                            break;
                        }
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextX--;
                        moveTimes++;
                    }
                }
                else
                {
                    dir = Direction.Down;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (moveTimes == temp)
                        {
                            break;
                        }
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextX++;
                        moveTimes++;
                    }
                }
            }
        }
        else
        {
            if (dX == 0)
            {
                if (dY > 0)
                {
                    dir = Direction.Left;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextY--;
                        moveTimes++;
                    }
                }
                else
                {
                    dir = Direction.Right;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextY++;
                        moveTimes++;
                    }
                }
            }
            else
            {
                if (dX > 0)
                {
                    dir = Direction.Up;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextX--;
                        moveTimes++;
                    }
                }
                else
                {
                    dir = Direction.Down;
                    while (!map.WillAgainstTheWall(dir, nextX, nextY))
                    {
                        if (map.AgainstTheThief(dir, nextX, nextY))
                        {
                            break;
                        }
                        nextX++;
                        moveTimes++;
                    }
                }
            }
        }

        pos.x = 0;
        pos.y = 0;

        switch (dir)
        {
            case Direction.Up:
                pos.x = defaultSpeed * moveTimes / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Down:
                pos.x = -defaultSpeed * moveTimes/ TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Left:
                pos.y = defaultSpeed * moveTimes/ TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Right:
                pos.y = -defaultSpeed * moveTimes/ TIME_SET;
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
