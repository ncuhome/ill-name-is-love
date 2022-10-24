using System;
using System.IO;
using UnityEngine;

public enum Direction
{
    Idle = -1,
    Right,
    Up,
    Left,
    Down
}

public class Map : MonoBehaviour
{
    [Header("地图相关文件")]
    public TextAsset mapStone; //记录石头
    public TextAsset mapCharactor; //记录Tile内元素

    [Header("地图和人物预制件脚本")]
    public MapTile mapTile; //地图Tilemap信息
    public Hero hero; //男主信息
    public Ghost [] ghosts; //鬼魂信息
    public Warrior [] warriors; //兵马俑信息

    [Header("人物相当于Anchor的出生点与起始点位置")]
    public Vector3 startLocalPoint;
    public Vector3 endLocalPoint;

    [Header("地图块信息")]
    public MapTile[,] mapTiles;

    [Header("地图元素信息")]
    //将地图石头与人物位置二维信息压缩成一维
    public string mapStoneLine;
    public string mapCharactorLine;

    [Header("其它地图信息")]
    public int width;
    public int height;
    public Vector3 leftAndUpLocalPoint; //地图左上角基准点坐标

    void Start()
    {
        ReadMapStone();
        ReadMapCharactor();
        LoadStone();
        LoadCharactor();
    }

    /// <summary>
    /// 从文件获取地图内石头位置，读取到一位数组中去
    /// </summary>
    private void ReadMapStone()
    {
        string mapStoneText = mapStone.text;
        string[] lines = mapStoneText.Split('\n');
        string[] tileNums = lines[0].Split(' ');

        height = lines.Length;
        width = tileNums.Length;

        using (StringReader sr = new StringReader(mapStoneText))
        {
            string line;
            for (int j = 0; j < height; j++)
            {
                line = sr.ReadLine();
                for (int i = 0; i < width; i++)
                {
                    tileNums = line.Split(' ');
                    mapStoneLine += tileNums[i];
                }
            }
        }
    }

    /// <summary>
    /// 从文件获取地图内各人物位置，读取到一位数组中去
    /// </summary>
    private void ReadMapCharactor()
    {
        string mapCharactorText = mapCharactor.text;
        string[] lines = mapCharactorText.Split('\n');
        string[] tileNums = lines[0].Split(' ');

        height = lines.Length;
        width = tileNums.Length;

        using (StringReader sr = new StringReader(mapCharactorText))
        {
            string line;
            for (int j = 0; j < height; j++)
            {
                line = sr.ReadLine();
                for (int i = 0; i < width; i++)
                {
                    tileNums = line.Split(' ');
                    mapCharactorLine += tileNums[i];
                }
            }
        }
    }

    // private void ReadMapStartAndEndPoint()
    // {
    //     using (StringReader sr = new StringReader(mapStartAndEndPoint.text))
    //     {
    //         string line;
    //         string [] param;
    //         line = sr.ReadLine();
    //         param = line.Split(' ');

    //         startLocalPoint = new Vector2(Convert.ToInt32(param[0]), Convert.ToInt32(param[1]));

    //         line = sr.ReadLine();
    //         param = line.Split(' ');
    //         endLocalPoint = new Vector2(Convert.ToInt32(param[0]), Convert.ToInt32(param[1]));
    //     }
    // }

    //TODO: 动态生成石头：四选一并保存信息到map里面
    private void LoadStone()
    {
        //地图贴图信息
        // Sprite [] mapSprites = Resources.LoadAll<Sprite>(mapSpritesPath);
        //地图位置信息
        // Vector2 [,] mapLocalPostions = new Vector2[width, height];
        //地图左上角LocalPosition
        // leftAndUpLocalPoint = new Vector2((int)-width / 2, (int)height / 2);
        // mapTiles = new MapTile[width, height];

        //填充人物起始点和地图终点
        // startLocalPoint.x = leftAndUpLocalPoint.x + (startLocalPoint.x - 1);
        // startLocalPoint.y = leftAndUpLocalPoint.y - (startLocalPoint.y - 1);
        // endLocalPoint.x = leftAndUpLocalPoint.x + (endLocalPoint.x - 1);
        // endLocalPoint.y = leftAndUpLocalPoint.y - (endLocalPoint.y - 1);

        //填充贴图相对于Anchor的位置的同时生成地图的同时存储小地图脚本信息
        // Vector2 temp = leftAndUpLocalPoint; 
        // MapTile tMapTile;
        // for (int j = 0; j < height; j++)
        // {
        //     temp.x = leftAndUpLocalPoint.x;
        //     for (int i = 0; i < width; i++)
        //     {
        //         //填充位置
        //         mapLocalPostions[i, j] = temp;
        //         //生成地图
        //         tMapTile = Instantiate<MapTile>(mapTile);
        //         tMapTile.transform.SetParent(this.transform);
        //         //让子地图脚本完善自身信息与位置（信息包括x, y, 贴图, 碰撞体字符）
        //         tMapTile.SetMapTile((int)temp.x, (int)temp.y, ref mapSprites[j * width + i]);
        //         //存储小地图脚本信息
        //         mapTiles[i, j] = tMapTile;

        //         temp.x++;
        //     }
        //     temp.y--;
        // }
    }


    //TODO: 动态生成人物元素并保存信息到map里面
    private void LoadCharactor()
    {
        // if (this.gameObject.name == "LeftMap")
        // {
        //     Hero tHero = Instantiate<Hero>(leftHero);
        //     tHero.transform.SetParent(this.transform);
        //     tHero.SetHero(startLocalPoint, endLocalPoint, HeroId.LeftHero);
        // }
        // if (this.gameObject.name == "RightMap")
        // {
        //     Hero tHero = Instantiate<Hero>(rightHero);
        //     tHero.transform.SetParent(this.transform);
        //     tHero.SetHero(startLocalPoint, endLocalPoint, HeroId.RightHero);
        // }  
    }

    //TODO: 判断是否撞墙
    public bool WillAgainstTheWall(Direction direction, Vector2 heroLocalPositon)
    {
        // int index = 0;
        // switch (direction)
        // {
        //     case Direction.Idle:
        //         return false;
        //     case Direction.Up:
        //         index = Convert.ToInt32((heroLocalPositon.x - leftAndUpLocalPoint.x) + (leftAndUpLocalPoint.y - heroLocalPositon.y) * width - width);
        //         break;
        //     case Direction.Down:
        //         index = Convert.ToInt32((heroLocalPositon.x - leftAndUpLocalPoint.x) + (leftAndUpLocalPoint.y - heroLocalPositon.y) * width + width);
        //         break;
        //     case Direction.Left:
        //         index = Convert.ToInt32((heroLocalPositon.x - leftAndUpLocalPoint.x) + (leftAndUpLocalPoint.y - heroLocalPositon.y) * width - 1);
        //         break;
        //     case Direction.Right:
        //         index = Convert.ToInt32((heroLocalPositon.x - leftAndUpLocalPoint.x) + (leftAndUpLocalPoint.y - heroLocalPositon.y) * width + 1);
        //         break;
        // }
        
        // if (mapCollisionLine[index] == 'H')
        // {
        //     return true;
        // }
        // if (mapCollisionLine[index] == '_')
        // {
        //     return false;
        // }
        
        return false;
    }

    //TODO: 判断是否撞鬼
    public bool WillAgainstTheGhost(Direction direction, Vector2 heroLocalPositon)
    {
        return false;
    }

    //TODO: 判断是否撞兵马俑
    public bool WillAgainstTheWarrior(Direction direction, Vector2 heroLocalPositon)
    {
        return false;
    }
}
