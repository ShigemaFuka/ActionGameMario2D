using System.Collections;
using UnityEngine;

/// <summary>
/// �e�ۂ̐���̊��N���X
/// ���Ԍo�߁A����ڐG�A�ՓˁA�����玩�g��j��
/// </summary>
public abstract class BaseBulletController_2 : MonoBehaviour
{
    // GM���v���n�u�����Ē����ꂵ�Ă��Ӗ��Ȃ��A�V�[����ɂȂ��ƃ_��(�C���X�^���X��) 
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("ScriptableObject�ȃL�����̃p�����[�^")] CharacterDates _characterDate = default;
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _crashEffectPrefab = default;
    [SerializeField] float _destroyTime = 5f;
    //�I�u�W�F�N�g�v�[���p�R���g���[���[�i�[�p�ϐ��錾
    ObjectPoolController _objectPool;

    void Start()
    {
        MoveBullet();
        //Destroy(gameObject, _destroyTime); 
        StartCoroutine(DestroyBullet());
        // �V�[���ɂP�����Ȃ����� 
        _gameManager = FindObjectOfType<GameManager>();
        //�I�u�W�F�N�g�v�[�����擾
        _objectPool = FindObjectOfType<ObjectPoolController>();
    }

    /// <summary>
    /// �v���C���[�ɍU�����ꂽ�玩�g��j�� 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Weapon"))
        {
            // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
            if (_crashEffectPrefab)
            {
                Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
            }
            _gameManager.AddScore(_characterDate.Score);
            //Destroy(gameObject);
            HideFromStage();
        }
    }

    /// <summary>
    /// ��������ɏՓ˂����玩�g��j������
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
        if (_crashEffectPrefab)
        {
            Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
        }
        // �폜
        //Destroy(gameObject);
        HideFromStage();
    }

    /// <summary>
    /// Start�֐��ŌĂ΂�� 
    /// </summary>
    public abstract void MoveBullet();

    void HideFromStage()
    {
        //�I�u�W�F�N�g�v�[����Collect�֐����Ăяo�����g�����
        _objectPool.Collect(this.gameObject);
    }
    //�R���[�`��
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(_destroyTime);
        HideFromStage();
    }
}
