using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class EnemyHp : MonoBehaviour
{
    /// <summary> �uPlayer�v�X�N���v�g�́u_attackValue�v </summary>
    [SerializeField] int _damageValue;
    /// <summary> �uPlayer�v�X�N���v�g </summary>
    [SerializeField] Player _scPlayer;
    /// <summary> �G�l�~�[��HP�̏����ݒ� </summary>
    [SerializeField] int _enemyHpMax;
    [SerializeField] int _enemyHp;
    SpriteRenderer _spre;
    Animator _anim;
    Collider2D _col2d;



    // Start is called before the first frame update
    void Start()
    {
        // �uMyRogue_01�v���擾
        GameObject _player = GameObject.Find("MyRogue_01");
        // �uPlayer�v�X�N���v�g���擾
        _scPlayer = _player.GetComponent<Player>();
        // �G�l�~�[��HP�̏�����
        _enemyHp = _enemyHpMax;
        _spre = GetComponent<SpriteRenderer>();
        _spre.color = Color.white;
        _anim = _player.GetComponent<Animator>();
        _col2d = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(_anim.GetBool("isAttack_3") == false)
        {
            _col2d.enabled = false;
        }
        else if(_anim.GetBool("isAttack_3") == true)
        {
            _col2d.enabled = true;
        }
        */
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // �uInput.GetKeyDown(KeyCode.N)�v���Ȃ��ƃL�[���͂Ȃ��Ă��_���[�W����,,,,,,,,
        // ���Ǔ����Ƌ@�\���Ȃ��A�A�A�A�A
        if (coll.gameObject.tag == "Weapon") //&& Input.GetKeyDown(KeyCode.N))
        {
            _damageValue = _scPlayer._attackValue;
            // HP���炵�Ă���
            _enemyHp = _enemyHp - _damageValue;  
            _spre.color = Color.red;

            if (_enemyHp < _damageValue)
            {
                Destroy(gameObject);
            }
            Debug.Log(_enemyHp);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Weapon")
        {
            _spre.color = Color.white;
        }
    }
}
