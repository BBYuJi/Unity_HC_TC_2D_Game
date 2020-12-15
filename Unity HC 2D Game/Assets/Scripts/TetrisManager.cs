using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    #region
    [Header("掉落時間"),Range(0.1f,3)]
    public float Droptime = 1.5f;
    [Header("目前分數")]
    public int CurrentScore;
    [Header("最高時間")]
    public int Scorehight;
    [Header("等級")]
    public int LV = 1;
    [Header("結束畫面")]
    public GameObject End;
    [Header("音效:掉落，移動，消除，結束。")]
    public AudioClip soundDrop;
    public AudioClip soundMove;
    public AudioClip soundEliminate;
    public AudioClip soundGmEnd;
    #endregion

    #region //事件
    
    #endregion

    #region  //方法
    
    /// <summary>
    /// 生成俄羅斯方塊
    /// </summary>
    private void produce()
    {

    }
    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="">增加的分數</param>
    public void addc(int addA)
    {

    }
    /// <summary>
    /// 遊戲時間
    /// </summary>
    private void timegame()
    {

    }
    /// <summary>
    /// 遊戲結束
    /// </summary>
    private void gameover()
    {

    }
    /// <summary>
    /// 重新遊戲
    /// </summary>
    public void replay()
    {

    }
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void excgame1()
    {

    }


    #endregion

}
