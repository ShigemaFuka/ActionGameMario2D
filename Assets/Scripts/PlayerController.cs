using UnityEngine; 

/// <summary>
/// �v���C���[�̈ړ��A�����ʒu�A���]�A�������̃A�j���[�V����
/// �𐧌� 
/// </summary>
public class PlayerController : MonoBehaviour
{
    Animator _anim = default;
    Rigidbody2D _rb = default;
    [Tooltip("�ړ����x�̌v�Z�Ŏg��")] float _movePower = 0;
    [SerializeField, Tooltip("�����̑���")] float _movePowerDef = 30f;
    [SerializeField, Tooltip("����̑���")] float _movePowerUp = 0;
    [Tooltip("���������̓��͒l")] float _h = 0;
    [Tooltip("���E���]")]  Vector3 _scale = default;
    [Tooltip("�X�^�[�g�|�W�V����")] GameObject _startPosition = default;
    [Tooltip("�v���C���[��HP�Ǘ�������X�N���v�g")] PlayerHp playerHp = null;
    
    void Start()
    {
        _movePowerUp = _movePowerDef * 1.5f;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = GameObject.Find("StartPos");
        this.transform.position = _startPosition.transform.position;
        playerHp = GetComponent<PlayerHp>();
    }

    void Update()
    {
        _h = Input.GetAxis("Horizontal"); 
        FlipX(_h);
        MoveControl(); 
    }
    void FixedUpdate()
    {
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
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

    void OnCollisionEnter2D(Collision2D coll)
    {
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
