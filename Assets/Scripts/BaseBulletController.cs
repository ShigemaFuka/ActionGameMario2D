using UnityEngine;
using System.Collections;

/// <summary>
/// 弾丸の制御の基底クラス
/// 時間経過、武器接触、衝突、したら自身を破棄
/// </summary>
public abstract class BaseBulletController : MonoBehaviour
{
    // GMをプレハブ化して直入れしても意味ない、シーン上にないとダメ(インスタンス化) 
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("ScriptableObjectなキャラのパラメータ")] CharacterDates _characterDate = default;
    [SerializeField, Tooltip("消滅時のエフェクト")] ParticleSystem[] _burnEffectParticles = default;
    [SerializeField, Tooltip("消滅時のエフェクト")] AudioSource _audioSource = default;
    [SerializeField, Tooltip("格納までの時間")] float _collectTime = 5f;
    [Tooltip("格納された弾丸を管理しているスクリプト")] public MakeBulletObjectPool _makeBulletObjectPool = default;

    void Start()
    {
        if (!_makeBulletObjectPool) _makeBulletObjectPool = FindAnyObjectByType<MakeBulletObjectPool>();
        // シーンに１つしかないから 
        _gameManager = FindAnyObjectByType<GameManager>();
        _wfs = new WaitForSeconds(_collectTime);
    }
    void OnEnable()
    {
        MoveBullet();
        TimeLimit();
    }
    /// <summary>
    /// プレイヤーに攻撃されたら自身を破棄 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Weapon") || coll.gameObject.CompareTag("RangeWeapon"))
        {
            // エフェクトとなるプレハブが設定されていたら、それを生成する
            _audioSource.PlayOneShot(_audioSource.clip);
            foreach (var bep in _burnEffectParticles)
            {
                bep.transform.position = gameObject.transform.position;
                bep.Play();
            }
            _gameManager.AddScore(_characterDate.Score);
            BurnEffectToFalse();
            _makeBulletObjectPool.Collect(gameObject);
        }
    }

    /// <summary>
    /// 何かしらに衝突したら自身を破棄する
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // エフェクトとなるプレハブが設定されていたら、それを生成する
        _audioSource.PlayOneShot(_audioSource.clip);
        foreach (var bep in _burnEffectParticles)
        {
            bep.transform.position = gameObject.transform.position;
            bep.Play();
        }
        BurnEffectToFalse();
        _makeBulletObjectPool.Collect(gameObject);
    }
    WaitForSeconds _wfs;
    /// <summary>
    /// 一定時間経過したら格納
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeLimit()
    {
        yield return _wfs;
        _audioSource.transform.position = gameObject.transform.position;
        _audioSource.PlayOneShot(_audioSource.clip);
        foreach (var bep in _burnEffectParticles)
        {
            bep.transform.position = gameObject.transform.position;
            bep.Play();
        }
        BurnEffectToFalse();
        _makeBulletObjectPool.Collect(gameObject);
    }
    WaitForSeconds _BETFwfs = new WaitForSeconds(0.2f);
    IEnumerator BurnEffectToFalse()
    {
        yield return _BETFwfs;
        _makeBulletObjectPool.Collect(gameObject);
    }
    /// <summary>
    /// OnEnable()関数で呼ばれる 
    /// </summary>
    public abstract void MoveBullet();
}