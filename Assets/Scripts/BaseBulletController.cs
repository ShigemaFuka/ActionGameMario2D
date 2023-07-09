using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

/// <summary>
/// 弾丸の制御の基底クラス
/// 時間経過、武器接触、衝突、したら自身を破棄
/// </summary>
public abstract class BaseBulletController : MonoBehaviour
{
    // GMをプレハブ化して直入れしても意味ない、シーン上にないとダメ 
    [SerializeField] GameManager _gameManager;
    [SerializeField, Tooltip("ScriptableObjectなキャラのパラメータ")] CharacterDates _characterDate; 
    [SerializeField, Tooltip("エフェクト")] GameObject _crashEffectPrefab = default;
    [SerializeField] float _destroyTime = 5f; 

    void Start()
    {
        MoveBullet(); 
        Destroy(gameObject, _destroyTime); 
        // シーンに１つしかないから 
        _gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// プレイヤーに攻撃されたら自身を破棄 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon")
        {
            // エフェクトとなるプレハブが設定されていたら、それを生成する
            if (_crashEffectPrefab)
            {
                Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
            }
            _gameManager.AddScore(_characterDate.Score);
            Destroy(gameObject);
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
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Start関数で呼ばれる 
    /// </summary>
    public abstract void MoveBullet(); 
}
