using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵キャラの行動制御 
/// 「　Weapon　」に接触したときのみダメージを受ける
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager; 
    int _damageValue; 
    // HP
    [SerializeField, Header("入力不要")] int _enemyHp;
    
    // その他
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    Collider2D _col2d;

    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDates _characterDate;
    [SerializeField, Tooltip("エフェクト")] GameObject _effectPrefab;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); 

        // エネミーのHPの初期化
        if (_characterDate)
            _enemyHp = _characterDate.Maxhp;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
        _col2d = GetComponent<Collider2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon") 
        {
            // 攻撃値 
            AttackValueController scr = coll.transform.root.gameObject.GetComponent<AttackValueController>();
            scr.Attack();
            _damageValue = scr._attackValue;

            // HP減らしていく
            _enemyHp = _enemyHp - _damageValue;  
            _spriteRenderer.color = Color.red;

            if (_enemyHp < _damageValue)
            {
                // エフェクトとなるプレハブが設定されていたら、それを生成する
                if (_effectPrefab)
                {
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
                }
                // スコア加算 
                _gameManager.AddScore(_characterDate.Score);
                // キル数加算  
                _gameManager.KillCount += 1;
                Debug.Log(_gameManager.KillCount); 
                Destroy(gameObject);
            }
            Debug.Log(_enemyHp);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Weapon")
        {
            _spriteRenderer.color = Color.white;
        }
    }
}
