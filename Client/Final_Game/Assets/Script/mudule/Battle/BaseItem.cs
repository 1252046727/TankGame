using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    //Ƥ��·��
    public string skinPath;
    //Ƥ��
    public GameObject skin;
    //����
    public Rigidbody rigidBody;
    //��ת�ٶ�
    public float rotatespeed = 45f;
    //�������͵ĵ���
    public int category;
    public void Init()
    {
        //Ƥ��
        GameObject skinPrefab = ResManager.LoadPrefab(skinPath);
        skin = (GameObject)Instantiate(skinPrefab);
        Destroy(skin,25.0f);
        Destroy(gameObject, 25.0f);
        skin.transform.parent = this.transform;
        //����
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true; // ����Ϊ�˶�ѧ����
    }

    public virtual void OnInit()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collObj = other.gameObject;
        BaseTank TriggerTank = collObj.GetComponent<BaseTank>();
        //��̹�˴���
        if (TriggerTank != null)
        {
            //��ʧ
            Destroy(skin);
            Destroy(gameObject);
            //�����Լ�
            if (TriggerTank.id != GameMain.id)
            {
                return;
            }
            //����ʰȡ��Ч
            BattlePanel.audioSource.Play();
            if (category == 0)
            {
                MsgAddHp msg = new MsgAddHp();
                msg.id = TriggerTank.id;
                NetManager.Send(msg);
            }
            else if (category == 1)
            {
                MsgAddAgility msg = new MsgAddAgility();
                msg.id = TriggerTank.id;
                NetManager.Send(msg);
            }
            else if(category == 2)
            {
                MsgAddAttack msg = new MsgAddAttack();
                msg.id = TriggerTank.id;
                NetManager.Send(msg);
            }
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
