using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //移动速度
    public float speed = 220f;
    //发射者
    public BaseTank tank;
    //炮弹模型
    private GameObject skin;
    //物理
    Rigidbody rigidBody;

    //初始化
    public void Init()
    {
        //皮肤
        GameObject skinRes = ResManager.LoadPrefab("bulletPrefab");
        skin = (GameObject)Instantiate(skinRes);
        skin.transform.parent = this.transform;
        skin.transform.localPosition = Vector3.zero;
        skin.transform.localEulerAngles = Vector3.zero;
        //物理
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        //向前移动
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //碰撞
    void OnCollisionEnter(Collision collisionInfo)
    {
        //打到的坦克
        GameObject collObj = collisionInfo.gameObject;
        BaseTank hitTank = collObj.GetComponent<BaseTank>();
        //不能打自己
        if (hitTank == tank)
        {
            return;
        }
        //打到其他坦克
        if(hitTank != null)
        {
            SendMsgHit(tank, hitTank);
        }
        //显示爆炸效果
        GameObject explode = ResManager.LoadPrefab("fire");
        explode = Instantiate(explode, transform.position, transform.rotation);
        //摧毁自身
        Destroy(gameObject);
        Destroy(explode,2.0f);
    }

    //发送伤害协议
    void SendMsgHit(BaseTank tank, BaseTank hitTank)
    {
        if (hitTank == null || tank == null)
        {
            return;
        }
        //不是自己发出的炮弹
        if (tank.id != GameMain.id)
        {
            return;
        }
        MsgHit msg = new MsgHit();
        msg.targetId = hitTank.id;
        msg.id = tank.id;
        msg.x = transform.position.x;
        msg.y = transform.position.y;
        msg.z = transform.position.z;
        NetManager.Send(msg);
    }
}
