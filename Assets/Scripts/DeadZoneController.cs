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
           
    //void OnCollisionEnter2D(Collision2D coll)
    //{
        //if (coll.gameObject.CompareTag("Player"))
        //{
        //    _gameManager.GameOver();
        //}
        //else if (coll.gameObject.CompareTag("Enemy"))
        //{
        //    _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
        //    _enemyGenerator.CharaCount--;
        //}
    //}
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(!_gameManager) _gameManager = FindAnyObjectByType<GameManager>();
        if (coll.gameObject.CompareTag("Player"))
        {
            _gameManager.GameOver();
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
            _enemyGenerator.CharaCount--;
        }
    }
}
