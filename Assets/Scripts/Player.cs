using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary> 移動速度の計算で使う </summary>
    float _movePower;
    /// <summary> 歩きの速さ </summary>
    [SerializeField] float _movePowerDef = 30f;
    /// <summary> 走りの速さ </summary>
    float _movePowerUp;
    /// <summary> 水平方向の入力値 </summary>
    float _h;
    /// <summary> 敵に使う攻撃値 </summary>
    public int _attackValue;
    /// <summary> 「_attackValue」を決めるための値 </summary>
    [SerializeField] int _atVa;
    float scaleX;

    void Start()
    {
        _movePowerUp = _movePowerDef * 3f;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _h = Input.GetAxis("Horizontal");
        FlipX(_h);

        MoveControl();
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
        if(Input.GetKeyDown(KeyCode.N))
        {
            Attack();
            _anim.SetTrigger("isAttack_3");
        }
    }

    void Attack()
    {
        // +-2で攻撃
        _attackValue = Random.Range(_atVa - 2, _atVa + 3);
        //Debug.Log("攻撃値 :  " + _attackValue);
    }

    /// <param name="horizontal">水平方向の入力値</param>
    void FlipX(float horizontal)
    {
        // _scaleX = this.transform.localScale.x;

        /*
        if()
        {

        }
        */
    }
}
