//同步坦克信息
public class MsgSyncTank : MsgBase
{
    public MsgSyncTank() { protoName = "MsgSyncTank"; }
    //位置、旋转、炮塔旋转
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float ex = 0f;
    public float ey = 0f;
    public float ez = 0f;
    public float turretY = 0f;
    public float gunX = 0f;
    //服务端补充
    public string id = "";		//哪个坦克
}

//开火
public class MsgFire : MsgBase
{
    public MsgFire() { protoName = "MsgFire"; }
    //炮弹初始位置、旋转
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float ex = 0f;
    public float ey = 0f;
    public float ez = 0f;
    //服务端补充
    public string id = "";		//哪个坦克
}

//击中
public class MsgHit : MsgBase
{
    public MsgHit() { protoName = "MsgHit"; }
    //击中谁
    public string targetId = "";
    //击中点	
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    //服务端补充
    public string id = "";      //哪个坦克
    public int hp = 0;          //被击中坦克血量
    public int damage = 0;		//受到的伤害
}

//生成道具同步
public class MsgItem : MsgBase
{
    public MsgItem() { protoName = "MsgItem"; }
    //道具的初始位置
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    //哪种类型的道具  0:血包 1:加速 2:攻击buff 
    public int opt;
}

//回血协议
public class MsgAddHp : MsgBase
{
    public MsgAddHp() { protoName = "MsgAddHp"; }
    public string id = "";      //哪个坦克
    public int recover;  //回复的血量
}

//增加攻击协议
public class MsgAddAttack : MsgBase
{
    public MsgAddAttack() { protoName = "MsgAddAttack"; }
    public string id = "";      //哪个坦克
    public int AttackBonus;  //攻击加成
}

//增加坦克敏捷协议
public class MsgAddAgility : MsgBase
{
    public MsgAddAgility() { protoName = "MsgAddAgility"; }
    public string id = "";      //哪个坦克
}