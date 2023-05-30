using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    // HP
    [SerializeField, Tooltip("プレイヤーのHPの初期設定")] int _playerHpMax;
    [SerializeField, Header("入力不要")] int _playerHp;
    // 参照
    [SerializeField, Tooltip("「EnemyHp」スクリプト")] EnemyHp _enemyHpScript;
    [SerializeField, Tooltip("「EnemyHp」スクリプトの「_attackValue」")] int _damageValue;

    // その他


    void Start()
    {
        // 「Enemy」を取得
        GameObject _enemy = GameObject.Find("Enemy");
        // 「EnemyHp」スクリプトを取得 → 攻撃値が欲しい
        _enemyHpScript = _enemy.GetComponent<EnemyHp>();

        // プレイヤーのHPの初期化
        _playerHp = _playerHpMax;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")        
        {
            // 参照したものを代入
            _damageValue = _enemyHpScript._attackValue;
            // HP減らしていく
            _playerHp = _playerHp - _damageValue;
            

            if (_playerHp < _damageValue)
            {
                Destroy(gameObject);
            }
            Debug.Log(_playerHp);
            //Debug.Log("弾丸に接触");

        }
    }
}