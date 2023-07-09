using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine; 

/// <summary>
/// プレイヤーの移動、ジャンプ、初期位置を制御 
/// </summary>
public class PlayerController : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    [SerializeField, Tooltip("ジャンプ時の計算で使う")] float _jumpPower;
    [Tooltip("移動速度の計算で使う")] float _movePower;
    [SerializeField, Tooltip("歩きの速さ")] float _movePowerDef = 30f;
    [SerializeField, Tooltip("走りの速さ")] float _movePowerUp;
    [Tooltip("水平方向の入力値")] float _h;
    [Tooltip("左右反転")]  Vector3 _scale;
    [SerializeField, Tooltip("ジャンプできるかの接地判定")] bool _isJump;
    [SerializeField, Tooltip("ジャンプできるかのカウント")] int _jumpCount;
    [SerializeField, Tooltip("ジャンプできて良いオブジェクトの名")] string[] _jumpables;
    [SerializeField, Tooltip("スタートポジション")] GameObject _startPosition;
    [SerializeField] GameManager _gameManager = default;


    /// <summary>プレイヤーの状態を表す</summary>
    [SerializeField] PlayerState _state = PlayerState.Normal;

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
        _movePowerUp = _movePowerDef * 2f;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = GameObject.Find("StartPos");
        this.transform.position = _startPosition.transform.position;
    }

    void Update()
    {
        _h = Input.GetAxis("Horizontal");
        FlipX(_h);

        MoveControl();
        Jump(); 

        if (gameObject.transform.position.y < -13)
        {
            _gameManager.GameOver(); 
        }

        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);

    }

    void Jump()
    {
        // ジャンプ
        if (_isJump && _jumpCount < 2)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _jumpCount++;
            }
        }
        else if (_jumpCount >= 2)
        {
            _isJump = false;
        }
    }

    void MoveControl()
    {
        // 攻撃        
        if (Input.GetKeyDown(KeyCode.N))
        {
            _anim.SetTrigger("isAttack"); 
        }
        // 方向指示があるとき 
        if(_h != 0)
        {
            if (Input.GetKey(KeyCode.B))
            {
                // 走る
                _movePower = _movePowerUp;
                _anim.SetBool("isRun", true);
            }
            else
            {
                // 歩く
                _movePower = _movePowerDef;
                _anim.SetBool("isRun", false);
                _anim.SetBool("isWalk", true);
            }
        }
        // 方向指示がないとき
        else
        {
            _anim.SetBool("isWalk", false);
            _anim.SetBool("isRun", false);
        }        
    }

    //左右反転
    /// <param name="horizontal">水平方向の入力値</param>
    void FlipX(float horizontal)
    {
         _scale = this.transform.localScale;

        if(horizontal > 0)
        {
            //_scaleX
            this.transform.localScale = new Vector3(Mathf.Abs(_scale.x), _scale.y, _scale.z);
        }
        else if(horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(_scale.x), _scale.y, _scale.z);
        }
    }

    // 接地したら再び２段階ジャンプができる
    void OnTriggerEnter2D(Collider2D col)
    {
        // ジャンプできるものの配列の中と比較し、一致したらジャンプできる
        for (var i = 0; i < _jumpables.Length; i++)
        {
            // 「col.gameObject」だとクローンが消滅したとき「Missing」扱いなる
            // 「対象A.Contains(対象B)」：対象Aに対象Bの文字列が含まれていたら
            // --> クローンやプレハブは名前が変わるため
            if (col.gameObject.name.Contains(_jumpables[i]))                  
            {
                _jumpCount = 0;
                _isJump = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // 読み取り 
        PlayerHp playerHp = GetComponent<PlayerHp>(); 
        if (coll.gameObject.tag == "Bullet" && playerHp.PlayerCurrentHp > 0)
        {
            ResetAnim(); 
            _anim.Play("Hit"); 
        }

    }

    void ResetAnim()
    {
        _anim.SetBool("isWalk", false); 
        _anim.SetBool("isRun", false);  
    }

    enum PlayerState
    {
        /// <summary>通常</summary>
        Normal,
        /// <summary>毒 ライフが減る</summary>
        Poisoned,
        /// <summary>麻痺 移動が遅くなる</summary>
        Paralyzed,
        /// <summary>死</summary>
        Dead,
    }
}
