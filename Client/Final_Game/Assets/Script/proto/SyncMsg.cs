//ͬ��̹����Ϣ
public class MsgSyncTank : MsgBase
{
    public MsgSyncTank() { protoName = "MsgSyncTank"; }
    //λ�á���ת��������ת
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float ex = 0f;
    public float ey = 0f;
    public float ez = 0f;
    public float turretY = 0f;
    public float gunX = 0f;
    //����˲���
    public string id = "";		//�ĸ�̹��
}

//����
public class MsgFire : MsgBase
{
    public MsgFire() { protoName = "MsgFire"; }
    //�ڵ���ʼλ�á���ת
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float ex = 0f;
    public float ey = 0f;
    public float ez = 0f;
    //����˲���
    public string id = "";		//�ĸ�̹��
}

//����
public class MsgHit : MsgBase
{
    public MsgHit() { protoName = "MsgHit"; }
    //����˭
    public string targetId = "";
    //���е�	
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    //����˲���
    public string id = "";      //�ĸ�̹��
    public int hp = 0;          //������̹��Ѫ��
    public int damage = 0;		//�ܵ����˺�
}

//���ɵ���ͬ��
public class MsgItem : MsgBase
{
    public MsgItem() { protoName = "MsgItem"; }
    //���ߵĳ�ʼλ��
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    //�������͵ĵ���  0:Ѫ�� 1:���� 2:����buff 
    public int opt;
}

//��ѪЭ��
public class MsgAddHp : MsgBase
{
    public MsgAddHp() { protoName = "MsgAddHp"; }
    public string id = "";      //�ĸ�̹��
    public int recover;  //�ظ���Ѫ��
}

//���ӹ���Э��
public class MsgAddAttack : MsgBase
{
    public MsgAddAttack() { protoName = "MsgAddAttack"; }
    public string id = "";      //�ĸ�̹��
    public int AttackBonus;  //�����ӳ�
}

//����̹������Э��
public class MsgAddAgility : MsgBase
{
    public MsgAddAgility() { protoName = "MsgAddAgility"; }
    public string id = "";      //�ĸ�̹��
}