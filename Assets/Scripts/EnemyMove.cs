using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 左右移動する敵キャラの行動制御、 
/// 移動する向きに応じて反転もする
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [SerializeField, Tooltip("範囲")] float _moveRange = 5.0f;
    [SerializeField, Tooltip("速度")] float _moveSpeed = 0.5f;
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
        // 一度だけ、左向きを右向きに修正
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
        Vector2 velo = Vector2.zero;    // velo は速度ベクトル 

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
