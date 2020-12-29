using UnityEngine;

public class Tetris : MonoBehaviour
{

    [Header("角度為零，線性的長度")]
    public float lenght0;
    [Header("角度為九十度，線性的長度")]
    public float lenght90;
    [Header("旋轉位移左右")]
    public int offX;
    [Header("旋轉位移上下")]
    public int offY;

    /// <summary>
    /// 紀錄目前長度
    /// </summary>
    private float lenght;
    private float lenghtdown;

    #region 繪製圖示
    /// <summary>
    /// 繪製圖示  OnDrawGizmos
    /// </summary>
    private void OnDrawGizmos()
    {
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
        }

    }
    #endregion


    private void Start()
    {
        // 儲存遊戲開始的長度
        lenght = lenght0;
    }

    private void Update()
    {
        CheakX();
    }

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





}
