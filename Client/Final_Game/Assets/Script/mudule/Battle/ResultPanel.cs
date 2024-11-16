using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : BasePanel
{
    //ʤ����ʾͼƬ
    private Image winImage;
    //ʧ����ʾͼƬ
    private Image lostImage;
    //ȷ����ť
    private Button okBtn;

    //��ʼ��
    public override void OnInit()
    {
        skinPath = "ResultPanel";
        layer = PanelManager.Layer.Tip;
    }
    //��ʾ
    public override void OnShow(params object[] args)
    {
        //Ѱ�����
        winImage = skin.transform.Find("WinImage").GetComponent<Image>();
        lostImage = skin.transform.Find("LostImage").GetComponent<Image>();
        okBtn = skin.transform.Find("OkBtn").GetComponent<Button>();
        //����
        okBtn.onClick.AddListener(OnOkClick);
        //��ʾ�ĸ�ͼƬ
        if (args.Length == 1)
        {
            bool isWIn = (bool)args[0];
            if (isWIn)
            {
                winImage.gameObject.SetActive(true);
                lostImage.gameObject.SetActive(false);
            }
            else
            {
                winImage.gameObject.SetActive(false);
                lostImage.gameObject.SetActive(true);
            }
        }
    }

    //�ر�
    public override void OnClose()
    {

    }

    //������ȷ����ť
    public void OnOkClick()
    {
        PanelManager.Open<RoomPanel>();
        BattleManager.Reset();
        Close();
    }
}
