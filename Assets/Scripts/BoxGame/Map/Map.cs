using System.Collections;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Direction
{
    Idle = -1,
    Right,
    Up,
    Left,
    Down
}

[Serializable]
//创建一个类获取移动的移动方向和移动次数
public class Path
{
    public Direction direction = Direction.Idle;//移动方向
    public int MoveTimes;  //从上个点到该点所需的移动次数
}

public class Map : MonoBehaviour
{
    [Header("地图相关文件")]
    public TextAsset map; //记录石头

    [Header("人物脚本")]
    public Hero hero; //男主信息
    public Ghost [] ghosts; //鬼魂信息
    public Thief [] thiefs; //鬼魂信息
    public Warrior [] warriors; //兵马俑信息

    [Header("终点位置")]
    // public Vector2 startLocalPoint;
    public Vector2 endLocalPoint;

    [Header("其它地图信息")]
    public int width;
    public int height;
    public string [,] charactor;
    // public string [,] newCharactor;

    public bool isMove = false;

    private Direction direction;

    void Start()
    {
        charactor = new string[height, width];
        // newCharactor = new string[height, width];
        ReadMap();
    }
    
    private void FixedUpdate()
    {
        if (!isMove)
        {
            if (InputManager.instance.GetDirection() != Direction.Idle)
            {
                direction = InputManager.instance.GetDirection();
                isMove = true;
                StartCoroutine(MakeNotMove(hero.TIME_SET + 2));
            }
        }
        else
        {
            hero.HeroFixedUpdate(this, direction);
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].GhostFixedUpdate(this, direction);
            }
            for (int i = 0; i < warriors.Length; i++)
            {
                warriors[i].WarriorFixedUpdate(this, direction);
            }
        }
    }

    /// <summary>
    /// 从文件获取地图内各人物与石头位置
    /// 空地为0，石头为1，男主为2，女主为3，女鬼为4，盗贼为5，兵马俑为6，是字符
    /// </summary>
    private void ReadMap()
    {
        using (StringReader sr = new StringReader(map.text))
        {
            sr.ReadLine();
            sr.ReadLine();
            string line;
            for (int i = 0; i < height; i++)
            {
                line = sr.ReadLine();
                for (int j = 0; j < width; j++)
                {
                    string[] tiles = line.Split(' ');
                    charactor[i, j] = tiles[j];
                }
            }
        }
    }

    IEnumerator MakeNotMove(int times)
    {
        for (int i = 0; i < times; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        CharactorTransformXY();
        CheckoutHeroDie();
        CheckoutWarriorsDie();
        CheckWin();
        isMove = false;
    }

    private void CheckWin()
    {
        if (hero.x == endLocalPoint.x && hero.y == endLocalPoint.y)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void CharactorTransformXY()
    {
        hero.TransformXY();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].TransformXY();
        }
        // for (int i = 0; i < thiefs.Length; i++)
        // {
        //     thief
        // }
        for (int i = 0; i < warriors.Length; i++)
        {
            warriors[i].TransformXY();
        }
    }

    private void CheckoutHeroDie()
    {
        for (int i = 0; i < warriors.Length; i++)
        {
            if (hero.x + 1 == warriors[i].x && hero.y == warriors[i].y)
            {
                BoxGameManager.instance.HeroDie();
            }
            if (hero.x - 1 == warriors[i].x && hero.y == warriors[i].y)
            {
                BoxGameManager.instance.HeroDie();
            }
            if (hero.x == warriors[i].x && hero.y + 1 == warriors[i].y)
            {
                BoxGameManager.instance.HeroDie();
            }
            if (hero.x == warriors[i].x && hero.y - 1 == warriors[i].y)
            {
                BoxGameManager.instance.HeroDie();
            }
        }
    }

    //TODO: 改用hash表
    private void CheckoutWarriorsDie()
    {
        int [] temp = new int[warriors.Length];
        int sameNums = 0;
        for (int i = 0; i < warriors.Length; i++)
        {
            for (int j = 0; j < warriors.Length; j++)
            {
                if (i == j)
                {
                    continue;
                }
                if (warriors[i].x == warriors[j].x && warriors[i].y == warriors[j].y)
                {
                    temp[sameNums] = i;
                    sameNums++;
                    break;
                }
            }
        }
        // Debug.Log(sameNums);
        if (sameNums != 0)
        {
            Warrior [] newWarriors = new Warrior[warriors.Length - sameNums];
            int index = 0;
            for (int i = 0, j = 0; i < warriors.Length; i++)
            {
                if (i == temp[index])
                {
                    index++;
                    continue;
                }
                newWarriors[j] = warriors[i];
                j++;
            }
            for (int i = 0; i < sameNums; i++)
            {
                Destroy(warriors[temp[i]].gameObject);
            }
            warriors = newWarriors;
        }
    }

    //TODO: 判断是否撞墙并填充新地图
    public bool WillAgainstTheWall(Direction direction, int x, int y, ref int nextX, ref int nextY, string index)
    {
        int newX = x;
        int newY = y;
        switch (direction)
        {
            case Direction.Up:
                newX--;
                break;
            case Direction.Down:
                newX++;
                break;
            case Direction.Left:
                newY--;
                break;
            case Direction.Right:
                newY++;
                break;
        }
        if (newX < 0 || newY < 0 || newX >= height || newY >= width)
        {
            return true;
        }
        switch (charactor[newX, newY])
        {
            case "1":
                return true;
            case "4":
                return true;
            default:
                break;
        }
        nextX = newX;
        nextY = newY;
        return false;
    }

    //TODO: 判断是否撞墙并填充新地图
    public bool WillAgainstTheWall(Direction direction, int x, int y, string index)
    {
        int newX = x;
        int newY = y;
        switch (direction)
        {
            case Direction.Up:
                newX--;
                break;
            case Direction.Down:
                newX++;
                break;
            case Direction.Left:
                newY--;
                break;
            case Direction.Right:
                newY++;
                break;
        }
        if (newX < 0 || newY < 0 || newX >= height || newY >= width)
        {
            return true;
        }
        switch (charactor[newX, newY])
        {
            case "1":
                return true;
            case "4":
                return true;
            default:
                break;
        }
        return false;
    }

    //TODO: 判断主角是否撞鬼
    public bool HeroWillAgainstTheGhost(Direction direction)
    {
        int dX = hero.x;
        int dY = hero.y;
        switch (direction)
        {
            case Direction.Up:
                dX--;
                break;
            case Direction.Down:
                dX++;
                break;
            case Direction.Left:
                dY--;
                break;
            case Direction.Right:
                dY++;
                break;
        }
        if (dX < 0 || dY < 0 || dX >= height || dY >= width)
        {
            return false;
        }

        for (int i = 0; i < ghosts.Length; i++)
        {
            if (dX == ghosts[i].x && dY == ghosts[i].y)
            {
                return true;
            }
        }
        return false;
    }

    public bool HaveTheWarrior(int x, int y)
    {
        for (int i = 0; i < warriors.Length; i++)
        {
            if (x == warriors[i].x && y == warriors[i].y)
            {
                return true;
            }
        }
        return false;
    }
}
