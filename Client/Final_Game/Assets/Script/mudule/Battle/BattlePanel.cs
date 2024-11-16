using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : BasePanel
{
    //hp
    private Image hpFill;
    private Text hpText;
    //info
    private Text camp1Text;
    private Text camp2Text;
    //拾取音效
    public static AudioSource audioSource;

    //初始化
    public override void OnInit()
    {
        skinPath = "BattlePanel";
        layer = PanelManager.Layer.Panel;
        // 添加 AudioSource 组件并设置音效
        audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("sound/Pick_up_sound");
        audioSource.clip = clip;

        // 设置音量
        audioSource.volume = 1.0f; // 最大音量，0.0f - 1.0f 之间的值
    }
    //显示
    public override void OnShow(params object[] args)
    {
        //寻找组件
        hpFill = skin.transform.Find("HpBar/Fill").GetComponent<Image>();
        hpText = skin.transform.Find("HpBar/HpText").GetComponent<Text>();
        camp1Text = skin.transform.Find("CampInfo/Camp1Text").GetComponent<Text>();
        camp2Text = skin.transform.Find("CampInfo/Camp2Text").GetComponent<Text>();
        ReflashCampInfo();

        NetManager.AddMsgListener("MsgLeaveBattle", OnMsgLeaveBattle);
        NetManager.AddMsgListener("MsgHit", OnMsgHit);
        NetManager.AddMsgListener("MsgAddHp", OnMsgAddHp);

        BaseTank tank = BattleManager.GetCtrlTank();
        if (tank != null)
        {
            //向上取整
            ReflashHp(Mathf.CeilToInt(tank.hp));
        }

    }


    //更新信息
    private void ReflashCampInfo()
    {
        int count1 = 0;
        int count2 = 0;
        foreach (BaseTank tank in BattleManager.tanks.Values)
        {
            if (tank.IsDie())
            {
                continue;
            }

            if (tank.camp == 1) { count1++; };
            if (tank.camp == 2) { count2++; };
        }
        camp1Text.text = "红:" + count1.ToString();
        camp2Text.text = count2.ToString() + ":蓝";
    }

    //更新hp
    private void ReflashHp(int hp)
    {
        if (hp < 0) { hp = 0; }
        //生命值条的填充比例
        hpFill.fillAmount = hp / 100f;
        hpText.text = "hp:" + hp;
    }

    //关闭
    public override void OnClose()
    {
        NetManager.RemoveMsgListener("MsgLeaveBattle", OnMsgLeaveBattle);
        NetManager.RemoveMsgListener("MsgHit", OnMsgHit);
    }

    //收到玩家退出协议
    public void OnMsgLeaveBattle(MsgBase msgBase)
    {
        ReflashCampInfo();
    }

    //收到击中协议
    public void OnMsgHit(MsgBase msgBase)
    {
        MsgHit msg = (MsgHit)msgBase;
        if (msg.targetId == GameMain.id)
        {
            BaseTank tank = BattleManager.GetCtrlTank();
            if (tank != null)
            {
                ReflashHp(Mathf.CeilToInt(tank.hp));
            }
        }
        ReflashCampInfo();

    }

    //收到回血协议
    public void OnMsgAddHp(MsgBase msgBase)
    {
        MsgAddHp msg = (MsgAddHp)msgBase;
        if (msg.id == GameMain.id)
        {
            BaseTank tank = BattleManager.GetCtrlTank();
            if (tank != null)
            {
                ReflashHp(Mathf.CeilToInt(tank.hp));
            }
        }
    }
}
