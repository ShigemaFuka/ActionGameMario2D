using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R�i�K�̃R���{�U���̃A�j���[�V�������� 
/// 
/// N�L�[�������Ă���A
/// �U�����[�V�����P�Ɉڍs���A0.5�b�ȓ���N�L�[�̓��͂������
/// �U�����[�V�����Q�Ɉڍs���A�����0.5�b�ȓ���N�L�[�̓��͂������
/// �U�����[�V�����R�Ɉڍs����
/// �e���[�V�������A0.5�b�ȓ��ɓ��͂��Ȃ���΃L�����Z�������ɂȂ� 
/// </summary>
public class ComboAttackAnimation : MonoBehaviour
{
    Animator _animator = default; 
    [SerializeField, Tooltip("��1�U���t���O")] bool _attack1Enable = false;
    [SerializeField, Tooltip("��2�U���t���O")] bool _attack2Enable = false;
    [SerializeField, Tooltip("��3�U���t���O")] bool _attack3Enable = false;
    [SerializeField, Tooltip("��1�U���̘A���g�p�h�~���K��Att_1����n�߂邽�߂̃t���O")] bool _startCombo = true;
    [SerializeField, Tooltip("�R���{�U���g�p�\���̃t���O")] bool _canCombo = false;
    [SerializeField, Tooltip("��2�U���󂯕t�����ԁ����Z�b�g����")] float _time1 = 0;
    [SerializeField, Tooltip("��3�U���󂯕t�����ԁ����Z�b�g����")] float _time2 = 0;
    [SerializeField, Tooltip("���Z�b�g����")] float _time3 = 0;
    [SerializeField] int count = 0; 
    void Start()
    {
        _animator = GetComponent<Animator>();
        _attack1Enable = true; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !_canCombo && !_attack2Enable && !_attack3Enable)
        {
            _canCombo = true;

            count = 0;
        }
           
        // �R���{�P 
        if (_canCombo && !_attack2Enable && _startCombo)
        {
            //���͂���������ԓ��͂��󂯕t���A���͂���������R���{�Q�ֈڍs�A�Ȃ�������L�����Z��
            if (Input.GetAxis("Horizontal") != 0 && count == 0)
            {
                //_animator.SetBool("isAttAndWalk_1", true);
                _animator.SetTrigger("isAttAndWalk_1Tri"); 

                count = 1;
            }
            else _animator.SetBool("isAtt_1", true);


            _time1 += Time.deltaTime;
            if (_time1 > 0.1 && _time1 < 1)
            {
                //���͎�t����
                if (Input.GetKeyDown(KeyCode.N))
                {
                    // ��2�U���ɑJ�� 
                    _time1 = 0;
                    _attack2Enable = true;
                    _startCombo = false; 

                    count = 0;
                }
            }
            if (_time1 > 1)
            {
                //���͂���Ȃ������Ƃ��̏���(��2�U���ɑJ�ڂ��Ȃ�)
                // �A�j���[�V��������̎��s���ꂽ��(1�b���炢)��1�U���g�p�s�ɂ���
                StartCoroutine(nameof(CanComboCorutine)); 
            }
        }
        // �R���{�Q 
        if (_attack2Enable)
        {
            if (_animator.GetBool("isAttAndWalk_1")) _animator.SetBool("isAttAndWalk_1", false);
            else if (_animator.GetBool("isAtt_1")) _animator.SetBool("isAtt_1", false);

            if (Input.GetAxis("Horizontal") != 0 && count == 0)
            {
                //_animator.SetBool("isAttAndWalk_2", true);
                _animator.SetTrigger("isAttAndWalk_2Tri");

                count = 1; 
            }
            else _animator.SetBool("isAtt_2", true);
            

            _time2 += Time.deltaTime;
            // �u_time2 > 0.1�v���Ƒ������āA�A�j���[�V�������r���ł��J�ڂ��� 
            if (_time2 > 0.4 && _time2 < 1)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    // ��3�U���ɑJ�� 
                    _time2 = 0;
                    _attack3Enable = true;
                    _attack2Enable = false;

                    count = 0; 
                }
            }
            if (_time2 > 1)
            {
                //���͂���Ȃ������Ƃ��̏���(��3�U���ɑJ�ڂ��Ȃ�)
                // �A�j���[�V��������̎��s���ꂽ��(1�b���炢)��2�U���g�p�s�ɂ���
                _attack2Enable = false;
                StartCoroutine(nameof(CanComboCorutine));
            }
        }
        // �R���{�R 
        if (_attack3Enable)
        {
            if (_animator.GetBool("isAttAndWalk_2")) _animator.SetBool("isAttAndWalk_2", false);
            else if (_animator.GetBool("isAtt_2")) _animator.SetBool("isAtt_2", false);


            if (Input.GetAxis("Horizontal") != 0 && count == 0)
            {
                //_animator.SetBool("isAttAndWalk_3", true);
                _animator.SetTrigger("isAttAndWalk_3Tri");

                count = 1; 
            }
            else _animator.SetBool("isAtt_3", true);



            _time3 += Time.deltaTime; 
            if (_time3 > 1)
            {
                // �A�j���[�V��������̎��s���ꂽ��(1�b���炢)��3�U���g�p�s�ɂ���  
                StartCoroutine(nameof(CanComboCorutine));
                _attack3Enable = false;
            }
        }
    }

    IEnumerator CanComboCorutine()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetBool("isWalk", true);
        }
        _canCombo = false;
        _startCombo = false;
        _attack2Enable = false;
        _attack3Enable = false;
        AttackReset();
        yield return new WaitForSeconds(0.2f);
        _startCombo = true;

        _attack1Enable = true; 

    }

    /// <summary>
    /// �A�j���[�V�����ƃ^�C�}�[��������
    /// </summary>
    void AttackReset()
    {
        _animator.SetBool("isAttAndWalk_1", false);
        _animator.SetBool("isAttAndWalk_2", false);
        _animator.SetBool("isAttAndWalk_3", false);
        //_animator.ResetTrigger("isAttAndWalk_1Tri"); 
        //_animator.ResetTrigger("isAttAndWalk_2Tri"); 
        //_animator.ResetTrigger("isAttAndWalk_3Tri"); 
        _animator.SetBool("isAtt_1", false);
        _animator.SetBool("isAtt_2", false);
        _animator.SetBool("isAtt_3", false);
        _time1 = 0;
        _time2 = 0;
        _time3 = 0;

        count = 0; 
    }
}
