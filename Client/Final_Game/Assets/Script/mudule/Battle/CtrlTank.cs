using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlTank : BaseTank
{
    //上一次发送同步信息的时间
    private float lastSendSyncTime = 0;
    //同步帧率
    public static float syncInterval = 0.05f;
    //生成道具Cd时间
    public float ItemCd = 20f;
    //上一次生成道具的时间
    public float lastItemTime = 0;

    new void Update()
    {
        base.Update();
        //移动控制
        MoveUpdate();
        //炮塔控制
        TurretUpdate();
        //开炮
        FireUpdate();
        //发送同步信息
        SyncUpdate();
        //生成道具
        ItemUpdate();
    }

    //移动控制
    public void MoveUpdate()
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //旋转
        float x = Input.GetAxis("Horizontal");
        transform.Rotate(0, x * steer * Time.deltaTime, 0);
        //前进后退
        float y = Input.GetAxis("Vertical");
        Vector3 s = y * transform.forward * speed * Time.deltaTime;
        transform.transform.position += s;
        //轮子旋转，履带滚动
        WheelUpdate(y);
    }

    //炮塔控制
    public void TurretUpdate()
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //或者轴向
        int axis = 0;
        if (Input.GetKey(KeyCode.Q))
        {
            axis = -1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            axis = 1;
        }
        if (axis == 0)
        {
            return;
        }
        //旋转角度
        Vector3 le = turret.localEulerAngles;
        le.y += axis * Time.deltaTime * turretSpeed;
        turret.localEulerAngles = le;
    }

    //开炮
    public void FireUpdate()
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //按键判断
        if (!Input.GetKey(KeyCode.Space) && !Input.GetMouseButton(0))
        {
            return;
        }
        //cd是否判断
        if (Time.time - lastFireTime < fireCd)
        {
            return;
        }
        //发射
        Bullet bullet = Fire();
        //发送同步协议
        MsgFire msg = new MsgFire();
        msg.x = bullet.transform.position.x;
        msg.y = bullet.transform.position.y;
        msg.z = bullet.transform.position.z;
        msg.ex = bullet.transform.eulerAngles.x;
        msg.ey = bullet.transform.eulerAngles.y;
        msg.ez = bullet.transform.eulerAngles.z;
        NetManager.Send(msg);
    }

    //发送同步信息
    public void SyncUpdate()
    {
        //时间间隔判断
        if (Time.time - lastSendSyncTime < syncInterval)
        {
            return;
        }
        lastSendSyncTime = Time.time;
        //发送同步协议
        MsgSyncTank msg = new MsgSyncTank();
        msg.x = transform.position.x;
        msg.y = transform.position.y;
        msg.z = transform.position.z;
        msg.ex = transform.eulerAngles.x;
        msg.ey = transform.eulerAngles.y;
        msg.ez = transform.eulerAngles.z;
        msg.turretY = turret.localEulerAngles.y;
        msg.gunX = gun.localEulerAngles.x;
        NetManager.Send(msg);
    }


    //计算爆炸位置
    public Vector3 ForecastExplodePoint()
    {
        //碰撞信息和碰撞点
        Vector3 hitPoint = Vector3.zero;
        RaycastHit hit;
        //沿着炮管方向的射线
        Vector3 pos = firePoint.position;
        Ray ray = new Ray(pos, firePoint.forward);
        //射线检测
        int layerMask = ~(1 << LayerMask.NameToLayer("Bullet"));
        if (Physics.Raycast(ray, out hit, 200.0f, layerMask))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = ray.GetPoint(200);
        }
        return hitPoint;
    }

    //发送道具同步信息
    public void ItemUpdate()
    {
        //时间间隔判断
        if (Time.time - lastItemTime < ItemCd)
        {
            return;
        }
        lastItemTime = Time.time;
        float minDistance = 10f; // 设置一个最小距离
        //发送同步协议
        MsgItem msg = new MsgItem();
        do
        {
            msg.x = transform.position.x + Random.Range(-80f, 80f);
            msg.z = transform.position.z + Random.Range(-80f, 80f);
        } while (Vector3.Distance(new Vector3(msg.x, 0, msg.z), new Vector3(transform.position.x, 0, transform.position.z)) < minDistance);
        // 获取地形的高度
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(msg.x, 200f, msg.z), Vector3.down, out hit))
        {
            msg.y = hit.point.y+2; // 获取实际地面高度
        }
        msg.opt = Random.Range(0, 3);
        NetManager.Send(msg);
    }
}
