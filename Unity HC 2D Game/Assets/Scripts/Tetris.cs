﻿using UnityEngine;

public class Tetris : MonoBehaviour
{
    #region 欄位

    [Header("角度為零，線性的長度")]
    public float lenght0;
    [Header("角度為九十度，線性的長度")]
    public float lenght90;
    [Header("旋轉位移左右")]
    public int offX;
    [Header("旋轉位移上下")]
    public int offY;
    [Header("偵測是否能旋轉")]
    public float lenghtRot0r;
    public float lenghtRot0l;
    public float lenghtRot90r;
    public float lenghtRot90l;


    /// <summary>
    /// 紀錄目前長度
    /// </summary>
    private float lenght;
    private float lenghtdown;
    private float lenghtRotR;
    private float lenghtRotL;


    /// <summary>
    /// 是否碰到右邊牆
    /// </summary>
    public bool wallright;
    /// <summary>
    /// 是否碰到左邊牆
    /// </summary>
    public bool wallleft;
    /// <summary>
    /// 是否碰到下方地板
    /// </summary>
    public bool walldown;
    /// <summary>
    /// 是否能旋轉
    /// </summary>
    public bool canRot = true;

    private RectTransform rect;

    #endregion

    [Header("每一顆小方塊的射線長度"), Range(0f, 2f)]
    public float smallLenght = 0.5f;

    #region 事件
    /// <summary>
    /// 繪製圖示  OnDrawGizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        #region 判定牆壁與地板
        // 將浮點數轉為整數
        int z = (int)transform.eulerAngles.z;

        if (z == 0 || z == 180)
        {
            // 儲存目前長度
            lenght = lenght0;

            // 圖示 的 繪製射線 ( 起點 , 方向 )
            // 圖示 的 顏色
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * lenght0);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -Vector3.right * lenght0);
            // 繪製向下線條
            lenghtdown = lenght90;
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.up * lenght90);
            // 繪製旋轉線條
            lenghtRotR = lenghtRot0r;
            lenghtRotL = lenghtRot0l;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.right * lenghtRot0r);
            Gizmos.DrawRay(transform.position, -Vector3.right * lenghtRot0l);

        }

        else if (z == 90 || z == 270)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.right * lenght90);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -Vector3.right * lenght90);
            // 繪製向下線條
            lenghtdown = lenght0;
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, -Vector3.up * lenght0);
            // 繪製旋轉線條
            lenghtRotR = lenghtRot90r;
            lenghtRotL = lenghtRot90l;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, Vector3.right * lenghtRot90r);
            Gizmos.DrawRay(transform.position, Vector3.left * lenghtRot90l);

        }
        #endregion

        #region 每一顆判定

        #endregion
    }



    private void Start()
    {
        // 儲存遊戲開始的長度
        lenght = lenght0;

        rect = GetComponent<RectTransform>();

        // 偵測有的子物件(小方塊) 就新增幾個陣列
        smallRightAll = new bool [transform.childCount];
    }

    private void Update()
    {
        CheakX();
    }

    #endregion

    #region 方法
    /// <summary>
    /// 小方塊底部碰撞
    /// </summary>
    public bool smallBottom;

    public bool smallRight;

    /// <summary>
    /// 所有方塊右邊是否有其他方塊
    /// </summary>
    public bool[] smallRightAll;
 

    private void CheakX()
    {
        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理 .射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right , lenght, 1<<8);
        
        if (hit && hit.transform.name == "牆右" )
        {
            wallright = true;
        }
        else
        {
            wallright = false;
        }

        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理 .射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hitL = Physics2D.Raycast(transform.position, -Vector3.right, lenght, 1 << 8);

        if (hitL && hitL.transform.name == "牆左")
        {
            wallleft = true;
        }
        else
        {
            wallleft = false;
        }

        // 旋轉射線
        RaycastHit2D hitRotR = Physics2D.Raycast(transform.position, Vector3.right, lenghtRot0r, 1 << 8);
        RaycastHit2D hitRotL = Physics2D.Raycast(transform.position, -Vector3.right, lenghtRot0l, 1 << 8);

        if (hitRotR && hitRotR.transform.name == "牆右" || hitRotL && hitRotL.transform.name == "牆左")
        {
            canRot = false;
        }
        else
        {
            canRot = true;
        }

        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理 .射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hitD = Physics2D.Raycast(transform.position, -Vector3.up, lenghtdown, 1 << 9);

        if (hitD && hitD.transform.name == "地板")
        {
            walldown = true;
        }
        else
        {
            walldown = false;
        }
    }

    /// <summary>
    /// 旋轉後位移的處理
    /// </summary>
    public void offset()
    {
        // 將浮點數轉為整數
        int z = (int)transform.eulerAngles.z;

        if (z==90 || z==270)
        {
            rect.anchoredPosition -= new Vector2(offX, offY);
        }
        else if (z== 0 || z== 180)
        {
            rect.anchoredPosition += new Vector2(offX, offY);
        }

    }
    #endregion
}
