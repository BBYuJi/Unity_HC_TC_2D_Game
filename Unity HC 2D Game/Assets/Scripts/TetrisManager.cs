﻿using UnityEngine;
using UnityEngine.UI;           // 引用介面API
using System.Linq;             // 查詢語言
using System.Collections;    // 引用  系統.集合  API  - 協同程序



public class TetrisManager : MonoBehaviour
{
    #region 欄位
    [Header("掉落時間"), Range(0.1f, 3)]
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
    [Header("生成方塊父物件")]
    public Transform traTetrisParen;
    [Header("生成的起始位置")]
    #endregion

    public Vector2[] posSpawn =
    {
        new Vector2(30,365),
        new Vector2(30,365),
        new Vector2(15,380),
        new Vector2(15,380),
        new Vector2(15,380),
        new Vector2(15,380),
        new Vector2(0,380)
    };


    /// <summary>
    /// 下一顆俄羅斯編號
    /// </summary>
    private int indexNext;

    /// <summary>
    /// 目前方塊
    /// </summary>
    private RectTransform TetrisA;

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    private void Start()
    {
        produce();
    }

    // 更新事件 : 一秒執行的 60 次
    private void Update()
    {
        ControlTertis();

        FastDown();
    }

    #region 方塊控制
    /// <summary>
    /// 方塊控制
    /// </summary>
    private void ControlTertis()
    {
        // 如果 已經有 目前方塊
        if (TetrisA)
        {
            timer += Time.deltaTime;      // 計時器 累加

            if (timer >= Droptime)
            {
                timer = 0;
                TetrisA.anchoredPosition -= new Vector2(0, 30);
            }


            // 取得目前方塊的 Tetris 腳本
            Tetris tetris = TetrisA.GetComponent<Tetris>();

            // 如果 往右 X < 280 才能移動
            // if (TetrisA.anchoredPosition.x < 280)
            // 如果 方塊 沒有碰到 右邊牆壁
            if (!tetris.wallright && !tetris.smalright)
            {
                // || 或者 (輸入: shift + |)
                // 按下 D 往右 移動 30
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    TetrisA.anchoredPosition += new Vector2(30, 0);
                }

            }

            // 如果 往左 X > -230 才能移動
            // if (TetrisA.anchoredPosition.x > -230)
            if (!tetris.wallleft && !tetris.smalleft)
            {

                // 按下 A 往左 移動 30
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    TetrisA.anchoredPosition -= new Vector2(30, 0);
                }

            }

            // 如果 方塊可以旋轉
            // 按下 W 旋轉 90
            // 屬性面板上面的 Rotation 必須用 eulerAngles 控制

            if (tetris.canRot)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    TetrisA.eulerAngles += new Vector3(0, 0, 90);

                    tetris.offset();
                }
            }
            


            if (!fastDown)                      // 如果 沒有在快速落下 才能 加速
            {
                //按下 S 掉落速度加速
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    Droptime = 0.15f;
                }
                else     //  否則 恢復 速度
                {
                    Droptime = 1.5f;
                }

                // 如果 方塊 碰到地板 就重新 生成下一顆
                // 或者 碰到其他方塊
                if (tetris.walldown || tetris.smallBotton)
                {
                    SetGround();                                    // 設定為地板
                //    CheckTetris();                                  // 檢查俄羅斯方塊 - 將方塊分離
                    StarGame();                                     // 生成下一顆
                    StartCoroutine(ShakeEffect());                  // 晃動效果   <啟動協同程序(協同方法());>
                }
            }
            
        }
    }
    #endregion

    #region 生成俄羅斯方塊
    /// <summary>
    /// 生成俄羅斯方塊
    /// 隨機顯示下一顆
    /// </summary>
    private void produce()
    {
        // 下一顆編號 = 隨機.範圍(最小,最大) 整數不等於最大值
        indexNext = Random.Range(0, 7);

        // 測試
        // indexNext = 2;

        // 下一顆俄羅斯方塊區域.取得子物件(子物件編號).轉為遊戲物件.啟動設定(顯示)
        traNextA.GetChild(indexNext).gameObject.SetActive(true);

    }
    #endregion

    #region 設定掉落後變為方塊

    /// <summary>
    /// 設定掉落後變為方塊
    /// </summary>
    private void SetGround()
    {
        /** 迴圈 : for 
        //(初始值 ; 條件 ; 迭代器)
        for (int i = 0; i < 10; i++)
        {

            print("迴圈 : " + i);
        }
        */

        int count = TetrisA.childCount;                             // 取得 目前 方塊 的子物件數量

        for (int i = 0; i < count; i++)                             // 迴圈 執行 子物件數量次數
        {
            TetrisA.GetChild(i).name = "方塊";                      // 名稱改為方塊
            TetrisA.GetChild(i).gameObject.layer = 10;               // 圖層改為方塊 
        }

        for (int i = 0; i < count; i++)
        {
            TetrisA.GetChild(0).SetParent(traScoreArea);
        }

        Destroy(TetrisA.gameObject);

    }
    #endregion

   

    #region  開始遊戲
    /// <summary>
    /// 開始遊戲
    /// 1. 生成方塊要放在正確位置
    /// 2. 上一次方塊隱藏
    /// 3. 隨機取得下一塊
    /// </summary>
    public void StarGame()
    {
        fastDown = false;               // 碰到地板後，沒有快速落下

        // 1. 生成方塊要放在正確位置
        // 保存上一次的方塊
        GameObject tetris = traNextA.GetChild(indexNext).gameObject;
        // 生成物件(物件,父物件)
        GameObject current = Instantiate(tetris, traTetrisParen);
        // GetComponent<任何元件>()
        // <T> 泛型 - 指的是所有類型
        // 目前俄羅斯方塊 . 取得元件<介面變形>() . 座標 = 陣列 [編號]
        current.GetComponent<RectTransform>().anchoredPosition = posSpawn[indexNext];

        // 2. 上一次方塊隱藏
        tetris.SetActive(false);

        // 3. 隨機取得下一塊
        produce();

        // 將生成方塊 RectTransform 元件儲存
        TetrisA = current.GetComponent<RectTransform>();

    }
    #endregion

    #region 協同程序
    // 協同程序
    // IEnumerator 傳回類型 - 時間
    private IEnumerator ShakeEffect()
    {
        yield return new WaitForSeconds(0.05f);

        // print("協同程序一開始");
        // yield return new WaitForSeconds(1f);
        // print("等待一秒過後...");
        // yield return new WaitForSeconds(2f);
        // print("又過二秒過!!");

        //取得震動效果物件的 Rect
        RectTransform rect = traTetrisParen.GetComponent<RectTransform>();

        // 晃動 向上 30 > 0 > 20 > 0
        // 等待 0.05
        float interval = 0.05f;

        rect.anchoredPosition += Vector2.up * 30;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition += Vector2.up * 20;
        yield return new WaitForSeconds(interval);
        rect.anchoredPosition = Vector2.zero;
        yield return new WaitForSeconds(interval);

    }
    #endregion

    #region 快速掉落功能
    /// <summary>
    /// 是否快速落下
    /// </summary>
    private bool fastDown;

    /// <summary>
    /// 快速掉落功能
    /// </summary>
    private void FastDown()
    {
        if (TetrisA && !fastDown)                                            // 如果 有 目前方塊
        {
            if (Input.GetKeyDown(KeyCode.Space))                // 如果 按下 空白鍵
            {
                fastDown = true;                                // 正在快速落下

                timer = 0.018f;                                  // 掉落時間


            }
        }
    }
    #endregion  

    [Header("分數判定區域")]
    public Transform traScoreArea;

    /// <summary>
    /// 檢查方塊是否連線
    /// </summary>
    private void CheckTetris()
    {

    }

    #region 方法

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
