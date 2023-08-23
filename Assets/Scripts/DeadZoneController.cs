using UnityEngine;

/// <summary>
/// �v���C���[���f�b�h�]�[���ɐڐG������Q�[���I�[�o�[�֐����Ăяo��
/// �G�l�~�[���ڐG������ADestroy����
/// ������A�^�b�`���A��I�u�W�F�N�g�Ƀ{�b�N�X�R���C�_��t���Ĕ���
/// </summary>
public class DeadZoneController : MonoBehaviour
{
    GameManager _gameManager = default;
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _gameManager.GameOver();
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject); 
        }
    }
}
