using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�L�����̍s������ 
/// �u�@Weapon�@�v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂�
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager; 
    int _damageValue; 
    // HP
    [SerializeField, Header("���͕s�v")] int _enemyHp;
    
    // ���̑�
    SpriteRenderer _spriteRenderer;
    Animator _anim;
    Collider2D _col2d;

    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDates _characterDate;
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _effectPrefab;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); 

        // �G�l�~�[��HP�̏�����
        if (_characterDate)
            _enemyHp = _characterDate.Maxhp;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Color.white;
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
            // �U���l 
            AttackValueController scr = coll.transform.root.gameObject.GetComponent<AttackValueController>();
            scr.Attack();
            _damageValue = scr._attackValue;

            // HP���炵�Ă���
            _enemyHp = _enemyHp - _damageValue;  
            _spriteRenderer.color = Color.red;

            if (_enemyHp < _damageValue)
            {
                // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
                if (_effectPrefab)
                {
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
                }
                // �X�R�A���Z 
                _gameManager.AddScore(_characterDate.Score);
                // �L�������Z  
                _gameManager.KillCount += 1;
                Debug.Log(_gameManager.KillCount); 
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
