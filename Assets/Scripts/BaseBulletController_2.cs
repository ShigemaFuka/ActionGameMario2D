using System.Collections;
using UnityEngine;

/// <summary>
/// 弾丸の制御の基底クラス
/// 時間経過、武器接触、衝突、したら自身を破棄
/// </summary>
public abstract class BaseBulletController_2 : MonoBehaviour
{
    // GMをプレハブ化して直入れしても意味ない、シーン上にないとダメ(インスタンス化) 
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("ScriptableObjectなキャラのパラメータ")] CharacterDates _characterDate = default;
    [SerializeField, Tooltip("エフェクト")] GameObject _crashEffectPrefab = default;
    [SerializeField] float _destroyTime = 5f;
    //オブジェクトプール用コントローラー格納用変数宣言
    ObjectPoolController _objectPool;

    void Start()
    {
        MoveBullet();
        //Destroy(gameObject, _destroyTime); 
        StartCoroutine(DestroyBullet());
        // シーンに１つしかないから 
        _gameManager = FindObjectOfType<GameManager>();
        //オブジェクトプールを取得
        _objectPool = FindObjectOfType<ObjectPoolController>();
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
            if (_crashEffectPrefab)
            {
                Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
            }
            _gameManager.AddScore(_characterDate.Score);
            //Destroy(gameObject);
            HideFromStage();
        }
    }

    /// <summary>
    /// 何かしらに衝突したら自身を破棄する
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // エフェクトとなるプレハブが設定されていたら、それを生成する
        if (_crashEffectPrefab)
        {
            Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
        }
        // 削除
        //Destroy(gameObject);
        HideFromStage();
    }

    /// <summary>
    /// Start関数で呼ばれる 
    /// </summary>
    public abstract void MoveBullet();

    void HideFromStage()
    {
        //オブジェクトプールのCollect関数を呼び出し自身を回収
        _objectPool.Collect(this.gameObject);
    }
    //コルーチン
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_destroyTime);
        HideFromStage();
    }
}
