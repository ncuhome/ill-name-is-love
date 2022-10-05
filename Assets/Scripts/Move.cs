using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float timer=0;
    public static bool isMove = false;
    private int direction = 0;
    public static int num = 0;

    //1为w，2为a，-1为s，-2为d
    void Start()
    {
        
   }

    // Update is called once per frame
    void Update()
    {
        // 点击w或者上箭头键，前移动
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && LimitScope2.wall_W==false )
        {
            if (isMove == false )
            {
                direction = 1;
                isMove = true;
                num++;
            }
        }

        // 点击S或者下箭头键，后移动
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && LimitScope2.wall_S==false)
        {
            if (isMove == false)
            {
                direction = -1;
                isMove = true;
                num++;
            }
        }

        // 点击A或者左箭头键，左移动
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && LimitScope2.wall_A==false)
        {
            if (isMove == false)
            {
                direction = 2;
                isMove = true;
                num++;
            }
        }


        // 点击D或者右箭头键，右移动
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && LimitScope2.wall_D==false)
        {
            if (isMove == false)
            {
                direction = -2;
                isMove = true;
                num++;
            }
        }
 
        if (isMove && timer < 1 )
        {
            switch (direction)
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
        if (timer > 1 )
        {
            timer = 0;
            isMove = false;
        }
    }
}
