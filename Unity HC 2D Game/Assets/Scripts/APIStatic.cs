using UnityEngine;

public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        print(Mathf.PI);

        //練習

        print("所有攝影機數量" + Camera.allCamerasCount);

        Cursor.visible = false;


        //Application.OpenURL("https://www.google.com/");
        print("5.5 去小數點" + Mathf.Floor(5.5F));

    }

    private void Update()
    {
        print("是否輸入任意鍵"+ Input.anyKey);

        print("遊戲經過時間" + Time.time);




    }


}
