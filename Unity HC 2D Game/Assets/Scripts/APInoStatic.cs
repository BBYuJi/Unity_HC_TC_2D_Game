
using UnityEngine;

public class APInoStatic : MonoBehaviour
{
    public Transform traA;
    public Transform traB;

    public GameObject myobject;

    public Transform traC;

    

    private void Start()
    {
       print("物件 A 的座標"+ traA.position);

        traB.position = new Vector3(1, 3, 5);

        print("我的物件圖層:"+myobject.layer);

        myobject.layer = 2;

        
    
    }

    private void Update()
    {
        



    }



}
