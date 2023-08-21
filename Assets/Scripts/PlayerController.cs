using UnityEngine; 

/// <summary>
/// �v���C���[�̈ړ��A�W�����v�A�����ʒu�𐧌� 
/// </summary>
public class PlayerController : MonoBehaviour
{
    Animator _anim = default;
    Rigidbody2D _rb = default;
    [SerializeField, Tooltip("�W�����v���̌v�Z�Ŏg��")] float _jumpPower = 0;
    [Tooltip("�ړ����x�̌v�Z�Ŏg��")] float _movePower = 0;
    [SerializeField, Tooltip("�����̑���")] float _movePowerDef = 30f;
    [SerializeField, Tooltip("����̑���")] float _movePowerUp = 0;
    [Tooltip("���������̓��͒l")] float _h = 0;
    [Tooltip("���E���]")]  Vector3 _scale = default;
    [SerializeField, Tooltip("�W�����v�ł��邩�̐ڒn����")] bool _isJump = false;
    [SerializeField, Tooltip("�W�����v�ł��邩�̃J�E���g")] int _jumpCount = 0;
    [SerializeField, Tooltip("�W�����v�ł��ėǂ��I�u�W�F�N�g�̖�")] string[] _jumpables = null;
    [Tooltip("�X�^�[�g�|�W�V����")] GameObject _startPosition = default;
    [Tooltip("GameManager")] GameManager _gameManager = default; 
    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
        _movePowerUp = _movePowerDef * 1.5f;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = GameObject.Find("StartPos");
        this.transform.position = _startPosition.transform.position;
    }

    void Update()
    {
        //Debug.Log(this.gameObject.transform.position.y);
        if (this.gameObject.transform.position.y <= -13)
        {
            _gameManager.GameOver();
        }
        _h = Input.GetAxis("Horizontal"); 
        FlipX(_h);
        Jump();
        MoveControl(); 
    }
    void FixedUpdate()
    {
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
    }
    /// <summary> 2�i�K�W�����v </summary>
    void Jump()
    {
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
    /// <summary> �����E����E�����̃A�j���[�V���� </summary>
    void MoveControl()
    {
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
            ResetAnim(); 
        }
    }

    ///<summary>���E���]</summary>
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
            _anim.Play("Hit"); 
        }
    }

    void ResetAnim()
    {
        _anim.SetBool("isWalk", false); 
        _anim.SetBool("isRun", false);  
    }
}
