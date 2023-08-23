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
    Vector2 _moveDirection = new Vector2(1, 0);
    [SerializeField] Vector2 _lineForWall = new Vector2(1, -0.5f);
    [SerializeField] Vector2 _lineForGround = new Vector2(1, -1f);
    [Tooltip("�n�ʂɐڂ�����^")] bool _canMove = false; 
    GameObject _chara = null;



    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_scale = this.transform.localScale;
        _chara = GameObject.Find(this.gameObject.name + "/Chara");
        _scale = _chara.transform.localScale;
        _scale.x = _chara.transform.localScale.x;
        // ��x�����A���������E�����ɏC��
        _scale.x = -_scale.x;
        //transform.localScale = _scale;
        _chara.transform.localScale = _scale;
        _canMove = false;
    }


    void Update()
    {
        CanMoveOnce(); 
        if (_canMove)
            Move();
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
            _moveDirection = -_moveDirection;
            _lineForWall.x = -_lineForWall.x;
            _lineForGround.x = -_lineForGround.x; 
            _scale.x = -_scale.x;
        }
        //transform.localScale = _scale; 
        _chara.transform.localScale = _scale; 
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
            Debug.DrawLine(start, start + new Vector2(0, -2f));
            RaycastHit2D hit_ground = Physics2D.Linecast(start, start + new Vector2(0, -2f), _ground);
            if (hit_ground)
            {
                _canMove = true;
            }
        }
    }
}
