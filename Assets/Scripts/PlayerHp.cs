using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    // HP
    [SerializeField, Tooltip("�v���C���[��HP�̏����ݒ�")] int _playerHpMax;
    [SerializeField, Header("���͕s�v")] int _playerHp;
    // �Q��
    [SerializeField, Tooltip("�uEnemyHp�v�X�N���v�g")] EnemyHp _enemyHpScript;
    [SerializeField, Tooltip("�uEnemyHp�v�X�N���v�g�́u_attackValue�v")] int _damageValue;

    // ���̑�


    void Start()
    {
        // �uEnemy�v���擾
        GameObject _enemy = GameObject.Find("Enemy");
        // �uEnemyHp�v�X�N���v�g���擾 �� �U���l���~����
        _enemyHpScript = _enemy.GetComponent<EnemyHp>();

        // �v���C���[��HP�̏�����
        _playerHp = _playerHpMax;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")        
        {
            // �Q�Ƃ������̂���
            _damageValue = _enemyHpScript._attackValue;
            // HP���炵�Ă���
            _playerHp = _playerHp - _damageValue;
            

            if (_playerHp < _damageValue)
            {
                Destroy(gameObject);
            }
            Debug.Log(_playerHp);
            //Debug.Log("�e�ۂɐڐG");

        }
    }
}