using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillPanel : BasePanel
{
    //���濪ʼ��ʾ��ʱ��
    private float startTime = 0;

    //��ʼ��
    public override void OnInit()
    {
        skinPath = "KillPanel";
        layer = PanelManager.Layer.Tip;
    }
    //��ʾ
    public override void OnShow(params object[] args)
    {
        startTime = Time.time;
    }

    //�ر�
    public override void OnClose()
    {

    }

    //������ȷ����ť
    public void Update()
    {
        if (Time.time - startTime > 2f)
        {
            Close();
        }
    }
}
