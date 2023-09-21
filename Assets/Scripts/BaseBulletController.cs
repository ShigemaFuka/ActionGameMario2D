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
    //[SerializeField, Tooltip("エフェクト")] GameObject _crashEffectPrefab = default; 
    [SerializeField, Tooltip("消滅時のエフェクト")] GameObject _burnEffect = default;
    [SerializeField, Tooltip("格納までの時間")] float _collectTime = 5f;
    [Tooltip("格納された弾丸を管理しているスクリプト")] public MakeBulletObjectPool _makeBulletObjectPool = default;

    //オブジェクトプール用コントローラー格納用変数宣言
    //ObjectPoolController _objectPool;

    void Start()
    {
        if (!_makeBulletObjectPool) _makeBulletObjectPool = FindAnyObjectByType<MakeBulletObjectPool>();
        //MoveBullet();
        //Destroy(gameObject, _destroyTime);
        // シーンに１つしかないから 
        _gameManager = FindAnyObjectByType<GameManager>();
        _wfs = new WaitForSeconds(_collectTime);
        //オブジェクトプールを取得
        //_objectPool = FindObjectOfType<ObjectPoolController>();
    }
    void OnEnable()
    {
        MoveBullet();
        TimeLimit();
        _burnEffect.SetActive(false);
    }
    /// <summary>
    /// プレイヤーに攻撃されたら自身を破棄 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Weapon"))
        {
            // エフェクトとなるプレハブが設定されていたら、それを生成する
            _burnEffect.SetActive(true);
            _burnEffect.transform.position = gameObject.transform.position;
            _gameManager.AddScore(_characterDate.Score);
            //Destroy(gameObject);
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
        _burnEffect.SetActive(true);
        _burnEffect.transform.position = gameObject.transform.position;
        //_gameManager.AddScore(_characterDate.Score);
        // 削除
        //Destroy(gameObject);
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
        _burnEffect.SetActive(true);
        _burnEffect.transform.position = gameObject.transform.position;
        BurnEffectToFalse();
        _makeBulletObjectPool.Collect(gameObject);
    }
    WaitForSeconds _BETFwfs = new WaitForSeconds(0.2f);
    IEnumerator BurnEffectToFalse()
    {
        yield return _BETFwfs;
        _burnEffect.SetActive(false);
        yield return _BETFwfs;
        _makeBulletObjectPool.Collect(gameObject);
    }
    /// <summary>
    /// OnEnable()関数で呼ばれる 
    /// </summary>
    public abstract void MoveBullet();
}
