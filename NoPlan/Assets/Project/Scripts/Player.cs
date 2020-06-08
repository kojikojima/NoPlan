using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

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
    }
}
