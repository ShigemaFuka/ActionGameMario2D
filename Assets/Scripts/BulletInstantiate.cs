using UnityEngine;

/// <summary>
/// �e�ۂ̐�������
/// �R�񐶐�������C���^�[�o��������
/// �ꔭ��������Ƃ����C���^�[�o������������ 
/// �}�Y�����q�I�u�W�F�N�g�ɂ���K�v������ 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BulletInstantiate : MonoBehaviour
{
    [SerializeField, Tooltip("�e�ۂ̃v���n�u")] GameObject _bulletPrefab;
    [SerializeField, Tooltip("���Ԃ��J�E���g����")] float _timeCount;
    [SerializeField, Tooltip("���˂��J�E���g����")] int _shotCount;
    [SerializeField, Tooltip("����")] float _timer;
    [SerializeField, Tooltip("���˃t���O")] public bool _isShot;
    [SerializeField] GameObject _muzzle;
    [SerializeField, Tooltip("�u���b���ƂɂP���������邩�v�̍ŏ��l")] float _minRange = 0.2f;
    [SerializeField, Tooltip("�u���b���ƂɂP���������邩�v�̍ő�l")] float _maxRange = 0.7f;
    [Header("_oneInterval = _minRange �` _maxRange")]
    [SerializeField, Tooltip("���b���ƂɂP���������邩")] float _oneInterval = 1;
    [SerializeField, Tooltip("�P�T�C�N����̃C���^�[�o��")] float _wait = 2;
    [Header("_totalTime = _oneInterval * _maxCount + _wait")]
    [SerializeField, Tooltip("���˂���ҋ@�܂ł̂P�T�C�N���̎���")] float _totalTime = 5;
    [SerializeField, Tooltip("�P�T�C�N���ŉ����������邩")] int _maxCount = 3;
    [Tooltip("�i�[���ꂽ�e�ۂ��Ǘ����Ă���X�N���v�g")] MakeBulletObjectPool _makeBulletObjectPool = default;

    // Time.deltaTime �Ŏ��ԃJ�E���g�A���b���ɐ���
    // ���̂��߂ɁA�G�l�~�[�ɃA�^�b�`
    // �v���n�u�Z�b�g�A�폜���s��
    void Start()
    {
        if(!_makeBulletObjectPool) _makeBulletObjectPool = FindAnyObjectByType<MakeBulletObjectPool>();
    }
    void OnEnable()
    {
        _oneInterval = Random.Range(_minRange, _maxRange);
        _timeCount = 0;
        _timer = 0;
        _shotCount = 0;
        _muzzle = transform.GetChild(0).gameObject;
        _totalTime = _oneInterval * _maxCount + _wait;
    }
    void Update()
    {
        // �����ɃJ�E���g��쓮
        // �ЂƂ�1.5->3�b�A�����ЂƂ�3.5->5�b�ŏI��聨���̍���2�b
        // 1.5->3�b�F���Ԋu�̒e�ہi0.5->1�b���Ɉꔭ * 3�j�A3.5->5�b�F���˂���ҋ@�܂ł̈�Z�b�g�ibool�j�A
        // 2�b�F���̔��˂܂ł̃C���^�[�o��

        // ���Ԃ̊Ǘ�************
        // 3.5f�b�𒴂����烊�Z�b�g
        if(_timer >= _totalTime) // 3.5->5
        {
            _isShot = true;
            _timer = 0;
        }
        // ���˃C���^�[�o��2�b
        else if (_timer >= _wait)
        {
            _isShot = false;
        }
        // 3.5�b���J�E���g
        if(_timer < _totalTime)  // 3.5->5
        {
            _timer = _timer + Time.deltaTime;
        }
        // **********************
        
        if(_isShot)
        {
            // 0.5�b�����Ŕ���
            if(_timeCount >= _oneInterval)  // 0.5->1
            {
                // ���Ԃ������ƌ덷���o�邽�߁A�񐔐���
                if(_shotCount >= _maxCount)
                {
                    // �R�񔭎˂��ꂽ��J�E���g���Z�b�g�łO����ĊJ
                    _shotCount = 0;
                }
                else if (_shotCount <= _maxCount - 1)
                {
                    // �R�񖢖��܂ł͒e�ۂ𐶐� 
                    if (_muzzle)
                        _makeBulletObjectPool.Launch(_muzzle.gameObject.transform.position);
                    _shotCount++;
                }
                _timeCount = 0;
            }
            else if(_timeCount < _oneInterval) // 0.5->1
            {
                _timeCount = _timeCount + Time.deltaTime;
            }
        }
    }
}

