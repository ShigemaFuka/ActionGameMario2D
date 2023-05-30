using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    [SerializeField, Tooltip("ジャンプ時の計算で使う")] float _jumpPower;
    [Tooltip("移動速度の計算で使う")] float _movePower;
    [SerializeField, Tooltip("歩きの速さ")] float _movePowerDef = 30f;
    [SerializeField, Tooltip("走りの速さ")] float _movePowerUp;
    [Tooltip("水平方向の入力値")] float _h;
    [Header("5以上を推奨")] [SerializeField, Tooltip("「_attackValue」を決めるための値")] int _atVa;
    [Header("これに値入力禁止")] [Tooltip("敵に使う攻撃値")] public int _attackValue;
    [Tooltip("左右反転")]  Vector3 _scale;
    [SerializeField, Tooltip("ジャンプできるかの接地判定")] bool _isJump;
    [SerializeField, Tooltip("ジャンプできるかのカウント")] int _jumpCount;
    [SerializeField, Tooltip("ジャンプできて良いオブジェクトの名")] string[] _jumpables;
    [SerializeField, Tooltip("スタートポジション")] GameObject _startPosition;



    void Start()
    {
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

        // ジャンプ
        if (_isJump && _jumpCount < 2)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _jumpCount++;
            }
        }
        else if(_jumpCount >= 2)
        {
            _isJump = false;
        }
    }

    private void FixedUpdate()
    {
        // 力を加えるのは FixedUpdate で行う
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
    }

    void MoveControl()
    {
        // 方向指示があるとき
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.B))
            {
                // 走る
                _movePower = _movePowerUp;
                _anim.SetBool("isRun", true);
                _anim.SetBool("isWalk", false);
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

        // 攻撃        
        if (Input.GetKeyDown(KeyCode.N))
        {
            Attack();
            _anim.SetTrigger("isAttack_3");
        }
    }

    void Attack()
    {
        // +-2で攻撃
        _attackValue = Random.Range(_atVa - 2, _atVa + 3);
        Debug.Log("攻撃値 :  " + _attackValue);
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
                //Debug.Log("Jumpable");
            }
        }
    }
}
