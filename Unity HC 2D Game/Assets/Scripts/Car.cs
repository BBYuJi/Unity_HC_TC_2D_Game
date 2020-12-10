using UnityEngine;

public class Car : MonoBehaviour
{
    #region 欄位
    // 註解 (說明)
    // 數值
    // 語法
    // 類別 名稱； 


    //私人 private 不會顯示在面板上
    //公開 public  會顯示在面板上

    [Header ("這是車子的尺寸"), Range (50,200)] 
    public int size = 200;
    [Tooltip ("這是車子的重量，單位是噸"), Range (1.5f,3f)]
    public float weight = 1.5f;
    [Header ("品牌"),Tooltip ("車子的品牌")]
    public string brand = "BMW";
    [Header("是否有天窗"), Tooltip("車子有無天窗")]
    public bool haswindow = true;

    
    //顏色 
    

    public Color colorA = Color.black;
    public Color colorB = new Color(0.5f, 2, 1);
    public Color colorC = new Color(0.5f, 2, 1,0.5f);

    //向量 Vector2

    public Vector2 V2A = Vector2.zero;
    public Vector2 V2B = Vector2.one;

    //音效與圖片

    public AudioClip sound1;
    public Sprite sprite;

    //遊戲物件與預製物

    public GameObject goA;
    public GameObject goB;

    //元件:屬性面板上的粗體字

    public Transform trA;
    public Camera cma;

    #endregion

    #region 事件

    private void Start()
    {
        print("哈囉,沃德");

        //取得欄位(抓出資料)

        print(size);
        print(brand);

        //設定欄位(修改)

        haswindow = false;

        //無傳回

        MethodA();

        //有傳回

        int intA = MeB();
        print(intA);

        MethodC(10);

        float b = BMI(70,1.74f);
        print("BMI:" + b);

    #endregion
    }
    

    #region 方法


    //無傳回 void

    private void MethodA()
    {
        print("00000000");
        print("11111111");
    }
   

    //有傳回

    private int MeB()
    {
        return 001;
    }
    
    private void MethodC(int number)
    {
        number += 10;
        print("累加後" + number);
    }

    private float BMI(float w,float h)
    {
        float bmi = w / (h * h);
        return bmi;
    }

    #endregion
}
