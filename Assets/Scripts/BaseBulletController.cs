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
    [SerializeField, Tooltip("���Ŏ��̃G�t�F�N�g")] ParticleSystem[] _burnEffectParticles = default;
    [SerializeField, Tooltip("���Ŏ��̃G�t�F�N�g")] AudioSource _audioSource = default;
    [SerializeField, Tooltip("�i�[�܂ł̎���")] float _collectTime = 5f;
    [Tooltip("�i�[���ꂽ�e�ۂ��Ǘ����Ă���X�N���v�g")] public MakeBulletObjectPool _makeBulletObjectPool = default;

    void Start()
    {
        if (!_makeBulletObjectPool) _makeBulletObjectPool = FindAnyObjectByType<MakeBulletObjectPool>();
        // �V�[���ɂP�����Ȃ����� 
        _gameManager = FindAnyObjectByType<GameManager>();
        _wfs = new WaitForSeconds(_collectTime);
    }
    void OnEnable()
    {
        MoveBullet();
        TimeLimit();
    }
    /// <summary>
    /// �v���C���[�ɍU�����ꂽ�玩�g��j�� 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Weapon") || coll.gameObject.CompareTag("RangeWeapon"))
        {
            // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
            _audioSource.PlayOneShot(_audioSource.clip);
            foreach (var bep in _burnEffectParticles)
            {
                bep.transform.position = gameObject.transform.position;
                bep.Play();
            }
            _gameManager.AddScore(_characterDate.Score);
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
        _audioSource.PlayOneShot(_audioSource.clip);
        foreach (var bep in _burnEffectParticles)
        {
            bep.transform.position = gameObject.transform.position;
            bep.Play();
        }
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
        _audioSource.transform.position = gameObject.transform.position;
        _audioSource.PlayOneShot(_audioSource.clip);
        foreach (var bep in _burnEffectParticles)
        {
            bep.transform.position = gameObject.transform.position;
            bep.Play();
        }
        BurnEffectToFalse();
        _makeBulletObjectPool.Collect(gameObject);
    }
    WaitForSeconds _BETFwfs = new WaitForSeconds(0.2f);
    IEnumerator BurnEffectToFalse()
    {
        yield return _BETFwfs;
        _makeBulletObjectPool.Collect(gameObject);
    }
    /// <summary>
    /// OnEnable()�֐��ŌĂ΂�� 
    /// </summary>
    public abstract void MoveBullet();
}