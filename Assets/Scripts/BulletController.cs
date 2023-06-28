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
    EnemyHp _enemyHpScript;


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
    }

    // 踏める 
    void OnTriggerEnter2D(Collider2D coll)
    {
        // プレイヤーに攻撃されたら消える
        if (coll.gameObject.tag == "Weapon")        
        {
            Destroy(gameObject);           
        } 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // 削除
        Destroy(gameObject);
    }
}
