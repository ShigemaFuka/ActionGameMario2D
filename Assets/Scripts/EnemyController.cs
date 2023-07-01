using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
/// �G�L�����̍s������ 
/// �u�@Weapon�@�v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂�
/// </summary>
public class EnemyController : MonoBehaviour
{
    // �Q��
    [SerializeField, Tooltip("�uPlayer�v�X�N���v�g�́u_attackValue�v")] int _damageValue;
    [SerializeField, Tooltip("�v���C���[Name������")] string _playerName;
    [SerializeField, Tooltip("�uPlayer�v�X�N���v�g")] PlayerMove _scPlayer;
    // HP
    //[SerializeField, Tooltip("�G�l�~�[��HP�̏����ݒ�")] int _enemyHpMax;
    [SerializeField, Header("���͕s�v")] int _enemyHp;
    
    // ���̑�
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    Collider2D _col2d;


    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDate characterDate;
    [SerializeField, Tooltip("�G�̋�����I��")]CharacterType characterType; 

    // Start is called before the first frame update
    void Start()
    {
        // �uPlayer�v���擾
        GameObject _player = GameObject.Find(_playerName);
        // �uPlayer�v�X�N���v�g���擾 �� �U���l���~����
        _scPlayer = _player.GetComponent<PlayerMove>();

        // �G�l�~�[��HP�̏�����
        if (characterType == CharacterType.Nomal)
            _enemyHp = characterDate.achievementList[1].Maxhp;
        else if(characterType == CharacterType.Midiam)
            _enemyHp = characterDate.achievementList[2].Maxhp;
        else if(characterType == CharacterType.Hard)
            _enemyHp = characterDate.achievementList[3].Maxhp;

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
        if (coll.gameObject.tag == "Weapon") 
        {
            AttackController scr = coll.transform.root.gameObject.GetComponent<AttackController>();
            scr.Attack();
            _damageValue = scr._attackValue;

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
