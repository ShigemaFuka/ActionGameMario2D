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
    Animator _animator; 
    [SerializeField, Tooltip("��2�U���t���O")] bool _attack2Enable;
    [SerializeField, Tooltip("��3�U���t���O")] bool _attack3Enable;
    [SerializeField, Tooltip("�K��Att_1����n�߂邽�߂̃t���O")] bool combocombo = true;
    [SerializeField] bool _canCombo = false;
    [SerializeField] float _time1;
    [SerializeField] float _time2;
    [SerializeField] float _time3;
    void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !_canCombo && !_attack2Enable && !_attack3Enable)
            _canCombo = true; 

        if (_canCombo && !_attack2Enable && combocombo)
        {
            //���͂���������ԓ��͂��󂯕t���A���͂���������R���{�Q�ֈڍs�A�Ȃ�������L�����Z��
            //�R���{�P
            _animator.SetBool("isAtt_1", true);

            _time1 += Time.deltaTime;
            if (_time1 > 0.1 && _time1 < 1)
            {
                //���͎�t����
                if (Input.GetKeyDown(KeyCode.N))
                {
                    _time1 = 0;
                    _attack2Enable = true;
                    combocombo = false;
                    //�^�C�}�[�����������A�R���{�P���I�t�ɂ��āA�R���{�Q��True�ɂ���
                }
            }
            if (_time1 > 1)
            {
                //���͂���Ȃ������Ƃ��̏���
                StartCoroutine(nameof(cancombocorutine)); 
            }
        }

        if (_attack2Enable)
        {
            Debug.Log("�R���{�Q");
            _animator.SetBool("isAtt_1", false);
            //�R���{�Q
            _animator.SetBool("isAtt_2", true);
            _time2 += Time.deltaTime; 
            if (_time2 > 0.5 && _time2 < 1)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    _time2 = 0;
                    _attack3Enable = true;
                    _attack2Enable = false;
                }
            }
            if (_time2 > 1)
            {
                _attack2Enable = false;
                StartCoroutine(nameof(cancombocorutine));
            }
        }
        // �R���{�R 
        if (_attack3Enable)
        {
            _animator.SetBool("isAtt_2", false);

            //�R���{�R
            _animator.SetBool("isAtt_3", true);
            _time3 += Time.deltaTime;
            if (_time3 > 1)
            {
                StartCoroutine(nameof(cancombocorutine));
                _attack3Enable = false;
            }
        }
    }

    IEnumerator cancombocorutine()
    {
        Debug.Log("���[�邿��");
        _canCombo = false;
        combocombo = false;
        _attack2Enable = false;
        _attack3Enable = false;
        AttackReset();
        yield return new WaitForSeconds(0.5f);
        combocombo = true;
    }

    void AttackReset()
    {
        _animator.SetBool("isAtt_1", false);
        _animator.SetBool("isAtt_2", false);
        _animator.SetBool("isAtt_3", false);
        _time1 = 0;
        _time2 = 0;
        _time3 = 0;
    }
}
