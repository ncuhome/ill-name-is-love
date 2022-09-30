using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    public BuoyancyEffector2D effector2D;
    public float[] level;

    public int levelIndex = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            if (levelIndex > 0)
            {
                levelIndex--;

            }
        }
        else if (Input.GetButtonDown("Down"))
        {
            if (levelIndex < level.Length - 1)
            {
                levelIndex++;
            }
        }
        effector2D.surfaceLevel = level[levelIndex];
    }
}
