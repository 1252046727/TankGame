using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //�ƶ��ٶ�
    public float speed = 220f;
    //������
    public BaseTank tank;
    //�ڵ�ģ��
    private GameObject skin;
    //����
    Rigidbody rigidBody;

    //��ʼ��
    public void Init()
    {
        //Ƥ��
        GameObject skinRes = ResManager.LoadPrefab("bulletPrefab");
        skin = (GameObject)Instantiate(skinRes);
        skin.transform.parent = this.transform;
        skin.transform.localPosition = Vector3.zero;
        skin.transform.localEulerAngles = Vector3.zero;
        //����
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        //��ǰ�ƶ�
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    //��ײ
    void OnCollisionEnter(Collision collisionInfo)
    {
        //�򵽵�̹��
        GameObject collObj = collisionInfo.gameObject;
        BaseTank hitTank = collObj.GetComponent<BaseTank>();
        //���ܴ��Լ�
        if (hitTank == tank)
        {
            return;
        }
        //������̹��
        if(hitTank != null)
        {
            SendMsgHit(tank, hitTank);
        }
        //��ʾ��ըЧ��
        GameObject explode = ResManager.LoadPrefab("fire");
        explode = Instantiate(explode, transform.position, transform.rotation);
        //�ݻ�����
        Destroy(gameObject);
        Destroy(explode,2.0f);
    }

    //�����˺�Э��
    void SendMsgHit(BaseTank tank, BaseTank hitTank)
    {
        if (hitTank == null || tank == null)
        {
            return;
        }
        //�����Լ��������ڵ�
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
