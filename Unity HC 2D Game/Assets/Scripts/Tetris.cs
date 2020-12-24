using UnityEngine;

public class Tetris : MonoBehaviour
{

    [Header("角度為零，線性的長度")]
    public float lenght0;
    [Header("角度為九十度，線性的長度")]
    public float lenght90;

    /// <summary>
    /// 紀錄目前長度
    /// </summary>
    public float lenght;

    #region 繪製圖示
    /// <summary>
    /// 繪製圖示  OnDrawGizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        // 圖示 的 顏色
        Gizmos.color = Color.red;

        // 將浮點數轉為整數
        int z = (int)transform.eulerAngles.z;

        if (z == 0 || z == 180)
        {
            // 儲存目前長度
            lenght = lenght0;

            // 圖示 的 繪製射線 ( 起點 , 方向 )
            Gizmos.DrawRay(transform.position, Vector3.right * lenght0);
        }

        else if (z == 90 || z == 270)
        {
            Gizmos.DrawRay(transform.position, Vector3.right * lenght90);
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

    public bool wallright;


    private void CheakX()
    {
        // 2D 物理碰撞資訊 區域變數名稱 = 2D 物理 .射線碰撞(起點，方向，長度，圖層)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right,1<<8);

        if (hit && hit.transform.name == "牆 右")
        {
            wallright = true;
        }
        else
        {
            wallright = false;
        }

    }





}
