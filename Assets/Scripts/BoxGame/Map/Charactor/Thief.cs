using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
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
    [Header("规定路线")]
    public Path [] paths;

    private int pathIndex = 0;
    private int pathCurTimes = 0;
    private Vector2 pos; //人物下一个FixedUpdate要走到的地方，pos.x表示x轴移动距离，pos.y表示z轴移动距离

    private void Start()
    {
        pathCurTimes = paths[pathIndex].MoveTimes;    
    }

    public void ThiefFixedUpdate(Map map, Direction dir)
    {
        //完成本次移动再进行下次移动
        if(timer > 0)
        {
            // Debug.Log(pos);
            timer--;
            Move(pos);
            return;
        }

        pos.x = 0;
        pos.y = 0;

        if (map.WillAgainstTheWall(paths[pathIndex].direction, x, y))
        {
            Direction tDir = paths[pathIndex].direction;
            while (paths[pathIndex].direction == tDir)
            {
                pathCurTimes--;
                if (pathCurTimes == 0)
                {
                    pathIndex = (pathIndex + 1) % paths.Length;
                    pathCurTimes = paths[pathIndex].MoveTimes;
                }
            }
        }
        
        if (map.WillAgainstTheHero(paths[pathIndex].direction, x, y))
        {
            switch (paths[pathIndex].direction)
            {
                case Direction.Up:
                if (dir == Direction.Down)
                {
                    map.isDieByThief = true;
                }
                break;
            case Direction.Down:
                if (dir == Direction.Up)
                {
                    map.isDieByThief = true;
                }
                break;
            case Direction.Left:
                if (dir == Direction.Right)
                {
                    map.isDieByThief = true;
                }
                break;
            case Direction.Right:
                if (dir == Direction.Left)
                {
                    map.isDieByThief = true;
                }
                break;
            }
        }

        switch (paths[pathIndex].direction)
        {
            case Direction.Up:
                nextX--;
                pos.x = defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Down:
                nextX++;
                pos.x = -defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Left:
                nextY--;
                pos.y = defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
            case Direction.Right:
                nextY++;
                pos.y = -defaultSpeed / TIME_SET;
                timer = TIME_SET;
                break;
        }
        pathCurTimes--;
        if (pathCurTimes == 0)
        {
            pathIndex = (pathIndex + 1) % paths.Length;
            pathCurTimes = paths[pathIndex].MoveTimes;
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
