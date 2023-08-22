using System.Collections;
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
    [SerializeField, Tooltip("��2�U���t���O")] bool _attack2Enable = false;
    [SerializeField, Tooltip("��3�U���t���O")] bool _attack3Enable = false;
    [SerializeField, Tooltip("��1�U���̘A���g�p�h�~���K��Att_1����n�߂邽�߂̃t���O")] bool _startCombo = true;
    [SerializeField, Tooltip("�R���{�U���g�p�\���̃t���O")] bool _canCombo = false;
    [SerializeField, Tooltip("��2�U���󂯕t�����ԁ����Z�b�g����")] float _time1 = 0;
    [SerializeField, Tooltip("��3�U���󂯕t�����ԁ����Z�b�g����")] float _time2 = 0;
    [SerializeField, Tooltip("���Z�b�g����")] float _time3 = 0;
    [SerializeField, Tooltip("�g���K�[�A���g�p�h�~�J�E���g")] int _count = 0;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // �����Ă���Œ��̓R���{�U���g�p�s�� 
        if (Input.GetKey(KeyCode.B))
        {
            _canCombo = false;
            _startCombo = false;
            AttackReset();
        }
        else _startCombo = true; 

        if (Input.GetKeyDown(KeyCode.N) && !_canCombo && !_attack2Enable && !_attack3Enable)
        {
            _canCombo = true;
            _count = 0;
        }

        // �R���{�P 
        if (_canCombo && !_attack2Enable && _startCombo)
        {
            // ��񂾂��g���K�[�N��������  
            if(_count == 0)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    // �ړ����Ȃ���̍U���A�j���[�V���� 
                    _animator.SetTrigger("isAttAndWalk_1Tri");   
                } // �U���݂̂̃A�j���[�V���� 
                else _animator.SetTrigger("isAtt_1Tri"); 
                // �A���Đ��s��  
                _count = 1;
            }
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
                    _count = 0;
                }
            }
            if (_time1 > 1)
            {
                //���͂���Ȃ������Ƃ��̏���(��2�U���ɑJ�ڂ��Ȃ�)
                // �A�j���[�V��������̎��s���ꂽ��(n�b��)��1�U���`�g�p�s�ɂ���
                StartCoroutine(nameof(CanComboCorutine)); 
            }
        }
        // �R���{�Q 
        if (_attack2Enable)
        {
            if (_count == 0)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    _animator.SetTrigger("isAttAndWalk_2Tri");              
                }
                else _animator.SetTrigger("isAtt_2Tri"); 
                _count = 1;
            }
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

                    _count = 0; 
                }
            }
            if (_time2 > 1)
            {
                //���͂���Ȃ������Ƃ��̏���(��3�U���ɑJ�ڂ��Ȃ�)
                // �A�j���[�V��������̎��s���ꂽ��(n�b��)��2�U���g�p�s�ɂ���
                _attack2Enable = false;
                StartCoroutine(nameof(CanComboCorutine));
            }
        }
        // �R���{�R 
        if (_attack3Enable)
        {
            if (_count == 0)
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    _animator.SetTrigger("isAttAndWalk_3Tri");            
                }
                else _animator.SetTrigger("isAtt_3Tri"); 
                _count = 1;
            }
            _time3 += Time.deltaTime; 
            if (_time3 > 1)
            {
                // �A�j���[�V��������̎��s���ꂽ��(n�b��)��3�U���g�p�s�ɂ���  
                StartCoroutine(nameof(CanComboCorutine));
                _attack3Enable = false;
            }
        }
    }

    IEnumerator CanComboCorutine()
    {
        // �X�^�����̃A�j���[�V���� 
        if (Input.GetAxis("Horizontal") != 0) _animator.SetBool("isWalk", true);
        else _animator.SetBool("isWalk", false); // �A�C�h�� �A�j���[�V���� 
        _canCombo = false;
        _startCombo = false;
        _attack2Enable = false;
        _attack3Enable = false;
        AttackReset();
        // n�b�ԃX�^�� 
        yield return new WaitForSeconds(0.2f);
        _startCombo = true; 
    }

    /// <summary>
    /// �A�j���[�V�����ƃ^�C�}�[��������
    /// </summary>
    void AttackReset()
    {
        _animator.ResetTrigger("isAtt_1Tri");
        _animator.ResetTrigger("isAtt_2Tri");
        _animator.ResetTrigger("isAtt_3Tri");
        _animator.ResetTrigger("isAttAndWalk_1Tri"); 
        _animator.ResetTrigger("isAttAndWalk_2Tri"); 
        _animator.ResetTrigger("isAttAndWalk_3Tri"); 
        _time1 = 0;
        _time2 = 0;
        _time3 = 0; 
        _count = 0; 
    }
}
