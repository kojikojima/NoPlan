using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private int hitPoint = 10;
    
    private Collider2D collider;

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

    public bool TakeDamage(int damage)
    {
        this.hitPoint -= damage;
        return true;
    }
}
