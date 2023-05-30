using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletInstantiate : MonoBehaviour
{
    
    [SerializeField, Tooltip("�e�ۂ̃v���n�u")] GameObject _bulletPrefab;
    GameObject _bullet;
    [SerializeField, Tooltip("���Ԃ��J�E���g����")] float _timeCount;
    [SerializeField, Tooltip("���˂��J�E���g����")] int _shotCount;
    [SerializeField, Tooltip("����")] float _timer;
    [SerializeField, Tooltip("���˃t���O")] bool _isShot;


    // Time.deltaTime �Ŏ��ԃJ�E���g�A���b���ɐ���
    // ���̂��߂ɁA�G�l�~�[�ɃA�^�b�`
    // �v���n�u�Z�b�g�A�폜���s��

    void Start()
    {
        _timeCount = 0;
        _timer = 0;       
        _isShot = true;
        _shotCount = 0;
    }

    void Update()
    {
        // �����ɃJ�E���g��쓮
        // �ЂƂ�1.5�b�A�����ЂƂ�3.5�b�ŏI��聨���̍���2�b
        // 1.5�b�F���Ԋu�̒e�ہi0.5�b���Ɉꔭ * 3�j�A3.5�b�F���˂���ҋ@�܂ł̈�Z�b�g�ibool�j�A
        // 2�b�F���̔��˂܂ł̃C���^�[�o��

        // ���Ԃ̊Ǘ�************
        // 3.5f�b�𒴂����烊�Z�b�g
        if(_timer >= 3.5f)
        {
            //Debug.Log(_timer);
            _isShot = true;
            _timer = 0;
        }
        // ���˃C���^�[�o��2�b
        else if (_timer >= 2.0f)
        {
            _isShot = false;
        }
        // 3.5�b���J�E���g
        if(_timer < 3.5f)
        {
            _timer = _timer + Time.deltaTime;
        }
        // **********************

        
        if(_isShot)
        {
            // 0.5�b�����Ŕ���
            if(_timeCount >= 0.5f)
            {
                // ���Ԃ������ƌ덷���o�邽�߁A�񐔐���
                if(_shotCount >= 3)
                {
                    // �R�񔭎˂��ꂽ��J�E���g���Z�b�g�łO����ĊJ
                    _shotCount = 0;
                }
                else if (_shotCount <= 2)
                {
                    // �R��܂ł͒e�ۂ𐶐�
                    Instantiate(_bulletPrefab);
                    //Debug.Log("_timeCount:   " + _timeCount);
                    _shotCount++;
                }
                _timeCount = 0;
            }
            else if(_timeCount < 0.5f)
            {
                _timeCount = _timeCount + Time.deltaTime;
            }
        }

    }
}

