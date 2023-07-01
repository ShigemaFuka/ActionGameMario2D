using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[��HP�𐧌�
/// �u�@Bullet�@�v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂� 
/// </summary>
public class PlayerHp : MonoBehaviour
{
    // HP
    [SerializeField, Header("���͕s�v")] int _playerHp; 
    [SerializeField, Tooltip("�v���C���[���󂯂�_���[�W")] int _damageValue;

    // ���̑�
    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDate characterDate;
    [SerializeField, Tooltip("�^�C�v��I��")] CharacterType characterType;


    void Start()
    {
        // �v���C���[��HP�̏�����
        if (characterType == CharacterType.Player)
            _playerHp = characterDate.achievementList[0].Maxhp;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")        
        {
            AttackController scr = coll.gameObject.GetComponent<AttackController>(); 
            scr.Attack(); 
            _damageValue = scr._attackValue; 

            // HP���炵�Ă���
            _playerHp = _playerHp - _damageValue;
            
            if (_playerHp < _damageValue)
            {
                //Destroy(gameObject);

                Debug.LogWarning("�v���C���[���񂾂�"); 
            }
            Debug.Log(_playerHp);
        }
    }
}