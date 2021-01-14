using UnityEngine;
using System.Linq;                    // 引用 系統.查尋語言 API - 偵測陣列 清單內的資料

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
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.down * smallLenght);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.right * smallLenght);
            Gizmos.DrawRay(transform.GetChild(i).position, Vector2.left * smallLenght);
        }
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
        ChcckBottom();
        checkLeftRight();
    }

    #endregion

    #region 檢查底部是否有其他方塊

    /// <summary>
    /// 小方塊底部碰撞
    /// </summary>
    public bool smallBotton;
    public bool smalright;
    public bool smalleft;

    /// <summary>
    /// 所有方塊右邊是否有其他方塊
    /// </summary>
    public bool[] smallRightAll;
    /// <summary>
    /// 所有方塊左邊是否有其他方塊
    /// </summary>
    public bool[] smallLeftAll;

    /// <summary>
    /// 檢查左右邊是否有其他方塊
    /// </summary>
    private void checkLeftRight()
    {
        for (int i = 0; i < transform.childCount; i++)
        {

            RaycastHit2D hitR = Physics2D.Raycast(transform.GetChild(i).position, Vector3.right, smallLenght, 1 << 10);

            if (hitR && hitR.collider.name == "方塊") // smalright = true;
                                                          // else smalright = false;
                smallRightAll[i] = true;                    // 將陣列對應的格子勾選
            else smallRightAll[i] = false;

            RaycastHit2D hitL = Physics2D.Raycast(transform.GetChild(i).position, Vector3.left, smallLenght, 1 << 10);

            if (hitL && hitL.collider.name == "方塊") smallLeftAll[i] = true;
            else smallLeftAll[i] = false;
        }

        // 檢查陣列內 等於( => ) ture 的資料
        // 陣列.哪裡( 代名詞 => 條件 )
        // var無類型
        var allRight = smallRightAll.Where(x => x == true);

        // 測試
        // print(allRight.ToArray().Length);                     // 轉為陣列 數量
        smallRight = allRight.ToArray().Length > 0;

        var allLeft = smallRightAll.Where(x => x == true);
        smallLeft = allLeft.ToArray().Length > 0;
    }

    
    /// <summary>
    /// 檢查底部是否有其他方塊
    /// </summary>
    private void ChcckBottom()
    {
        // 迴圈執行每一顆方塊
        for (int i = 0; i < transform.childCount; i++)
        {
            // 每一顆小方塊 射線(每一顆小方塊中心點，向下，長度，圖層)
            RaycastHit2D hitdown = Physics2D.Raycast(transform.GetChild(i).position, Vector3.down, smallLenght, 1 << 10);

          //  print(hitdown.collider.name);

            if (hitdown && hitdown.collider.name == "方塊") smallBotton = true;
            // {
            // 如 只有一行 {} 可省去
            // }
        }
    }
    #endregion


    #region 方法

    /// <summary>
    /// 小方塊底部碰撞
    /// </summary>
    public bool smallBottom;

    public bool smallRight;
    public bool smallLeft;

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
