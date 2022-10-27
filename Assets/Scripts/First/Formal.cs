using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Formal : MonoBehaviour
{
    [Tooltip("含有Selectable及Image组件的UI对象，作为连接点。")]
    public List<RectTransform> _lstPoints = new List<RectTransform>();
    [Tooltip("用于设置所画线的颜色。（可使用“unlit/Color”Shader）")]
    public Material _matLineColor;
    [Tooltip("用于设置所画线的高度。")]
    public int _nHalfHeight = 15;
    [Tooltip("用于设置选择时Image的颜色。")]
    public Color _clrSelect = Color.red;
    [Tooltip("用于设置未选择时Image的颜色。")]
    public Color _clrUnSelect = Color.white;

    [HideInInspector]
    public List<RectTransform> _lstSelectPoints = new List<RectTransform>();//已选择连接点
    [HideInInspector]
    public List<int> _lstPassword = new List<int>();//以输入密码
    [HideInInspector]
    public Action<bool> onInputState;//true为开始输入，false为结束输入

    private bool _isPressing = false;//是否按下
    private Vector2 _vtPressPos;//按下点坐标

    private float _fDistance;//距离
    private float _fDegree;//夹角
    private Matrix4x4 _matrixTrans;//变换矩阵
    private Vector2[] _vertexPos = new Vector2[4];//顶点数组

    private Vector2 _tempPos;

    void Awake()
    {
        InitEnterEvent();//初始化所有连接点的消息
        ClearLines();//清空相关数据
    }

    void Update()
    {
        if (!IsPressed())
        {//当未按下时清空是数据
            ClearLines();
        }
    }

    bool IsPressed()
    {
        //触摸
        if (Input.touchCount > 0)
        {
            switch (Input.touches[0].phase)
            {
                case TouchPhase.Began:
                    _isPressing = true;
                    if (onInputState != null)
                    {
                        onInputState(true);//状态改变
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    _isPressing = false;
                    if (onInputState != null)
                    {
                        onInputState(false);//状态改变
                    }
                    break;
            }

            _vtPressPos = Input.touches[0].position;
        }
        else
        {
            //鼠标
            if (Input.GetMouseButtonDown(0))
            {
                _isPressing = true;
                if (onInputState != null)
                {
                    onInputState(true);//状态改变
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isPressing = false;
                if (onInputState != null)
                {
                    onInputState(false);//状态改变
                }
            }

            _vtPressPos = Input.mousePosition;
        }

        return _isPressing;
    }

    void OnPostRender()
    {
        DrawLines();//画所有线
    }

    void OnGUI()
    {
        string msg = "";

        msg += "是否正在输入:" + IsPressed() + "\n";
        msg += "密码:";
        for (int i = 0; i < _lstPassword.Count; i++)
        {
            msg += _lstPassword[i] + ",";
        }

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.normal.textColor = new Color(1, 1, 1);   //设置字体颜色
        guiStyle.fontSize = 75;       //设置字体大小
        GUILayout.Label(msg, guiStyle);
    }

    void InitEnterEvent()
    {
        //为每个点添加Enter事件
        _lstPoints.ForEach((rtTrans) =>
        {
            EventTrigger trigger = rtTrans.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = rtTrans.gameObject.AddComponent<EventTrigger>();
            }

            //添加事件
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;//进入事件
            EventTrigger.TriggerEvent evtEnter = new EventTrigger.TriggerEvent();
            evtEnter.AddListener(OnSelectPoint);
            entryEnter.callback = evtEnter;
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryDown = new EventTrigger.Entry();
            entryDown.eventID = EventTriggerType.PointerDown;//按下事件
            EventTrigger.TriggerEvent evtDown = new EventTrigger.TriggerEvent();
            evtDown.AddListener(OnSelectPoint);
            entryDown.callback = evtDown;
            trigger.triggers.Add(entryDown);
        });
    }

    public void OnSelectPoint(BaseEventData obj)
    {
        //转换数据类型
        PointerEventData data = obj as PointerEventData;

        GameObject target = null;
        if (null != data.pointerEnter)
        {
            target = data.pointerEnter;
        }
        else if (null != data.pointerPress)
        {
            target = data.pointerPress;
        }

        AddSelectPoint(target);//添加选择连接点
    }

    void AddSelectPoint(GameObject obj)
    {
        if (IsPressed() && null != obj)
        {
            //将未连接的点添加到需要连接的点的列表中去
            RectTransform rtTrans = obj.GetComponent<RectTransform>();
            if (null != rtTrans && !_lstSelectPoints.Contains(rtTrans))
            {
                //添加到绘制列表
                _lstSelectPoints.Add(rtTrans);
                //添加密码序列
                _lstPassword.Add(_lstPoints.IndexOf(rtTrans));
                //改变颜色
                rtTrans.GetComponent<Image>().color = _clrSelect;
            }
        }
    }

    void ClearLines()
    {
        //清空选择及密码列表
        _lstSelectPoints.Clear();
        _lstPassword.Clear();

        //还原颜色
        _lstPoints.ForEach((rtTrans) =>
        {
            rtTrans.GetComponent<Image>().color = _clrUnSelect;
        });
    }

    void DrawLine(Vector2 vtStart, Vector2 vtEnd)
    {
        _tempPos = vtEnd - vtStart;
        _fDistance = Vector3.Distance(Vector3.zero, _tempPos);//距离
        _fDegree = Vector3.Angle(_tempPos, Vector3.right);//与x轴正方向的夹角

        //判断旋转方向，逆时针为正，顺时针为付
        if (_tempPos.y < 0)
        {
            _fDegree *= -1;
        }

        //设置变换矩阵
        _matrixTrans.SetTRS(vtStart, Quaternion.Euler(0, 0, _fDegree), Vector3.one);//设置变换矩阵


        //设置绘制顶点坐标
        _vertexPos[0].x = 0;
        _vertexPos[0].y = -_nHalfHeight;
        _vertexPos[1].x = 0;
        _vertexPos[1].y = _nHalfHeight;
        _vertexPos[2].x = _fDistance;
        _vertexPos[2].y = _nHalfHeight;
        _vertexPos[3].x = _fDistance;
        _vertexPos[3].y = -_nHalfHeight;

        //绘制
        GL.PushMatrix();
        GL.LoadPixelMatrix();//使(0,0,0)为左下角，(Screen.width,Screen.height,0)为右上角
        GL.MultMatrix(_matrixTrans);
        GL.Begin(GL.QUADS);//绘制四边形
        for (int n = 0; n < 4; n++)
        {
            GL.Vertex(_vertexPos[n]);
        }
        GL.End();
        GL.PopMatrix();
    }

    void DrawLines()
    {
        //设置线的材质
        _matLineColor.SetPass(0);

        //连接已选择的点
        for (int nIndex = 0; nIndex < _lstSelectPoints.Count - 1; nIndex++)
        {
            DrawLine(_lstSelectPoints[nIndex].position, _lstSelectPoints[nIndex + 1].position);
        }

        //连接到Press点
        if (IsPressed() && _lstSelectPoints.Count > 0)
        {
            DrawLine(_vtPressPos, _lstSelectPoints[_lstSelectPoints.Count - 1].position);
        }
    }
}
