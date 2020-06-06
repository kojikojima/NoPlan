using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigi;

    /// <summary>
    /// あわけ
    /// </summary>
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();

        Observable.EveryUpdate()
          .Where(_ => Input.GetKey(KeyCode.W))
          .Subscribe(_ => 
          {
              var sequence = rigi.DOMoveY( 5f, 1f)
                .SetRelative(true);
          });

        Observable.EveryUpdate()
          .Where(_ => Input.GetKey(KeyCode.A))
          .Subscribe(_ =>
          {
              var sequence = rigi.DOMoveX(-5f, 1f)
                .SetRelative(true);
          });

        Observable.EveryUpdate()
          .Where(_ => Input.GetKey(KeyCode.S))
          .Subscribe(_ =>
          {
              var sequence = rigi.DOMoveY(-5f, 1f)
                .SetRelative(true);
          });

        Observable.EveryUpdate()
          .Where(_ => Input.GetKey(KeyCode.D))
          .Subscribe(_ =>
          {
              var sequence = rigi.DOMoveX( 50000f, 1f)
                .SetRelative(true);
          });
    }
}
