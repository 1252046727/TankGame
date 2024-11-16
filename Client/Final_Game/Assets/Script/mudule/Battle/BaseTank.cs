using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTank : MonoBehaviour
{
    //坦克模型
    private GameObject skin;

    //转向速度
    public float steer = 30;
    //移动速度
    public float speed = 6f;
    //炮塔旋转速度
    public float turretSpeed = 25f;
    //炮塔
    public Transform turret;
    //炮管
    public Transform gun;
    //发射点
    public Transform firePoint;
    //炮弹Cd时间
    public float fireCd = 1.5f;
    //上一次发射炮弹的时间
    public float lastFireTime = 0;
    //物理
    protected Rigidbody rigidBody;
    //生命值
    public int hp = 100;
    //攻击力
    public int attack = 15;
    //属于哪一名玩家
    public string id = "";
    //阵营
    public int camp = 0;
    //炮管旋转
    public float minGunAngle = -20;
    public float maxGunAngle = 20;
    public float gunSpeed = 4f;
    //轮子和履带
    public Transform wheels;
    public Transform track;
    //地形
    public Terrain terrain;
    //协程
    private Coroutine agilityCoroutine;
    // Use this for initialization
    public void Start()
    {

    }

    //初始化
    public virtual void Init(string skinPath)
    {
        //皮肤
        GameObject skinRes = ResManager.LoadPrefab(skinPath);
        skin = (GameObject)Instantiate(skinRes);
        skin.transform.parent = this.transform;
        skin.transform.localPosition = Vector3.zero;
        skin.transform.localEulerAngles = Vector3.zero;
        //物理
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.mass = 50; // 根据需要调整
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(0, 2.5f, 1.47f);
        boxCollider.size = new Vector3(7, 5, 12);
        //炮塔炮管
        turret = skin.transform.Find("Turret");
        gun = turret.transform.Find("Gun");
        firePoint = gun.transform.Find("FirePoint");
        //轮子履带
        wheels = skin.transform.Find("Wheels");
        track = skin.transform.Find("Track");
        //寻找地形
        terrain = Terrain.activeTerrain; // 获取当前活动的地形
    }

    //发射炮弹
    public Bullet Fire()
    {
        //已经死亡
        if (IsDie())
        {
            return null;
        }
        //产生炮弹
        GameObject bulletObj = new GameObject("bullet");
        bulletObj.layer = LayerMask.NameToLayer("Bullet");
        Bullet bullet = bulletObj.AddComponent<Bullet>();
        bullet.Init();
        bullet.tank = this;
        //位置
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        //更新时间
        lastFireTime = Time.time;
        return bullet;
    }

    //是否死亡
    public bool IsDie()
    {
        return hp <= 0;
    }

    //被攻击
    public void Attacked(int att)
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //扣血
        hp -= att;
        //死亡
        if (IsDie())
        {
            //显示焚烧效果
            GameObject obj = ResManager.LoadPrefab("explosion");
            GameObject explosion = Instantiate(obj, transform.position, transform.rotation);
            explosion.transform.SetParent(transform);
        }
    }

    //捡到血包回血
    public void AddHp(int att)
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //扣血
        hp += att;
        if(hp > 100) hp = 100;
    }

    //捡到攻击道具加攻击
    public void AddAttack(int att)
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //扣血
        attack += att;
        if (attack > 25) attack = 25;
    }

    //捡到敏捷道具加敏捷
    public void AddAgility()
    {
        //已经死亡
        if (IsDie())
        {
            return;
        }
        //移速
        speed = 12;
        //发射子弹cd
        fireCd = 1f;
        if (id == GameMain.id)
        {
            CameraFollow cf = gameObject.GetComponent<CameraFollow>();
            cf.speed = speed;
        }
        // 如果已有协程在运行，先停止它
        if (agilityCoroutine != null)
        {
            StopCoroutine(agilityCoroutine);
        }
        //开始协程
        agilityCoroutine = StartCoroutine(CallFunctionAfterDelay(30f));
    }

    private IEnumerator CallFunctionAfterDelay(float delay)
    {
        // 等待指定的时间
        yield return new WaitForSeconds(delay);
        //移速
        speed = 6;
        //发射子弹cd
        fireCd = 1.5f;
        if (id == GameMain.id)
        {
            CameraFollow cf = gameObject.GetComponent<CameraFollow>();
            cf.speed = speed;
        }
        // 清空协程引用
        agilityCoroutine = null;
    }

    private void MyFunction()
    {
        Debug.Log("Function called after 60 seconds!");
        // 在这里添加你想要执行的代码
    }

    // Update is called once per frame
    public void Update()
    {

    }
    //轮子旋转，履带滚动
    public void WheelUpdate(float axis)
    {
        //计算速度
        float v = Time.deltaTime * speed * axis * 100;
        //旋转每个轮子
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(new Vector3(v, 0, 0), Space.Self);//围绕自身x轴转
        }
        //滚动履带
        MeshRenderer mr = track.gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
        {
            return;
        };
        Material mtl = mr.material;
        //设置主帖图偏移量（对应材质MainMaps Offset.Y）视觉上形成滚动效果
        //256度对应offset变化1
        mtl.mainTextureOffset += new Vector2(0, v / 256);
    }
}
