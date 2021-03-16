using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    public int Fire { private set; get; } = 10;

    private float bulletLifeTime = 5;

    private void Start()
    {
        Invoke("BulletDestroy", bulletLifeTime);
    }

    public void Shot(Vector2 vec, float speed, int atk)
    {
        Fire = atk;

        var rb = GetComponent<Rigidbody2D>();

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, GetAim(rb.position, vec));

        Observable.EveryUpdate()
          .Subscribe(_ =>
          {
              rb.MovePosition(rb.position + vec * speed * Time.fixedDeltaTime);
          }).AddTo(this);
        
    }

    public float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Wall")
        {
            BulletDestroy();
        }
    }

    public void BulletDestroy()
    {
        Destroy(this.gameObject);
    }
}
