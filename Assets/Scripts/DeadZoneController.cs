using UnityEngine;

/// <summary>
/// �v���C���[���f�b�h�]�[���ɐڐG������Q�[���I�[�o�[�֐����Ăяo��
/// �G�l�~�[���ڐG������ACollet(SetActive���U��)����
/// ������A�^�b�`���A��I�u�W�F�N�g�Ƀ{�b�N�X�R���C�_��t���Ĕ���
/// </summary>
public class DeadZoneController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager = default;
    [SerializeField] EnemyGenerator _enemyGenerator = default;
    [SerializeField, Tooltip("�v���C���[�ڐG����SE")] AudioSource _audioSource = default;

    void OnEnable()
    {
        _audioSource.Stop();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(!_gameManager) _gameManager = FindAnyObjectByType<GameManager>();
        if (coll.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            _gameManager.GameOver();
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
        }
    }
}