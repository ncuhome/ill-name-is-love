using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float timer=0;
    public static bool isMove = false;
    private int direction = 0;
    public static int num = 0;

    //1为w，2为a，3为s，4为d
    void Start()
    {
        
   }

    // Update is called once per frame
    void Update()
    {
        // 点击w或者上箭头键，前移动
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isMove == false )
            {
                direction = 1;
                isMove = true;
                num++;
            }
        }

        // 点击S或者下箭头键，后移动
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isMove == false)
            {
                direction = 3;
                isMove = true;
                num++;
            }
        }

        // 点击A或者左箭头键，左移动
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isMove == false)
            {
                direction = 2;
                isMove = true;
                num++;
            }
        }


        // 点击D或者右箭头键，右移动
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isMove == false)
            {
                direction = 4;
                isMove = true;
                num++;
            }
        }
        if (isMove == false && LimitScope.wall == true)
            num--;
 
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
                case 3:
                    this.transform.Translate(-Time.deltaTime, 0, 0);
                    timer += Time.deltaTime;
                    break;
                case 4:
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
