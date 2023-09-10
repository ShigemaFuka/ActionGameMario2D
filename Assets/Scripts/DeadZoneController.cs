using UnityEngine;

/// <summary>
/// �v���C���[���f�b�h�]�[���ɐڐG������Q�[���I�[�o�[�֐����Ăяo��
/// �G�l�~�[���ڐG������ACollet(SetActive���U��)����
/// ������A�^�b�`���A��I�u�W�F�N�g�Ƀ{�b�N�X�R���C�_��t���Ĕ���
/// </summary>
public class DeadZoneController : MonoBehaviour
{
    GameManager _gameManager = default;
    [SerializeField] EnemyGenerator _enemyGenerator = default;
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _gameManager.GameOver();
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
            _enemyGenerator.CharaCount--;
        }
    }
}
