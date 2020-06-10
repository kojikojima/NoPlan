using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Bullet bulletPrefab;

    private Rigidbody2D rb;

    /// <summary>
    /// あわけ
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        var vec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Observable.EveryUpdate()
            .Do(_ => { vec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); })
            .Where(_ => vec != Vector2.zero)
            .Subscribe(_ => 
            {
                // 移動量制限
                if (vec.sqrMagnitude > Mathf.Sqrt(1f))
                {
                    vec = vec.normalized;
                }

                rb.MovePosition(rb.position + vec * speed * Time.fixedDeltaTime);
            });

        Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Mouse0))
            .Subscribe(_ =>
            {
                Vector2 pos = (Vector2)Input.mousePosition - new Vector2(Screen.width/2, Screen.height/2);
                var bullet = Instantiate(bulletPrefab);
                bullet.transform.position = this.transform.position;
                bullet.Shot(pos.normalized, 5f);
            });
    }
}