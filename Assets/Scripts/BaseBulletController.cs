using UnityEngine;
using System.Collections;

/// <summary>
/// �e�ۂ̐���̊��N���X
/// ���Ԍo�߁A����ڐG�A�ՓˁA�����玩�g��j��
/// </summary>
public abstract class BaseBulletController : MonoBehaviour
{
    // GM���v���n�u�����Ē����ꂵ�Ă��Ӗ��Ȃ��A�V�[����ɂȂ��ƃ_��(�C���X�^���X��) 
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("ScriptableObject�ȃL�����̃p�����[�^")] CharacterDates _characterDate = default;
    //[SerializeField, Tooltip("�G�t�F�N�g")] GameObject _crashEffectPrefab = default; 
    [SerializeField, Tooltip("���Ŏ��̃G�t�F�N�g")] GameObject _burnEffect = default;
    [SerializeField, Tooltip("�i�[�܂ł̎���")] float _collectTime = 5f;
    [Tooltip("�i�[���ꂽ�e�ۂ��Ǘ����Ă���X�N���v�g")] public MakeBulletObjectPool _makeBulletObjectPool = default;

    //�I�u�W�F�N�g�v�[���p�R���g���[���[�i�[�p�ϐ��錾
    //ObjectPoolController _objectPool;

    void Start()
    {
        if (!_makeBulletObjectPool) _makeBulletObjectPool = FindAnyObjectByType<MakeBulletObjectPool>();
        //MoveBullet();
        //Destroy(gameObject, _destroyTime);
        // �V�[���ɂP�����Ȃ����� 
        _gameManager = FindAnyObjectByType<GameManager>();
        _wfs = new WaitForSeconds(_collectTime);
        //�I�u�W�F�N�g�v�[�����擾
        //_objectPool = FindObjectOfType<ObjectPoolController>();
    }
    void OnEnable()
    {
        MoveBullet();
        TimeLimit();
        _burnEffect.SetActive(false);
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
            _burnEffect.SetActive(true);
            _burnEffect.transform.position = gameObject.transform.position;
            _gameManager.AddScore(_characterDate.Score);
            //Destroy(gameObject);
            BurnEffectToFalse();
            _makeBulletObjectPool.Collect(gameObject);
        }
    }

    /// <summary>
    /// ��������ɏՓ˂����玩�g��j������
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
        _burnEffect.SetActive(true);
        _burnEffect.transform.position = gameObject.transform.position;
        //_gameManager.AddScore(_characterDate.Score);
        // �폜
        //Destroy(gameObject);
        BurnEffectToFalse();
        _makeBulletObjectPool.Collect(gameObject);
    }
    WaitForSeconds _wfs;
    /// <summary>
    /// ��莞�Ԍo�߂�����i�[
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeLimit()
    {
        yield return _wfs;
        _burnEffect.SetActive(true);
        _burnEffect.transform.position = gameObject.transform.position;
        BurnEffectToFalse();
        _makeBulletObjectPool.Collect(gameObject);
    }
    WaitForSeconds _BETFwfs = new WaitForSeconds(0.2f);
    IEnumerator BurnEffectToFalse()
    {
        yield return _BETFwfs;
        _burnEffect.SetActive(false);
        yield return _BETFwfs;
        _makeBulletObjectPool.Collect(gameObject);
    }
    /// <summary>
    /// OnEnable()�֐��ŌĂ΂�� 
    /// </summary>
    public abstract void MoveBullet();
}
