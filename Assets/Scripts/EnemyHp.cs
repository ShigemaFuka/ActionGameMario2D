using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class EnemyHp : MonoBehaviour
{
    // 参照
    [SerializeField, Tooltip("「Player」スクリプトの「_attackValue」")] int _damageValue;
    [SerializeField, Tooltip("プレイヤーNameを入れる")] string _playerName;
    [SerializeField, Tooltip("「Player」スクリプト")] PlayerMove _scPlayer;
    // HP
    [SerializeField, Tooltip("エネミーのHPの初期設定")] int _enemyHpMax;
    [SerializeField, Header("入力不要")] int _enemyHp;
    // 攻撃値
    [Header("5以上を推奨")][SerializeField, Tooltip("「_attackValue」を決めるための値")] int _atVa;
    //[Header("入力不要")][Tooltip("プレイヤー相手に使う攻撃値")] public int _attackValue;
    // その他
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    Collider2D _col2d;
    



    // Start is called before the first frame update
    void Start()
    {
        // 「Player」を取得
        GameObject _player = GameObject.Find(_playerName);
        // 「Player」スクリプトを取得 → 攻撃値が欲しい
        _scPlayer = _player.GetComponent<PlayerMove>();
        // エネミーのHPの初期化
        _enemyHp = _enemyHpMax;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
        _anim = _player.GetComponent<Animator>();
        _col2d = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon")        //&& Input.GetKeyDown(KeyCode.N))
        {
            _damageValue = _scPlayer._attackValue;
            // HP減らしていく
            _enemyHp = _enemyHp - _damageValue;  
            _spriteRenderer.color = Color.red;

            if (_enemyHp < _damageValue)
            {
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
