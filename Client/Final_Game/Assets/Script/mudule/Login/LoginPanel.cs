using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    //�˺������
    private InputField idInput;
    //���������
    private InputField pwInput;
    //��½��ť
    private Button loginBtn;
    //ע�ᰴť
    private Button regBtn;
    //����ͼ
    private Image bgImage;
    //��ʼ��ʾ��ʱ��
    private float startTime = float.MaxValue;
    //��ʾ����ʧ��
    private bool showConnFail = false;
    //ip�͵�ַ
    private string ip = "127.0.0.1";
    private int port = 8888;
    //��ʼ��
    public override void OnInit()
    {
        skinPath = "LoginPanel";
        layer = PanelManager.Layer.Panel;
    }

    //��ʾ
    public override void OnShow(params object[] args)
    {
        //Ѱ�����
        idInput = skin.transform.Find("IdInput").GetComponent<InputField>();
        pwInput = skin.transform.Find("PwInput").GetComponent<InputField>();
        loginBtn = skin.transform.Find("LoginBtn").GetComponent<Button>();
        regBtn = skin.transform.Find("RegisterBtn").GetComponent<Button>();
        bgImage = skin.transform.Find("BgImage").GetComponent<Image>();
        //����
        loginBtn.onClick.AddListener(OnLoginClick);
        regBtn.onClick.AddListener(OnRegClick);
        //����Э�����
        NetManager.AddMsgListener("MsgLogin", OnMsgLogin);
        //�����¼�����
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
        //���ӷ�����
        NetManager.Connect(ip, port);
        //��¼ʱ��
        startTime = Time.time;
    }

    //�ر�
    public override void OnClose()
    {
        //����Э�����
        NetManager.RemoveMsgListener("MsgLogin", OnMsgLogin);
        //�����¼�����
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.RemoveEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
    }

    //���ӳɹ��ص�
    void OnConnectSucc(string err)
    {
        Debug.Log("OnConnectSucc");
    }

    //����ʧ�ܻص�
    void OnConnectFail(string err)
    {
        showConnFail = true;
        //PanelManager.Open<TipPanel>(err);
    }

    //������ע�ᰴť
    public void OnRegClick()
    {
        PanelManager.Open<RegisterPanel>();
    }



    //�����µ�½��ť
    public void OnLoginClick()
    {
        //�û�������Ϊ��
        if (idInput.text == "" || pwInput.text == "")
        {
            PanelManager.Open<TipPanel>("�û��������벻��Ϊ��");
            return;
        }
        //����
        MsgLogin msgLogin = new MsgLogin();
        msgLogin.id = idInput.text;
        msgLogin.pw = pwInput.text;
        NetManager.Send(msgLogin);
    }

    //�յ���½Э��
    public void OnMsgLogin(MsgBase msgBase)
    {
        MsgLogin msg = (MsgLogin)msgBase;
        if (msg.result == 0)
        {
            Debug.Log("��¼�ɹ�");
            //����id
            GameMain.id = msg.id;
            //�򿪷����б����
            PanelManager.Open<RoomListPanel>();
            //�رս���
            Close();
        }
        else
        {
            PanelManager.Open<TipPanel>("��¼ʧ��");
        }
    }
    //update
    public void Update()
    {
        //����ͼ����Ч��
        float w = Mathf.Ceil(Time.time * 2) % 10 == 0 ? 500f : 0.1f;//Ƶ��
        //����ͼ������������1.0��1.2֮�䲨��
        float a = 1 + 0.1f - 0.1f * Mathf.Sin(w * Time.time);   //���
        bgImage.transform.localScale = new Vector3(a, a, 1);
        //����ʧ��
        if (showConnFail)
        {
            showConnFail = false;
            PanelManager.Open<TipPanel>("��������ʧ�ܣ������´���Ϸ");
        }
    }
}
