using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    public static bool Death = false;
    public static int num = 0;
    public static bool isMove = false;

    public float moveSpeed = 1.5f;
    private int direction = 0;
    private float timer = 0;
    //1为w，2为a，-1为s，-2为d
    void Start()
    {
        Death = false;
        num = 0;
        isMove = false;
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("销毁" + this.gameObject.name);
            Destroy(this.gameObject);
            Death = true;
        }
        if(other.gameObject.name =="Next")
        {
            Debug.Log("下一关" );
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

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
                    this.transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
                    timer += Time.deltaTime;
                    break;
                case 2:
                    this.transform.Translate(0, 0, Time.deltaTime * moveSpeed);
                    timer += Time.deltaTime;
                    break;
                case -1:
                    this.transform.Translate(-Time.deltaTime * moveSpeed, 0, 0);
                    timer += Time.deltaTime;
                    break;
                case -2:
                    this.transform.Translate(0, 0, -Time.deltaTime * moveSpeed);
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
