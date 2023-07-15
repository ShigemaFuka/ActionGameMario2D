using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��̃I�u�W�F�N�g�ɂ�����A�^�b�`���A
/// �v���C���[�̎��S�A�j���[�V�����𐶐�����
/// ��PlayerDeath�X�N���v�g����Destroy�֐������Ă��� 
/// </summary>
public class InstantiatePlayerDeathAnim : MonoBehaviour
{
    PlayerHp _playerHp;
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _player;
    bool _isDeath = false; 

    // Start is called before the first frame update
    void Start()
    {
        _playerHp = FindAnyObjectByType<PlayerHp>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerHp.PlayerCurrentHp <= 0 && !_isDeath)
        {
            Instantiate(_prefab, _player.transform.position, Quaternion.identity, gameObject.transform);
            _isDeath = true; 
        }
    }
}
