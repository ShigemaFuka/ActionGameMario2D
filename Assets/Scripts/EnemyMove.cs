using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���E�ړ�����G�L�����̍s������A 
/// �ړ���������ɉ����Ĕ��]������
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [SerializeField, Tooltip("�͈�")] float _moveRange = 5.0f;
    [SerializeField, Tooltip("���x")] float _moveSpeed = 0.5f;
    Vector2 _startPos = default;
    Vector3 _scale = default;
    [SerializeField] LayerMask _tile;
    Rigidbody2D _rb = default;
    Vector2 _moveDirection = new Vector2(1, 0);
    [SerializeField] Vector2 _lineForWall = new Vector2(1, -0.5f);



    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _startPos = transform.position;
        _scale = this.transform.localScale;
        // ��x�����A���������E�����ɏC��
        _scale.x = -_scale.x;
        transform.localScale = _scale; 
    }


    void Update()
    {
        Move();
    }

    

    void Move()
    {
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForWall, _tile);
        Vector2 velo = Vector2.zero;    // velo �͑��x�x�N�g�� 

        if (hit.collider)
        {
            Debug.Log("Hit Wall");
            _moveDirection = -_moveDirection;
            _lineForWall.x = -_lineForWall.x;
            _scale.x = -_scale.x;
        }

        transform.localScale = _scale; 
        velo = _moveDirection.normalized * _moveSpeed;
        velo.y = _rb.velocity.y;
        _rb.velocity = velo;
    }
}
