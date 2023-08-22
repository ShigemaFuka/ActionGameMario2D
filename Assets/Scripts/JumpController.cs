using UnityEngine;

/// <summary>
/// weaponのコライダに反応して、壁ジャンプしてしまうのを防ぐために、
/// ジャンプのみを管理する
/// プレイヤー直下に空オブジェクトを作り、コライダを付け、それをジャンプ判定用にする 
/// </summary>
public class JumpController : MonoBehaviour
{
    Rigidbody2D _rb = default;
    [SerializeField, Tooltip("ジャンプ時の計算で使う")] float _jumpPower = 0;
    [SerializeField, Tooltip("ジャンプできるかの接地判定")] bool _isJump = false;
    [SerializeField, Tooltip("ジャンプできるかのカウント")] int _jumpCount = 0;
    [SerializeField, Tooltip("ジャンプできて良いオブジェクトの名")] string[] _jumpables = null;

    void Start()
    {
        _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Jump();
    }

    /// <summary> 2段階ジャンプ </summary>
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

    // 接地したら再び２段階ジャンプができる
    void OnTriggerEnter2D(Collider2D col)
    {
        // ジャンプできるものの配列の中と比較し、一致したらジャンプできる
        for (var i = 0; i < _jumpables.Length; i++)
        {
            // 「col.gameObject」だとクローンが消滅したとき「Missing」扱いなる
            // 「対象A.Contains(対象B)」：対象Aに対象Bの文字列が含まれていたら
            // --> クローンやプレハブは名前が変わるため(今作はtagを使用頻度を減らす)
            if (col.gameObject.name.Contains(_jumpables[i]))
            {
                _jumpCount = 0;
                _isJump = true;
            }
        }
    }
}
