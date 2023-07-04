using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    [SerializeField, Tooltip("�W�����v���̌v�Z�Ŏg��")] float _jumpPower;
    [Tooltip("�ړ����x�̌v�Z�Ŏg��")] float _movePower;
    [SerializeField, Tooltip("�����̑���")] float _movePowerDef = 30f;
    [SerializeField, Tooltip("����̑���")] float _movePowerUp;
    [Tooltip("���������̓��͒l")] float _h;
    [Header("5�ȏ�𐄏�")] [SerializeField, Tooltip("�u_attackValue�v�����߂邽�߂̒l")] int _atVa;
    [Header("����ɒl���͋֎~")] [Tooltip("�G�Ɏg���U���l")] public int _attackValue;
    [Tooltip("���E���]")]  Vector3 _scale;
    [SerializeField, Tooltip("�W�����v�ł��邩�̐ڒn����")] bool _isJump;
    [SerializeField, Tooltip("�W�����v�ł��邩�̃J�E���g")] int _jumpCount;
    [SerializeField, Tooltip("�W�����v�ł��ėǂ��I�u�W�F�N�g�̖�")] string[] _jumpables;
    [SerializeField, Tooltip("�X�^�[�g�|�W�V����")] GameObject _startPosition;  

    /// <summary>�v���C���[�̏�Ԃ�\��</summary>
    [SerializeField] PlayerState _state = PlayerState.Normal;

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

        // �W�����v
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
        // �͂�������̂� FixedUpdate �ōs��
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
    }

    void MoveControl()
    {
        // �U��        
        if (Input.GetKeyDown(KeyCode.N))
        {
            _anim.SetTrigger("isAttack"); 
        }
        // �����w��������Ƃ� 
        if(_h != 0)
        {
            if (Input.GetKey(KeyCode.B))
            {
                // ����
                _movePower = _movePowerUp;
                _anim.SetBool("isRun", true);
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
    }

    //���E���]
    /// <param name="horizontal">���������̓��͒l</param>
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

    // �ڒn������ĂтQ�i�K�W�����v���ł���
    void OnTriggerEnter2D(Collider2D col)
    {
        // �W�����v�ł�����̂̔z��̒��Ɣ�r���A��v������W�����v�ł���
        for (var i = 0; i < _jumpables.Length; i++)
        {
            // �ucol.gameObject�v���ƃN���[�������ł����Ƃ��uMissing�v�����Ȃ�
            // �u�Ώ�A.Contains(�Ώ�B)�v�F�Ώ�A�ɑΏ�B�̕����񂪊܂܂�Ă�����
            // --> �N���[����v���n�u�͖��O���ς�邽��
            if (col.gameObject.name.Contains(_jumpables[i]))                  
            {
                _jumpCount = 0;
                _isJump = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // �ǂݎ�� 
        PlayerHp playerHp = GetComponent<PlayerHp>(); 
        if (coll.gameObject.tag == "Bullet" && playerHp.PlayerCurrentHp > 0)
        {
            ResetAnim(); 
            _anim.SetBool("isHit", true); 
        }

    }

    void ResetAnim()
    {
        _anim.SetBool("isWalk", false);
        _anim.SetBool("isRun", false);
        _anim.SetBool("isHit", false);  
    }

    enum PlayerState
    {
        /// <summary>�ʏ�</summary>
        Normal,
        /// <summary>�� ���C�t������</summary>
        Poisoned,
        /// <summary>��� �ړ����x���Ȃ�</summary>
        Paralyzed,
        /// <summary>��</summary>
        Dead,
    }
}
