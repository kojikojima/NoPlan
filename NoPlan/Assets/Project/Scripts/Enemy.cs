using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoint = 10;
    
    private Collider2D collider;

    private void Start()
    {
        this.collider = this.GetComponent<Collider2D>();

        this.OnTriggerEnter2DAsObservable()
            .Subscribe(x =>
            {
                var bullet = x.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    if(x.tag == "PlayerBullet")
                    {
                        TakeDamage(bullet.Fire);
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
