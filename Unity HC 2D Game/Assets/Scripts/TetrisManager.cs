using UnityEngine;

public class TetrisManager : MonoBehaviour
{
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


}
