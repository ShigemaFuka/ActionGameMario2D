using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using static GameManager;
using static PlayerController;

/// <summary>
/// �v���C���[��HP�𐧌�
/// �u Bullet �v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂� 
/// Death�A�j���[�V��������GameOver�֐����Ăяo���Ă��� 
/// </summary>
public class PlayerHp : MonoBehaviour
{

    [SerializeField] GameManager _gameManager = default; 

    // HP
    [Tooltip("���݂̃v���C���[��HP")] int _playerCurrentHp;
    public int PlayerCurrentHp { get { return _playerCurrentHp; } }

    [SerializeField, Tooltip("�v���C���[���󂯂�_���[�W")] int _damageValue = 0;

    // ���̑�
    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDates characterDate = null; 
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _effectPrefab = null; 
    [SerializeField, Tooltip("Hp�̃X���C�_�[")] Slider _slider = null; 


    void Start()
    {
        // �v���C���[��HP�̏�����
        if (characterDate)
        {
            _playerCurrentHp = characterDate.Maxhp;
            _slider = GameObject.Find("HpSlider").GetComponent<Slider>(); 
            _slider.maxValue = characterDate.Maxhp; 
            _slider.value = characterDate.Maxhp; 
        }
        _gameManager = FindAnyObjectByType<GameManager>();
        //_gameManager._state = PlayerState.Alive; 
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
            _slider.value = _playerCurrentHp;

            if (_playerCurrentHp <= 0) 
            {
                // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐����� 
                if (_effectPrefab)
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
                // �v���C���[���������� 
                PlayerHide(); 
                Debug.LogWarning("�v���C���[���񂾂�"); 
            }
            Debug.Log(_playerCurrentHp); 
        }
    }

    /// <summary>
    /// ���S����Death�v���C���[�𐶐����A����Ƀv���C���[���A�N�e�B�u�ɂ��� 
    /// SetActive(true)�ɂ��Ȃ��Ă��V�[�����[�h���ꂽ�Ƃ��ɕ������� 
    /// </summary>
    void PlayerHide()
    {
        this.gameObject.SetActive(false); 
    }
}