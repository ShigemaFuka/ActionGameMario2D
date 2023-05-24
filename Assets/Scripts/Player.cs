using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary> �ړ����x�̌v�Z�Ŏg�� </summary>
    float _movePower;
    /// <summary> �����̑��� </summary>
    [SerializeField] float _movePowerDef = 30f;
    /// <summary> ����̑��� </summary>
    float _movePowerUp;
    /// <summary> ���������̓��͒l </summary>
    float _h;
    /// <summary> �G�Ɏg���U���l </summary>
    public int _attackValue;
    /// <summary> �u_attackValue�v�����߂邽�߂̒l </summary>
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
        // �͂�������̂� FixedUpdate �ōs��
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
    }

    void MoveControl()
    {
        // �����w��������Ƃ�
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.B))
            {
                // ����
                _movePower = _movePowerUp;
                _anim.SetBool("isRun", true);
                _anim.SetBool("isWalk", false);
            }
            else
            {
                // ����
                _movePower = _movePowerDef;
                _anim.SetBool("isRun", false);
                _anim.SetBool("isWalk", true);
            }
        }
        // �����w�����Ȃ��Ƃ�
        else
        {
            _anim.SetBool("isWalk", false);
            _anim.SetBool("isRun", false);
        }
        // �U��        
        if(Input.GetKeyDown(KeyCode.N))
        {
            Attack();
            _anim.SetTrigger("isAttack_3");
        }
    }

    void Attack()
    {
        // +-2�ōU��
        _attackValue = Random.Range(_atVa - 2, _atVa + 3);
        //Debug.Log("�U���l :  " + _attackValue);
    }

    /// <param name="horizontal">���������̓��͒l</param>
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
