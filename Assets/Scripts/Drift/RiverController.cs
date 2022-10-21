using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    public static RiverController instance;

    public BuoyancyEffector2D [] effector2D;
    public float[] level;

    public int[] levelIndex;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }

    private void Update()
    {
        int key = InputHandler.instance.HandleJoyStickInput();
        HeroRiverChange(key);
    }

    private void HeroRiverChange(int key)
    {
        switch (key)
        {
            case 1:
                if (levelIndex[0] > 0)
                {
                    levelIndex[0]--;
                }
                break;
            case -1:
                if (levelIndex[0] < level.Length - 1)
                {
                    levelIndex[0]++;
                }
                break;
            default:
                break;
        }
        effector2D[0].surfaceLevel = level[levelIndex[0]];
    }

    public void MoveAnother(int boat)
    {
        if (levelIndex[boat] > 0)
        {
            levelIndex[boat]--;
            return;
        }
        if (levelIndex[boat] < level.Length - 1)
        {
            levelIndex[boat]++;
            return;
        }
    }
}
