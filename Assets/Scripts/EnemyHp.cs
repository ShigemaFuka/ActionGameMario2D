using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class EnemyHp : MonoBehaviour
{
    // �Q��
    [SerializeField, Tooltip("�uPlayer�v�X�N���v�g�́u_attackValue�v")] int _damageValue;
    [SerializeField, Tooltip("�v���C���[Name������")] string _playerName;
    [SerializeField, Tooltip("�uPlayer�v�X�N���v�g")] PlayerMove _scPlayer;
    // HP
    [SerializeField, Tooltip("�G�l�~�[��HP�̏����ݒ�")] int _enemyHpMax;
    [SerializeField, Header("���͕s�v")] int _enemyHp;
    // �U���l
    [Header("5�ȏ�𐄏�")][SerializeField, Tooltip("�u_attackValue�v�����߂邽�߂̒l")] int _atVa;
    //[Header("���͕s�v")][Tooltip("�v���C���[����Ɏg���U���l")] public int _attackValue;
    // ���̑�
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    Collider2D _col2d;
    



    // Start is called before the first frame update
    void Start()
    {
        // �uPlayer�v���擾
        GameObject _player = GameObject.Find(_playerName);
        // �uPlayer�v�X�N���v�g���擾 �� �U���l���~����
        _scPlayer = _player.GetComponent<PlayerMove>();
        // �G�l�~�[��HP�̏�����
        _enemyHp = _enemyHpMax;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
        _anim = _player.GetComponent<Animator>();
        _col2d = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon")        //&& Input.GetKeyDown(KeyCode.N))
        {
            _damageValue = _scPlayer._attackValue;
            // HP���炵�Ă���
            _enemyHp = _enemyHp - _damageValue;  
            _spriteRenderer.color = Color.red;

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
            _spriteRenderer.color = Color.white;
        }
    }
}
