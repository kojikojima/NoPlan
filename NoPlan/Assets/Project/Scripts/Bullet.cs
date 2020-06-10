using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    public int Fire { private set; get; } = 10;

    public void Shot(Vector2 vec, float speed)
    {
        var rb = GetComponent<Rigidbody2D>();
        
        Observable.EveryUpdate()
          .Subscribe(_ =>
          {
              rb.MovePosition(rb.position + vec * speed * Time.fixedDeltaTime);
          }).AddTo(this);
    }
}
