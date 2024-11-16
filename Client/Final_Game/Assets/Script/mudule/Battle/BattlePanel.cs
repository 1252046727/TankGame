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
    //ʰȡ��Ч
    public static AudioSource audioSource;

    //��ʼ��
    public override void OnInit()
    {
        skinPath = "BattlePanel";
        layer = PanelManager.Layer.Panel;
        // ��� AudioSource �����������Ч
        audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("sound/Pick_up_sound");
        audioSource.clip = clip;

        // ��������
        audioSource.volume = 1.0f; // ���������0.0f - 1.0f ֮���ֵ
    }
    //��ʾ
    public override void OnShow(params object[] args)
    {
        //Ѱ�����
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
            //����ȡ��
            ReflashHp(Mathf.CeilToInt(tank.hp));
        }

    }


    //������Ϣ
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
        camp1Text.text = "��:" + count1.ToString();
        camp2Text.text = count2.ToString() + ":��";
    }

    //����hp
    private void ReflashHp(int hp)
    {
        if (hp < 0) { hp = 0; }
        //����ֵ����������
        hpFill.fillAmount = hp / 100f;
        hpText.text = "hp:" + hp;
    }

    //�ر�
    public override void OnClose()
    {
        NetManager.RemoveMsgListener("MsgLeaveBattle", OnMsgLeaveBattle);
        NetManager.RemoveMsgListener("MsgHit", OnMsgHit);
    }

    //�յ�����˳�Э��
    public void OnMsgLeaveBattle(MsgBase msgBase)
    {
        ReflashCampInfo();
    }

    //�յ�����Э��
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

    //�յ���ѪЭ��
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
