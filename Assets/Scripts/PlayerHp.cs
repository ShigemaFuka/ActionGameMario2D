using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[��HP�𐧌�
/// �u�@Bullet�@�v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂� 
/// </summary>
public class PlayerHp : MonoBehaviour
{

    [SerializeField] GameManager _gameManager = default; 

    // HP
    [Tooltip("���݂̃v���C���[��HP")] int _playerCurrentHp;
    public int PlayerCurrentHp { get { return _playerCurrentHp; } }

    [SerializeField, Tooltip("�v���C���[���󂯂�_���[�W")] int _damageValue;

    // ���̑�
    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDates characterDate; 
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _effectPrefab;


    void Start()
    {
        // �v���C���[��HP�̏�����
        if (characterDate)
            _playerCurrentHp = characterDate.Maxhp;
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")        
        {
            AttackValueController scr = coll.gameObject.GetComponent<AttackValueController>(); 
            scr.Attack(); 
            _damageValue = scr._attackValue; 

            // HP���炵�Ă��� 
            _playerCurrentHp = _playerCurrentHp - _damageValue;
            // �c��HP�����U���g�p�ɋL�^ 
            _gameManager.RemainingHp = _playerCurrentHp; 
            
            if (_playerCurrentHp <= 0)
            {
                // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
                if (_effectPrefab)
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation); 
                //_gameManager.GameOver();  
                Debug.LogWarning("�v���C���[���񂾂�"); 
            }
            Debug.Log(_playerCurrentHp); 
        }
    }
}