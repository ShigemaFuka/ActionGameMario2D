using UnityEngine;
/// <summary>
/// ���E�ړ�����G�L�����̍s������A 
/// �ړ���������ɉ����Ĕ��]������
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [SerializeField, Tooltip("���x")] float _moveSpeed = 0.5f;
    Vector3 _scale = default;
    [SerializeField] LayerMask _wall;
    [SerializeField] LayerMask _ground;
    Rigidbody2D _rb = default;
    Vector2 _moveDirection = new Vector2(-1, 0);
    [SerializeField] Vector2 _lineForWall = new Vector2(-1, -0.5f);
    [SerializeField] Vector2 _lineForGround = new Vector2(-1, -1f);
    [Tooltip("�n�ʂɐڂ�����^")] bool _canMove = false; 
    GameObject _chara = null;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // �e�q�֌W�������ύX�����Ƃ��͒���  
        _chara = transform.GetChild(2).gameObject;
        _scale = _chara.transform.localScale;
        _canMove = false;
        _moveDirection = new Vector2(-1, 0);
    }
    void Update()
    {
        CanMoveOnce(); 
        if (_canMove) Move();
    }
    void Move()
    {
        Vector2 start = this.transform.position;
        // �ǂ̔��� 
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit_wall = Physics2D.Linecast(start, start + _lineForWall, _wall);
        // ���̔��� 
        Debug.DrawLine(start, start + _lineForGround);
        RaycastHit2D hit_ground = Physics2D.Linecast(start, start + _lineForGround, _ground);
        Vector2 velo = Vector2.zero;    // velo �͑��x�x�N�g�� 

        if (hit_wall || !hit_ground)
        {
            // ���] 
            _scale.x = -_scale.x;
            _chara.transform.localScale = _scale;
            _moveDirection = -_moveDirection;
            _lineForWall.x = -_lineForWall.x;
            _lineForGround.x = -_lineForGround.x; 
        }
        velo = _moveDirection.normalized * _moveSpeed;
        velo.y = _rb.velocity.y;
        _rb.velocity = velo;
    }

    /// <summary>
    /// �o�����ɋ󒆂ɂ���Ƃ��A���E�ɂԂ�邽�߁A
    /// �n�ʂɈ�x�G���܂ł́AMove()�����s�����Ȃ�
    /// </summary>
    void CanMoveOnce()
    {
        // �n�ʂɐڂ����烉�C���������Ȃ�  
        if (!_canMove)
        {
            Vector2 start = this.transform.position;
            Debug.DrawLine(start, start + new Vector2(0, -3f));
            RaycastHit2D hit_ground = Physics2D.Linecast(start, start + new Vector2(0, -3f), _ground);
            RaycastHit2D hit_wall = Physics2D.Linecast(start, start + new Vector2(0, -3f), _wall);
            if (hit_ground || hit_wall)
            {
                _canMove = true;
            }
        }
    }
    void OnBecameVisible()
    {
        _canMove = false; 
    }
}
