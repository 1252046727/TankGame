using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour
{
    //Ƥ��·��
    public string skinPath;
    //Ƥ��
    public GameObject skin;
    //�㼶
    public PanelManager.Layer layer = PanelManager.Layer.Panel;
    //��ʼ��
    public void Init()
    {
        //Ƥ��
        GameObject skinPrefab = ResManager.LoadPrefab(skinPath);
        skin = (GameObject)Instantiate(skinPrefab);
    }
    //�ر�
    public void Close()
    {
        string name = this.GetType().ToString();
        PanelManager.Close(name);
    }

    //��ʼ��ʱ
    public virtual void OnInit()
    {
    }
    //��ʾʱ
    public virtual void OnShow(params object[] para)
    {
    }
    //�ر�ʱ
    public virtual void OnClose()
    {
    }

}
