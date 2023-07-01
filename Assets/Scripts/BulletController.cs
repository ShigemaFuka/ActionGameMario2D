using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField, Tooltip("弾が飛ぶ速さ")] float _speed = 3f;
    [SerializeField, Tooltip("弾の生存期間（秒）")] float _lifeTime = 5f;
    Rigidbody2D _rb;
    // [SerializeField, Tooltip("マズル")] GameObject _muzzle;
    GameObject _enemy;
    EnemyController _enemyHpScript;
    //[SerializeField] AudioClip _clip;
    [SerializeField, Tooltip("エフェクト")] GameObject _effectPrefab; 


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // 参照(Attack関数を使いたい)
        _enemy = GameObject.Find("Enemy");
        //_enemyHpScript = _enemy.GetComponent<EnemyHp>();

        /*
        _muzzle = GameObject.Find("Enemy/Muzzle");
        // マズルの位置から弾丸発射
        this.transform.position = _muzzle.transform.position;
        */
        _rb.velocity = Vector2.left * _speed;

        // 生存期間が経過したら自分自身を破棄する
        Destroy(this.gameObject, _lifeTime);

        //_clip = GetComponent<AudioSource>().clip; 
    }

    // 踏める 
    void OnTriggerEnter2D(Collider2D coll)
    {
        // プレイヤーに攻撃されたら消える
        if (coll.gameObject.tag == "Weapon")        
        {
            /*
            // 爆発の音と画像を出す 
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            */
            // エフェクトとなるプレハブが設定されていたら、それを生成する
            if (_effectPrefab)
            {
                Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
            }
            Destroy(gameObject);
        } 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // エフェクトとなるプレハブが設定されていたら、それを生成する
        if (_effectPrefab)
        {
            Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
        }
        /*
        // 爆発の音と画像を出す 
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        */
        // 削除
        Destroy(gameObject);
    }
}
