using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// 敵キャラの行動制御 
/// 「　Weapon　」に接触したときのみダメージを受ける
/// </summary>
public class EnemyController : MonoBehaviour
{
    // 参照
    [SerializeField, Tooltip("「Player」スクリプトの「_attackValue」")] int _damageValue;
    [SerializeField, Tooltip("プレイヤーNameを入れる")] string _playerName;
    [SerializeField, Tooltip("「Player」スクリプト")] PlayerMove _scPlayer;
    // HP
    //[SerializeField, Tooltip("エネミーのHPの初期設定")] int _enemyHpMax;
    [SerializeField, Header("入力不要")] int _enemyHp;
    
    // その他
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    Collider2D _col2d;


    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDate characterDate;
    [SerializeField, Tooltip("敵の強さを選択")]CharacterType characterType; 

    // Start is called before the first frame update
    void Start()
    {
        // 「Player」を取得
        GameObject _player = GameObject.Find(_playerName);
        // 「Player」スクリプトを取得 → 攻撃値が欲しい
        _scPlayer = _player.GetComponent<PlayerMove>();

        // エネミーのHPの初期化
        if (characterType == CharacterType.Nomal)
            _enemyHp = characterDate.achievementList[1].Maxhp;
        else if(characterType == CharacterType.Midiam)
            _enemyHp = characterDate.achievementList[2].Maxhp;
        else if(characterType == CharacterType.Hard)
            _enemyHp = characterDate.achievementList[3].Maxhp;

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
        if (coll.gameObject.tag == "Weapon") 
        {
            AttackController scr = coll.transform.root.gameObject.GetComponent<AttackController>();
            scr.Attack();
            _damageValue = scr._attackValue;

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
