using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTank : MonoBehaviour
{
    //̹��ģ��
    private GameObject skin;

    //ת���ٶ�
    public float steer = 30;
    //�ƶ��ٶ�
    public float speed = 6f;
    //������ת�ٶ�
    public float turretSpeed = 25f;
    //����
    public Transform turret;
    //�ڹ�
    public Transform gun;
    //�����
    public Transform firePoint;
    //�ڵ�Cdʱ��
    public float fireCd = 1.5f;
    //��һ�η����ڵ���ʱ��
    public float lastFireTime = 0;
    //����
    protected Rigidbody rigidBody;
    //����ֵ
    public int hp = 100;
    //������
    public int attack = 15;
    //������һ�����
    public string id = "";
    //��Ӫ
    public int camp = 0;
    //�ڹ���ת
    public float minGunAngle = -20;
    public float maxGunAngle = 20;
    public float gunSpeed = 4f;
    //���Ӻ��Ĵ�
    public Transform wheels;
    public Transform track;
    //����
    public Terrain terrain;
    //Э��
    private Coroutine agilityCoroutine;
    // Use this for initialization
    public void Start()
    {

    }

    //��ʼ��
    public virtual void Init(string skinPath)
    {
        //Ƥ��
        GameObject skinRes = ResManager.LoadPrefab(skinPath);
        skin = (GameObject)Instantiate(skinRes);
        skin.transform.parent = this.transform;
        skin.transform.localPosition = Vector3.zero;
        skin.transform.localEulerAngles = Vector3.zero;
        //����
        rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.mass = 50; // ������Ҫ����
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = new Vector3(0, 2.5f, 1.47f);
        boxCollider.size = new Vector3(7, 5, 12);
        //�����ڹ�
        turret = skin.transform.Find("Turret");
        gun = turret.transform.Find("Gun");
        firePoint = gun.transform.Find("FirePoint");
        //�����Ĵ�
        wheels = skin.transform.Find("Wheels");
        track = skin.transform.Find("Track");
        //Ѱ�ҵ���
        terrain = Terrain.activeTerrain; // ��ȡ��ǰ��ĵ���
    }

    //�����ڵ�
    public Bullet Fire()
    {
        //�Ѿ�����
        if (IsDie())
        {
            return null;
        }
        //�����ڵ�
        GameObject bulletObj = new GameObject("bullet");
        bulletObj.layer = LayerMask.NameToLayer("Bullet");
        Bullet bullet = bulletObj.AddComponent<Bullet>();
        bullet.Init();
        bullet.tank = this;
        //λ��
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        //����ʱ��
        lastFireTime = Time.time;
        return bullet;
    }

    //�Ƿ�����
    public bool IsDie()
    {
        return hp <= 0;
    }

    //������
    public void Attacked(int att)
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //��Ѫ
        hp -= att;
        //����
        if (IsDie())
        {
            //��ʾ����Ч��
            GameObject obj = ResManager.LoadPrefab("explosion");
            GameObject explosion = Instantiate(obj, transform.position, transform.rotation);
            explosion.transform.SetParent(transform);
        }
    }

    //��Ѫ����Ѫ
    public void AddHp(int att)
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //��Ѫ
        hp += att;
        if(hp > 100) hp = 100;
    }

    //�񵽹������߼ӹ���
    public void AddAttack(int att)
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //��Ѫ
        attack += att;
        if (attack > 25) attack = 25;
    }

    //�����ݵ��߼�����
    public void AddAgility()
    {
        //�Ѿ�����
        if (IsDie())
        {
            return;
        }
        //����
        speed = 12;
        //�����ӵ�cd
        fireCd = 1f;
        if (id == GameMain.id)
        {
            CameraFollow cf = gameObject.GetComponent<CameraFollow>();
            cf.speed = speed;
        }
        // �������Э�������У���ֹͣ��
        if (agilityCoroutine != null)
        {
            StopCoroutine(agilityCoroutine);
        }
        //��ʼЭ��
        agilityCoroutine = StartCoroutine(CallFunctionAfterDelay(30f));
    }

    private IEnumerator CallFunctionAfterDelay(float delay)
    {
        // �ȴ�ָ����ʱ��
        yield return new WaitForSeconds(delay);
        //����
        speed = 6;
        //�����ӵ�cd
        fireCd = 1.5f;
        if (id == GameMain.id)
        {
            CameraFollow cf = gameObject.GetComponent<CameraFollow>();
            cf.speed = speed;
        }
        // ���Э������
        agilityCoroutine = null;
    }

    private void MyFunction()
    {
        Debug.Log("Function called after 60 seconds!");
        // �������������Ҫִ�еĴ���
    }

    // Update is called once per frame
    public void Update()
    {

    }
    //������ת���Ĵ�����
    public void WheelUpdate(float axis)
    {
        //�����ٶ�
        float v = Time.deltaTime * speed * axis * 100;
        //��תÿ������
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(new Vector3(v, 0, 0), Space.Self);//Χ������x��ת
        }
        //�����Ĵ�
        MeshRenderer mr = track.gameObject.GetComponent<MeshRenderer>();
        if (mr == null)
        {
            return;
        };
        Material mtl = mr.material;
        //��������ͼƫ��������Ӧ����MainMaps Offset.Y���Ӿ����γɹ���Ч��
        //256�ȶ�Ӧoffset�仯1
        mtl.mainTextureOffset += new Vector2(0, v / 256);
    }
}
