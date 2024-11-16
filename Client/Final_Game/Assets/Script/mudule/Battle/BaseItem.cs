using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    //皮肤路径
    public string skinPath;
    //皮肤
    public GameObject skin;
    //物理
    public Rigidbody rigidBody;
    //旋转速度
    public float rotatespeed = 45f;
    //哪种类型的道具
    public int category;
    public void Init()
    {
        //皮肤
        GameObject skinPrefab = ResManager.LoadPrefab(skinPath);
        skin = (GameObject)Instantiate(skinPrefab);
        Destroy(skin,25.0f);
        Destroy(gameObject, 25.0f);
        skin.transform.parent = this.transform;
        //物理
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true; // 设置为运动学物体
    }

    public virtual void OnInit()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collObj = other.gameObject;
        BaseTank TriggerTank = collObj.GetComponent<BaseTank>();
        //被坦克触碰
        if (TriggerTank != null)
        {
            //消失
            Destroy(skin);
            Destroy(gameObject);
            //不是自己
            if (TriggerTank.id != GameMain.id)
            {
                return;
            }
            //播放拾取音效
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
