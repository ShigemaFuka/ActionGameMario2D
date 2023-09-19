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

    void Start()
    {
        _playerHp = FindAnyObjectByType<PlayerHp>(); 
    }

    void Update()
    {
        if(_playerHp.PlayerCurrentHp <= 0 && !_isDeath)
        {
            Instantiate(_prefab, _player.transform.position, Quaternion.identity, gameObject.transform);
            _isDeath = true; 
        }
    }
}
