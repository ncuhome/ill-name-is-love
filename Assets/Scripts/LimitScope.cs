using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitScope : MonoBehaviour
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

    public static bool wall=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (limitX < transform1.position.x  || limitZ < transform1.position.z  || limit_Z > transform1.position.z || limit_X > transform1.position.x)
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
