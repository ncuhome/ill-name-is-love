using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public static float MIN_DIR_OFFSET = 0.05f;

    // public Hero hero;
    public Map map;

    private void Awake()
    {
        instance = this;    
    }

    public Direction GetDirection()
    {
        if (map.isMove)
        {
            return Direction.Idle;
        }
        // if (SceneController.instance.IsInDialogue)
        // {
        //     return Direction.Idle;
        // }
        Direction dir = JudgeDirection(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
        if (map.HeroWillAgainstTheGhost(dir))
        {
            return Direction.Idle;
        }
        return dir;
    }

    private Direction JudgeDirection(float x, float y)
    {
        float absX = Mathf.Abs(x);
        float absY = Mathf.Abs(y);
        if (absX <= MIN_DIR_OFFSET && absY <= MIN_DIR_OFFSET)
        {
            return Direction.Idle;
        }
        if (absX > absY)
        {
            if (x > 0)
            {
                return Direction.Up;
            }
            else
            {
                return Direction.Down;
            }
        }
        else
        {
            if (y > 0)
            {
                return Direction.Right;
            }
            else
            {
                return Direction.Left;
            }
        }
    }
}
