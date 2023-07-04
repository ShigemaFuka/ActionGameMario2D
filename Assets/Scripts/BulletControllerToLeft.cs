using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成された瞬間自力で左に移動する
/// 破棄も自身で行う 
/// </summary>
public class BulletControllerToLeft : BaseBulletController 
{
    [SerializeField, Tooltip("弾が飛ぶ速さ")] float _speed = 3f;
    Rigidbody2D _rb; 

    public override void MoveBullet()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _rb.velocity = Vector2.left * _speed; 
    }
    
    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    // プレイヤーに攻撃されたら消える
    //    if (coll.gameObject.tag == "Weapon")        
    //    {
    //        /*
    //        // 爆発の音と画像を出す 
    //        AudioSource.PlayClipAtPoint(_clip, transform.position);
    //        */
    //        // エフェクトとなるプレハブが設定されていたら、それを生成する
    //        if (_effectPrefab)
    //        {
    //            Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
    //        }
    //        Destroy(gameObject);
    //    } 
    //}

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    // エフェクトとなるプレハブが設定されていたら、それを生成する
    //    if (_effectPrefab)
    //    {
    //        Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
    //    }
    //    /*
    //    // 爆発の音と画像を出す 
    //    AudioSource.PlayClipAtPoint(_clip, transform.position);
    //    */
    //    // 削除
    //    Destroy(gameObject);
    //}
}
