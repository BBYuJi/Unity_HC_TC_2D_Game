﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{


    // public公開 - 允許按鈕呼叫

        /// <summary>
        /// 延遲呼叫，等音效播完
        /// </summary>
    public void DelayStartGame()
    {
        //延遲呼叫("方法名稱",延遲秒數);
        //Invorke();
        Invoke("StartGame", 1.7F);
    }

    /// <summary>
    /// 延遲呼叫
    /// </summary>
    public void DelayexcGame()
    {
        Invoke("excGmae", 1.7f);
    }

    

    /// <summary>
    /// 開始遊戲
    /// </summary>
    private void StartGame()
    {
        //載入場景
        //場景管理器 @ 載入場景("遊戲名稱");
        SceneManager.LoadScene("遊戲場景");
    }


    /// <summary>
    /// 離開遊戲
    /// </summary>
    private void excGmae()
    {
        //離開遊戲  應用程式 的 離開()
        Application.Quit();
    }
}
