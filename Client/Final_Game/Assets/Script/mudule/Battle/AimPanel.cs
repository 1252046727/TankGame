using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimPanel : BasePanel
{
    CtrlTank tank;
    private Image aimImage;
    //��ʼ��
    public override void OnInit()
    {
        skinPath = "AimPanel";
        layer = PanelManager.Layer.Panel;
    }
    //��ʾ
    public override void OnShow(params object[] args)
    {
        //Ѱ�����
        aimImage = skin.transform.Find("Image").GetComponent<Image>();

        tank = (CtrlTank)BattleManager.GetCtrlTank();
    }

    //�ر�
    public override void OnClose()
    {

    }

    //������ȷ����ť
    public void Update()
    {
        if (tank == null)
        {
            return;
        }
        //3D����
        Vector3 point = tank.ForecastExplodePoint();
        //��Ļ����  (�� 3D ����ת��Ϊ��Ļ����)
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(point);
        //UI����
        aimImage.transform.position = screenPoint;
    }
}
