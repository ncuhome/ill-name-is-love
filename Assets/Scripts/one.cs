using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class one : MonoBehaviour
{
    private float timer = 0;
    private bool isMove = false;
    private bool LongPress = false;
    private float direction = 0;
    private bool KeyUp = false;
    //1为w，2为a，3为s，4为d
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 点击w或者上箭头键，前移动
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = 1;
            KeyUp = false;
            if (isMove == false && LongPress == false)
            {
                isMove = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            LongPress = false;
            KeyUp = true;
        }


        // 点击S或者下箭头键，后移动
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = 3;
            KeyUp = false;
            if (isMove == false && LongPress == false)
            {
                isMove = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            LongPress = false;
            KeyUp = true;
        }


        // 点击A或者左箭头键，左移动
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = 2;
            KeyUp = false;
            if (isMove == false && LongPress == false)
            {
                isMove = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            LongPress = false;
            KeyUp = true;
        }


        // 点击D或者右箭头键，右移动
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 4;
            KeyUp = false;
            if (isMove == false && LongPress == false)
            {
                isMove = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            LongPress = false;
            KeyUp = true;
        }

        if (isMove && timer < 1)
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
        if (timer > 1)
        {
            timer = 0;
            isMove = false;
            if (KeyUp == false)
            {
                LongPress = true;
            }
        }
    }

}
