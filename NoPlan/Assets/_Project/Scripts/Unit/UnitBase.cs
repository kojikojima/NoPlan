using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// ユニットの基底クラス
/// </summary>
public abstract class UnitBase : MonoBehaviour
{
    /*-------------------------------------------------------------------*/
    /* 変数                                                              */
    /*-------------------------------------------------------------------*/
    [SerializeField]
    protected UnitActionControllerBase _actionController;
    [SerializeField]
    protected MonoBehaviour _motionController;

    protected IntReactiveProperty _hitPoint = new IntReactiveProperty();
    protected IntReactiveProperty _maxHitPoint = new IntReactiveProperty();

    protected Subject<Collider2D> _hitColliderSubject = new Subject<Collider2D>();
    protected Subject<AttackInfo> _takeDamageSubject = new Subject<AttackInfo>();

    /*-------------------------------------------------------------------*/
    /* プロパティ                                                        */
    /*-------------------------------------------------------------------*/
    // TODO: 仮
    /// <summary>
    /// バフ, デバフ情報
    /// </summary>
    public Unit BuffStatusInfo { get; protected set; } = new Unit();

    /// <summary>
    /// 現在HP
    /// </summary>
    public IReadOnlyReactiveProperty<int> HitPoint => _hitPoint;

    /// <summary>
    /// 最大HP
    /// </summary>
    public IReadOnlyReactiveProperty<int> MaxHitPoint => _maxHitPoint;

    /// <summary>
    /// 攻撃力
    /// </summary>
    public abstract int Power { get; }

    /// <summary>
    /// 防御力
    /// </summary>
    public abstract int Defense { get; }

    /// <summary>
    /// 命中値
    /// </summary>
    public abstract int HitValue { get; }

    /// <summary>
    /// 衝突判定時に発行されるイベント
    /// </summary>
    public IObservable<Collider2D> HitColliderObservable => _hitColliderSubject;

    /// <summary>
    /// 被ダメージ時に発行されるイベント
    /// </summary>
    public IObservable<AttackInfo> TakeDamageObservable => _takeDamageSubject;

    /*-------------------------------------------------------------------*/
    /* 外部メソッド                                                      */
    /*-------------------------------------------------------------------*/
    /// <summary>
    /// 攻撃を受ける
    /// </summary>
    /// <param name="attackInfo"></param>
    /// <returns></returns>
    public virtual bool TakeAttack(AttackInfo attackInfo)
    {
        // 命中判定
        if (_JudgeHitAttack(attackInfo))
        {
            // ダメージ計算
            _TakeDamage(attackInfo);
            return true;
        }
        else
        {
            // はずれ
            return false;
        }
    }

    /*-------------------------------------------------------------------*/
    /* 内部メソッド                                                      */
    /*-------------------------------------------------------------------*/
    /// <summary>
    /// 初期化
    /// </summary>
    protected void Awake()
    {
        // アクション管理クラスの初期化
        _actionController?.SetUp(this);

        // HPが0になった時のイベント
        HitPoint.Pairwise()
            .Where(value => value.Previous > 0 && 0 >= value.Previous)
            .Subscribe(_ =>
            {
                _LoseHitPoint();
            });

        // 衝突イベント登録
        this.OnTriggerEnter2DAsObservable()
            .Subscribe(collider =>
            {
                // 衝突処理
                _HitCollider(collider);
            }).AddTo(this);
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    protected virtual void _Die()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 衝突処理
    /// </summary>
    /// <param name="collider"></param>
    protected virtual void _HitCollider(Collider2D collider)
    {
        if (collider.CompareTag(TagType.Bullet.GetTagName()))
        {
            if (collider.TryGetComponent<Bullet>(out var bullet))
            {
                Debug.Log($"当たった弾:{bullet.name}");
            }
        }

        // 衝突判定時のイベント発行
        _hitColliderSubject.OnNext(collider);
    }

    /// <summary>
    /// HPがなくなった時
    /// </summary>
    protected virtual void _LoseHitPoint()
    {
        _Die();
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="attackInfo"></param>
    /// <returns></returns>
    protected virtual bool _TakeDamage(AttackInfo attackInfo)
    {
        _hitPoint.Value = attackInfo.Value;

        // 被ダメージイベントを発行
        _takeDamageSubject.OnNext(attackInfo);
        return true;
    }

    /// <summary>
    /// 被攻撃の命中判定
    /// </summary>
    /// <param name="attackInfo"></param>
    /// <returns></returns>
    protected virtual bool _JudgeHitAttack(AttackInfo attackInfo)
    {
        // TODO: 命中率の計算
        return HitValue > 10;
    }
}
