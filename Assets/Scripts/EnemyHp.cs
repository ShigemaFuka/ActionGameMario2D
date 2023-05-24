using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class EnemyHp : MonoBehaviour
{
    /// <summary> 「Player」スクリプトの「_attackValue」 </summary>
    [SerializeField] int _damageValue;
    /// <summary> 「Player」スクリプト </summary>
    [SerializeField] Player _scPlayer;
    /// <summary> エネミーのHPの初期設定 </summary>
    [SerializeField] int _enemyHpMax;
    [SerializeField] int _enemyHp;
    SpriteRenderer _spre;
    Animator _anim;
    Collider2D _col2d;



    // Start is called before the first frame update
    void Start()
    {
        // 「MyRogue_01」を取得
        GameObject _player = GameObject.Find("MyRogue_01");
        // 「Player」スクリプトを取得
        _scPlayer = _player.GetComponent<Player>();
        // エネミーのHPの初期化
        _enemyHp = _enemyHpMax;
        _spre = GetComponent<SpriteRenderer>();
        _spre.color = Color.white;
        _anim = _player.GetComponent<Animator>();
        _col2d = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(_anim.GetBool("isAttack_3") == false)
        {
            _col2d.enabled = false;
        }
        else if(_anim.GetBool("isAttack_3") == true)
        {
            _col2d.enabled = true;
        }
        */
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // 「Input.GetKeyDown(KeyCode.N)」がないとキー入力なくてもダメージ負う,,,,,,,,
        // けど入れると機能しない、、、、、
        if (coll.gameObject.tag == "Weapon") //&& Input.GetKeyDown(KeyCode.N))
        {
            _damageValue = _scPlayer._attackValue;
            // HP減らしていく
            _enemyHp = _enemyHp - _damageValue;  
            _spre.color = Color.red;

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
            _spre.color = Color.white;
        }
    }
}
