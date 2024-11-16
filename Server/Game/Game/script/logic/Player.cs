using System;

public class Player
{
    //id
    public string id = "";
    //指向ClientState
    public ClientState state;
    //构造函数
    public Player(ClientState state)
    {
        this.state = state;
    }
    //临时数据
    //坐标
    public float x;
    public float y;
    public float z;
    public float ex;
    public float ey;
    public float ez;
    //在哪个房间
    public int roomId = -1;
    //阵营
    public int camp = 1;
    //坦克生命值
    public int hp = 100;
    //坦克攻击力
    public int attack = 15;
    //数据库数据
    public PlayerData data;

    //发送信息
    public void Send(MsgBase msgBase)
    {
        NetManager.Send(state, msgBase);
    }

}


