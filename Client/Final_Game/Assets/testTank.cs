using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTank : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //界面
        PanelManager.Init();
        PanelManager.Open<LoginPanel>();
        PanelManager.Open<TipPanel>("用户名或密码错误！");
        //坦克
        //GameObject tankObj = new GameObject("myTank");
        //CtrlTank ctrlTank = tankObj.AddComponent<CtrlTank>();
        //ctrlTank.Init("tankPrefab");
        //相机
        //tankObj.AddComponent<CameraFollow>();
        //被打的坦克
        GameObject tankObj2 = new GameObject("enemyTank");
        BaseTank baseTank = tankObj2.AddComponent<BaseTank>();
        baseTank.Init("tankPrefab");
        baseTank.transform.position = new Vector3(0, 10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        NetManager.Update();
    }
}
