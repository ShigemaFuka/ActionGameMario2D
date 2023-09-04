using UnityEngine;
/// <summary>
/// 左右移動する敵キャラの行動制御、 
/// 移動する向きに応じて反転もする
/// </summary>
public class EnemyMove : MonoBehaviour
{
    [SerializeField, Tooltip("速度")] float _moveSpeed = 0.5f;
    Vector3 _scale = default;
    [SerializeField] LayerMask _wall;
    [SerializeField] LayerMask _ground;
    Rigidbody2D _rb = default;
    Vector2 _moveDirection = new Vector2(-1, 0);
    [SerializeField] Vector2 _lineForWall = new Vector2(-1, -0.5f);
    [SerializeField] Vector2 _lineForGround = new Vector2(-1, -1f);
    [Tooltip("地面に接したら真")] bool _canMove = false; 
    GameObject _chara = null;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // 親子関係あたりを変更したときは注意  
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
        // 壁の判定 
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit_wall = Physics2D.Linecast(start, start + _lineForWall, _wall);
        // 床の判定 
        Debug.DrawLine(start, start + _lineForGround);
        RaycastHit2D hit_ground = Physics2D.Linecast(start, start + _lineForGround, _ground);
        Vector2 velo = Vector2.zero;    // velo は速度ベクトル 

        if (hit_wall || !hit_ground)
        {
            // 反転 
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
    /// 出現時に空中にいるとき、左右にぶれるため、
    /// 地面に一度触れるまでは、Move()を実行させない
    /// </summary>
    void CanMoveOnce()
    {
        // 地面に接したらラインを引かない  
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
