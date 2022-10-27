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
    public Tiger [] tigers; //老虎信息

    [Header("终点位置")]
    // public Vector2 startLocalPoint;
    public Vector2 endLocalPoint;

    [Header("其它地图信息")]
    public int width;
    public int height;
    public string [,] charactor;
    // public string [,] newCharactor;

    public bool isMove = false;
    public bool isDieByThief = false;

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
            for (int i = 0; i < thiefs.Length; i++)
            {
                thiefs[i].ThiefFixedUpdate(this, direction);
            }
            for (int i = 0; i < tigers.Length; i++)
            {
                tigers[i].TigerFixedUpdate(this, direction);
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
        CheckoutThiefsDie();
        CheckWin();
        isMove = false;
    }

    private void CheckWin()
    {
        if (hero.x == endLocalPoint.x && hero.y == endLocalPoint.y)
        {
            BoxSceneManager.instance.EndGame();
        }
    }

    private void CharactorTransformXY()
    {
        hero.TransformXY();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].TransformXY();
        }
        for (int i = 0; i < thiefs.Length; i++)
        {
            thiefs[i].TransformXY();
        }
        for (int i = 0; i < warriors.Length; i++)
        {
            warriors[i].TransformXY();
        }
        for (int i = 0; i < tigers.Length; i++)
        {
            tigers[i].TransformXY();
        }
    }

    private void CheckoutHeroDie()
    {
        for (int i = 0; i < warriors.Length; i++)
        {
            if ((hero.x + 1 == warriors[i].x && hero.y == warriors[i].y)
            || (hero.x - 1 == warriors[i].x && hero.y == warriors[i].y)
            || (hero.x == warriors[i].x && hero.y + 1 == warriors[i].y)
            || (hero.x == warriors[i].x && hero.y - 1 == warriors[i].y)
            || (hero.x == warriors[i].x && hero.y == warriors[i].y))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        for (int i = 0; i < thiefs.Length; i++)
        {
            if (hero.x == thiefs[i].x && hero.y == thiefs[i].y)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (isDieByThief)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        for (int i = 0; i < tigers.Length; i++)
        {
            if (hero.x == tigers[i].x && hero.y == tigers[i].y)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    //TODO: 改用hash表
    private void CheckoutThiefsDie()
    {
        int [] temp1 = new int[tigers.Length];
        int sameNums1 = 0;
        int [] temp2 = new int[thiefs.Length];
        int sameNums2 = 0;
        for (int i = 0; i < tigers.Length; i++)
        {
            for (int j = 0; j < thiefs.Length; j++)
            {
                if (tigers[i].x == thiefs[j].x && tigers[i].y == thiefs[j].y)
                {
                    bool hasSame1 = false;
                    bool hasSame2 = false;
                    for (int t = 0; t < sameNums1; t++)
                    {
                        if (temp1[t] == i)
                        {
                            hasSame1 = true;
                            break;
                        }
                    }
                    if (!hasSame1)
                    {
                        temp1[sameNums1] = i;
                        sameNums1++;
                    }
                    for (int t = 0; t < sameNums2; t++)
                    {
                        if (temp2[t] == j)
                        {
                            hasSame2 = true;
                            break;
                        }
                    }
                    if (!hasSame2)
                    {
                        temp2[sameNums2] = j;
                        sameNums2++;
                    }
                }
            }
        }
        if (sameNums1 != 0)
        {
            Tiger [] newTigers = new Tiger[tigers.Length - sameNums1];
            int index = 0;
            for (int i = 0, j = 0; i < tigers.Length; i++)
            {
                if (i == temp1[index])
                {
                    index++;
                    continue;
                }
                newTigers[j] = tigers[i];
                j++;
            }
            for (int i = 0; i < sameNums1; i++)
            {
                Destroy(tigers[temp1[i]].gameObject);
            }
            tigers = newTigers;
        }
        if (sameNums2 != 0)
        {
            Thief [] newThiefs = new Thief[thiefs.Length - sameNums2];
            int index = 0;
            for (int i = 0, j = 0; i < thiefs.Length; i++)
            {
                if (i == temp2[index])
                {
                    index++;
                    continue;
                }
                newThiefs[j] = thiefs[i];
                j++;
            }
            for (int i = 0; i < sameNums2; i++)
            {
                Destroy(thiefs[temp2[i]].gameObject);
            }
            thiefs = newThiefs;
        }
    }

    //判断是否撞墙（或鬼）
    public bool WillAgainstTheWall(Direction direction, int x, int y, ref int nextX, ref int nextY)
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
        if (charactor[newX, newY] == "1")
        {
            return true;
        }
        for (int i = 0; i < ghosts.Length; i++)
        {
            if (newX == ghosts[i].x && newY == ghosts[i].y)
            {
                return true;
            }
        }
        nextX = newX;
        nextY = newY;
        return false;
    }

    //判断是否撞墙（或鬼）
    public bool WillAgainstTheWall(Direction direction, int x, int y)
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
        if (charactor[newX, newY] == "1")
        {
            return true;
        }
        for (int i = 0; i < ghosts.Length; i++)
        {
            if (newX == ghosts[i].x && newY == ghosts[i].y)
            {
                return true;
            }
        }
        return false;
    }

    //判断是否可能撞人
    public bool WillAgainstTheHero(Direction direction, int x, int y)
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
            return false;
        }
        if (hero.x == newX && hero.y == newY)
        {
            return true;
        }
        return false;
    }

    //判断老虎是否经过土匪
    public bool AgainstTheThief(Direction direction, int x, int y)
    {
        for (int i = 0; i < thiefs.Length; i++)
        {
            if (x == thiefs[i].nextX && y == thiefs[i].nextY)
            {
                return true;
            }
        }
        return false;
    }

    //判断鬼是否要移动
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

    //判断鬼是否满足停止移动的一个条件
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
