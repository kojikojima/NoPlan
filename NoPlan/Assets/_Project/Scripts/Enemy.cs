using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[System.Serializable]
public class EnemyStatus
{
    public int hp;
    public int atk;
    public int def;
    public int spd;
}

public class BuffStaus
{
    public float atkBuff;
    public float GetAtkBuff()
    {
        return atkBuff;
    }
}

public class ActionAIBase
{
    public Enemy owener;
    public virtual void Update() { }
    public virtual void TakeDamage() { }
}

public class LexActionAI:ActionAIBase
{

    public override void Update()
    {
        //if (owener.state == 戦闘中)
        if (owener.hp > 10)
        {
            Debug.Log("bb");
        }
    }

    public override void TakeDamage()
    {
        Debug.Log("aa");
    }
}

public enum BuffType
{
    Atk,
    Def,
    Spd,
    All,
}
public class BuffStatus
{
    public BuffType buffType;
    public float time;
    public int value;
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private int hitPoint = 10;
    
    private Collider2D collider;

    public ActionAIBase ai;

    public List<BuffStatus> buffStatus = new List<BuffStatus>();

    public int hp,maxhp;
    private int baseAtk, baseDef, baseSpd;
    public BuffStaus buff;

    public int AttackValue { get { return (int)((float)baseAtk * buff.GetAtkBuff()); } }

    public void SetUp(EnemyStatus status)
    {
        hp = GetDefaultHp(status);
        maxhp = status.hp;
        baseAtk = status.atk;
        baseDef = status.def;
        baseSpd = status.spd;
    }

    private int GetDefaultHp(EnemyStatus status)
    {
        // 最初から全快じゃない場合はなんか処理入れる
        return status.hp;
    }

    private void Start()
    {
        this.collider = this.GetComponent<Collider2D>();

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Mouse1))
            .Subscribe(_ =>
            {
                Vector2 pos = (Vector2)Input.mousePosition - new Vector2(Screen.width / 2, Screen.height / 2);
                var bullet = Instantiate(bulletPrefab);
                bullet.tag = "EnemyBullet";
                bullet.transform.position = this.transform.position;
                bullet.Shot(pos.normalized, 5f, 5);
            });

        this.OnTriggerEnter2DAsObservable()
            .Subscribe(x =>
            {
                var bullet = x.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    if(x.tag == "PlayerBullet")
                    {
                        TakeDamage(bullet.Fire);
                        bullet.BulletDestroy();
                    }
                }
            }).AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => this.hitPoint <= 0)
            .Subscribe(_ =>
            {
                Destroy(this.gameObject);
            }).AddTo(this);
    }

    private void Update()
    {
        //ai.Update();
    }

    public bool TakeDamage(int damage)
    {
        this.hitPoint -= damage;
        hp -= damage;
        ai.TakeDamage();
        return true;
    }
}
