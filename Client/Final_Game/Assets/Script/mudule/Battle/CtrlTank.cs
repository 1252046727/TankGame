using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlTank : BaseTank
{
    //��һ�η���ͬ����Ϣ��ʱ��
    private float lastSendSyncTime = 0;
    //ͬ��֡��
    public static float syncInterval = 0.05f;
    //���ɵ���Cdʱ��
    public float ItemCd = 20f;
    //��һ�����ɵ��ߵ�ʱ��
    public float lastItemTime = 0;

    new void Update()
    {
        base.Update();
        //�ƶ�����
        MoveUpdate();
        //��������
        TurretUpdate();
        //����
        FireUpdate();
        //����ͬ����Ϣ
        SyncUpdate();
        //���ɵ���
        ItemUpdate();
    }

    //�ƶ�����
    public void MoveUpdate()
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //��ת
        float x = Input.GetAxis("Horizontal");
        transform.Rotate(0, x * steer * Time.deltaTime, 0);
        //ǰ������
        float y = Input.GetAxis("Vertical");
        Vector3 s = y * transform.forward * speed * Time.deltaTime;
        transform.transform.position += s;
        //������ת���Ĵ�����
        WheelUpdate(y);
    }

    //��������
    public void TurretUpdate()
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //��������
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
        //��ת�Ƕ�
        Vector3 le = turret.localEulerAngles;
        le.y += axis * Time.deltaTime * turretSpeed;
        turret.localEulerAngles = le;
    }

    //����
    public void FireUpdate()
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //�����ж�
        if (!Input.GetKey(KeyCode.Space) && !Input.GetMouseButton(0))
        {
            return;
        }
        //cd�Ƿ��ж�
        if (Time.time - lastFireTime < fireCd)
        {
            return;
        }
        //����
        Bullet bullet = Fire();
        //����ͬ��Э��
        MsgFire msg = new MsgFire();
        msg.x = bullet.transform.position.x;
        msg.y = bullet.transform.position.y;
        msg.z = bullet.transform.position.z;
        msg.ex = bullet.transform.eulerAngles.x;
        msg.ey = bullet.transform.eulerAngles.y;
        msg.ez = bullet.transform.eulerAngles.z;
        NetManager.Send(msg);
    }

    //����ͬ����Ϣ
    public void SyncUpdate()
    {
        //ʱ�����ж�
        if (Time.time - lastSendSyncTime < syncInterval)
        {
            return;
        }
        lastSendSyncTime = Time.time;
        //����ͬ��Э��
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


    //���㱬ըλ��
    public Vector3 ForecastExplodePoint()
    {
        //��ײ��Ϣ����ײ��
        Vector3 hitPoint = Vector3.zero;
        RaycastHit hit;
        //�����ڹܷ��������
        Vector3 pos = firePoint.position;
        Ray ray = new Ray(pos, firePoint.forward);
        //���߼��
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

    //���͵���ͬ����Ϣ
    public void ItemUpdate()
    {
        //ʱ�����ж�
        if (Time.time - lastItemTime < ItemCd)
        {
            return;
        }
        lastItemTime = Time.time;
        float minDistance = 10f; // ����һ����С����
        //����ͬ��Э��
        MsgItem msg = new MsgItem();
        do
        {
            msg.x = transform.position.x + Random.Range(-80f, 80f);
            msg.z = transform.position.z + Random.Range(-80f, 80f);
        } while (Vector3.Distance(new Vector3(msg.x, 0, msg.z), new Vector3(transform.position.x, 0, transform.position.z)) < minDistance);
        // ��ȡ���εĸ߶�
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(msg.x, 200f, msg.z), Vector3.down, out hit))
        {
            msg.y = hit.point.y+2; // ��ȡʵ�ʵ���߶�
        }
        msg.opt = Random.Range(0, 3);
        NetManager.Send(msg);
    }
}
