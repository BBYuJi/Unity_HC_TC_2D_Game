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
    [Header("下一顆俄羅斯方塊區域")]
    public Transform traNextA;
    [Header("畫布")]
    public Transform trAAA;


    /// <summary>
    /// 下一顆俄羅斯編號
    /// </summary>
    public int indexNext;

    private void Start()
    {
        produce();
    }



    /// <summary>
    /// 生成俄羅斯方塊
    /// 隨機顯示下一顆
    /// </summary>
    private void produce()
    {
        // 下一顆編號 = 隨機.範圍(最小,最大) 整數不等於最大值
        indexNext=  Random.Range(0, 7);
        // 下一顆俄羅斯方塊區域.取得子物件(子物件編號).轉為遊戲物件.啟動設定(顯示)
        traNextA.GetChild(indexNext).gameObject.SetActive(true);

    }

    /// <summary>
    /// 開始遊戲
    /// 1. 生成方塊要放在正確位置
    /// 2. 上一次方塊隱藏
    /// 3. 隨機取得下一塊
    /// </summary>
    public void StarGame()
    {
        // 保存上一次的方塊
        GameObject tetris = traNextA.GetChild(indexNext).gameObject;
        // 生成物件(物件,父物件)
        GameObject current= Instantiate(tetris,trAAA);
        // GetComponent<任何元件>()
        // <T> 泛型 - 指的是所有類型
        current.GetComponent<RectTransform>().anchoredPosition = new Vector2(10,270);


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
