using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitScope : MonoBehaviour
{
    [Header("获得需要限制运动范围的物体")]
    [SerializeField]
    private Transform transform1;

    /// <summary> X轴范围 </summary>
    [Header("限制物体的X轴正方向")]
    [SerializeField]
    private float limitX;

    [Header("限制物体的X轴负方向")]
    [SerializeField]
    private float limit_X;

    /// <summary> Z轴范围 </summary>
    [Header("限制物体的Z轴正方向")]
    [SerializeField]
    private float limitZ;

    [Header("限制物体的Z轴负方向")]
    [SerializeField]
    private float limit_Z;

    public static bool wall=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (limitX < transform1.position.x || limitZ < transform1.position.z || limit_Z > transform1.position.z || limit_X > transform1.position.x)
        {
            wall = true;
            Vector3 temp = transform1.position;
            if (temp.z > limitZ)
            {
                temp.z = limitZ;
            }
            if (temp.z < limit_Z)
            {
                temp.z = limit_Z;
            }
            if (temp.x > limitX)
            {
                temp.x = limitX;
            }
            if (temp.x < limit_X)
            {
                temp.x = limit_X;
            }
            transform1.position = temp;
        }
        else
            wall = false;
    }
}
