using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitScope2 : MonoBehaviour
{
    [Header("�����Ҫ�����˶���Χ������")]
    [SerializeField]
    private Transform transform1;

    /// <summary> X�᷶Χ </summary>
    [Header("���������X��������")]
    [SerializeField]
    private float limitX;

    [Header("���������X�Ḻ����")]
    [SerializeField]
    private float limit_X;

    /// <summary> Z�᷶Χ </summary>
    [Header("���������Z��������")]
    [SerializeField]
    private float limitZ;

    [Header("���������Z�Ḻ����")]
    [SerializeField]
    private float limit_Z;

    public static bool wall_W = false;
    public static bool wall_A = false;
    public static bool wall_S = false;
    public static bool wall_D = false;
    // Start is called before the first frame update
    void Start()
    {
        wall_W = false;
        wall_A = false;
        wall_S = false;
        wall_D = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (limitX < transform1.position.x + 1)
        {
            wall_W = true;
        }
        else
            wall_W = false;

        if (limit_X > transform1.position.x - 1)
        {
            wall_S = true;
        }
        else
            wall_S = false;

        if (limitZ < transform1.position.z + 1)
        {
            wall_A = true;
        }
        else
            wall_A = false;

        if (limit_Z > transform1.position.z - 1)
        {
            wall_D = true;
        }
        else
            wall_D = false;

    }
}
