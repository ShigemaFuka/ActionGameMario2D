using UnityEngine; 

/// <summary>
/// プレイヤーの移動、初期位置、反転、いくつかのアニメーション
/// を制御 
/// </summary>
public class PlayerController : MonoBehaviour
{
    Animator _anim = default;
    Rigidbody2D _rb = default;
    [Tooltip("移動速度の計算で使う")] float _movePower = 0;
    [SerializeField, Tooltip("歩きの速さ")] float _movePowerDef = 30f;
    [SerializeField, Tooltip("走りの速さ")] float _movePowerUp = 0;
    [Tooltip("水平方向の入力値")] float _h = 0;
    [Tooltip("左右反転")]  Vector3 _scale = default;
    [Tooltip("スタートポジション")] GameObject _startPosition = default;
    [Tooltip("プレイヤーのHP管理をするスクリプト")] PlayerHp playerHp = null;
    
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

    /// <summary> 歩く・走る・それらのアニメーション </summary>
    void MoveControl()
    {
        // 方向指示があるとき 
        if(_h != 0)
        {
            if (Input.GetKey(KeyCode.B))
            {
                // 走る
                _movePower = _movePowerUp;
                _anim.SetBool("isRun", true);
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
            ResetAnim(); 
        }
    }

    ///<summary>左右反転</summary>
    /// <param name="horizontal">水平方向の入力値</param>
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
