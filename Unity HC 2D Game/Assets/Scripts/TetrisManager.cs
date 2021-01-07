﻿using UnityEngine;
using System.Collections;    // 引用  系統.集合  API  - 協同程序


public class TetrisManager : MonoBehaviour
{
    #region
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 啟動協同程序(協同方法());
            StartCoroutine(ShakeEffect());
        }
    }

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
            if (!tetris.wallright)
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
            if (!tetris.wallleft)
            {

                // 按下 A 往右 移動 30
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    TetrisA.anchoredPosition -= new Vector2(30, 0);
                }

            }



            // 如果 方塊可以旋轉
            // 按下 W 旋轉 90
            // 屬性面板上面的 Rotation 必須用 eulerAngles 控制


            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                TetrisA.eulerAngles += new Vector3(0, 0, 90);

                tetris.offset();
            }



            //按下 S 掉落速度加速
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                Droptime = 0.3f;
            }
            else     //  否則 恢復 速度
            {
                Droptime = 1.5f;
            }

            // 如果 方塊 碰到地板 就重新 遊戲(生成) 
            if (tetris.walldown)
            {
                StarGame();
            }
        }
    }

    /// <summary>
    /// 生成俄羅斯方塊
    /// 隨機顯示下一顆
    /// </summary>
    private void produce()
    {
        // 下一顆編號 = 隨機.範圍(最小,最大) 整數不等於最大值
        indexNext = Random.Range(0, 7);

        // 測試
        // indexNext = 4;

        // 下一顆俄羅斯方塊區域.取得子物件(子物件編號).轉為遊戲物件.啟動設定(顯示)
        traNextA.GetChild(indexNext).gameObject.SetActive(true);

    }

    #region  開始遊戲
    /// <summary>
    /// 開始遊戲
    /// 1. 生成方塊要放在正確位置
    /// 2. 上一次方塊隱藏
    /// 3. 隨機取得下一塊
    /// </summary>
    public void StarGame()
    {
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

    [Header("分數判定區域")]
    public Transform traScoreArea;

    /// <summary>
    /// 檢查方塊是否連線
    /// </summary>
    private void CheckTetris()
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

    // 協同程序
    // IEnumerator 傳回類型 - 時間
    private IEnumerator ShakeEffect()
    {
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

}
