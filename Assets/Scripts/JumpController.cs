using UnityEngine;

/// <summary>
/// weapon�̃R���C�_�ɔ������āA�ǃW�����v���Ă��܂��̂�h�����߂ɁA
/// �W�����v�݂̂��Ǘ�����
/// �v���C���[�����ɋ�I�u�W�F�N�g�����A�R���C�_��t���A������W�����v����p�ɂ��� 
/// </summary>
public class JumpController : MonoBehaviour
{
    Rigidbody2D _rb = default;
    [SerializeField, Tooltip("�W�����v���̌v�Z�Ŏg��")] float _jumpPower = 0;
    [SerializeField, Tooltip("�W�����v�ł��邩�̐ڒn����")] bool _isJump = false;
    [SerializeField, Tooltip("�W�����v�ł��邩�̃J�E���g")] int _jumpCount = 0;
    [SerializeField, Tooltip("�W�����v�ł��ėǂ��I�u�W�F�N�g�̖�")] string[] _jumpables = null;

    void Start()
    {
        _rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Jump();
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

    // �ڒn������ĂтQ�i�K�W�����v���ł���
    void OnTriggerEnter2D(Collider2D col)
    {
        // �W�����v�ł�����̂̔z��̒��Ɣ�r���A��v������W�����v�ł���
        for (var i = 0; i < _jumpables.Length; i++)
        {
            // �ucol.gameObject�v���ƃN���[�������ł����Ƃ��uMissing�v�����Ȃ�
            // �u�Ώ�A.Contains(�Ώ�B)�v�F�Ώ�A�ɑΏ�B�̕����񂪊܂܂�Ă�����
            // --> �N���[����v���n�u�͖��O���ς�邽��(�����tag���g�p�p�x�����炷)
            if (col.gameObject.name.Contains(_jumpables[i]))
            {
                _jumpCount = 0;
                _isJump = true;
            }
        }
    }
}
