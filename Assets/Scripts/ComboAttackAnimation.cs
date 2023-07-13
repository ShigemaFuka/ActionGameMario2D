using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �R�i�K�̃R���{�U���̃A�j���[�V�������� 
/// 
/// N�L�[�������Ă���A0.5�b�ȓ���N�L�[�̓��͂������
/// �U�����[�V�����Q�Ɉڍs���A�����0.5�b�ȓ���N�L�[�̓��͂������
/// �U�����[�V�����R�Ɉڍs����
/// �e���[�V�������A0.5�b�ȓ��ɓ��͂��Ȃ���΃L�����Z�������ɂȂ� 
/// </summary>
public class ComboAttackAnimation : MonoBehaviour
{
    Animator _animator; 
    [SerializeField] bool combo1enable;
    [SerializeField] bool combo2enable;
    [SerializeField] bool combocombo = true;
    [SerializeField] bool cancombo = false;
    [SerializeField] float combo1time;
    [SerializeField] float combo2time;
    [SerializeField] float combo3time;
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !cancombo && !combo1enable && !combo2enable)
        { cancombo = true; }

        if (cancombo && !combo1enable && combocombo)
        {
            //���͂���������ԓ��͂��󂯕t���A���͂���������R���{�Q�ֈڍs�A�Ȃ�������L�����Z��
            //�R���{�P
            _animator.SetBool("isAtt_1", true);
            Debug.Log("�R���{�P");

            combo1time += Time.deltaTime;
            if (combo1time > 0.1 && combo1time < 1)
            {
                //���͎�t����
                if (Input.GetKeyDown(KeyCode.N))
                {
                    combo1time = 0;

                    //combo1enable = false;
                    combo1enable = true;
                    combocombo = false;
                    //�^�C�}�[�����������A�R���{�P���I�t�ɂ��āA�R���{�Q��True�ɂ���
                }
            }
            if (combo1time > 1)
            {
                //���͂���Ȃ������Ƃ��̏���
                StartCoroutine("cancombocorutine");
            }
        }

        if (combo1enable)
        {
            Debug.Log("�R���{�Q");
            //AttackReset(); 
            _animator.SetBool("isAtt_1", false);
            //�R���{�Q
            _animator.SetBool("isAtt_2", true);
            combo2time += Time.deltaTime;
            if (combo2time < 1 && combo2time > 0.7)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    combo2time = 0;
                    combo2enable = true;
                    combo1enable = false;
                }
            }
            if (combo2time > 1)
            {
                combo1enable = false;
                StartCoroutine("cancombocorutine");
            }
        }

        if (combo2enable)
        {
            Debug.Log("�R���{�R");
            _animator.SetBool("isAtt_2", false);

            //�R���{�R
            _animator.SetBool("isAtt_3", true);
            combo3time += Time.deltaTime;
            if (combo3time > 1)
            {
                StartCoroutine("cancombocorutine");
                combo2enable = false;
            }
        }
    }

    IEnumerator cancombocorutine()
    {

        Debug.Log("���[�邿��");
        cancombo = false;
        combocombo = false;
        combo1enable = false;
        combo2enable = false;
        AttackReset();
        yield return new WaitForSeconds(0.5f);
        combocombo = true;
    }

    void AttackReset()
    {
        _animator.SetBool("isAtt_1", false);
        _animator.SetBool("isAtt_2", false);
        _animator.SetBool("isAtt_3", false);
        combo1time = 0;
        combo2time = 0;
        combo3time = 0;
    }
}





    //Animator _animator; 
    //[SerializeField, Tooltip("�R���{�U���̒i�K")] int _count;
    //[SerializeField, Tooltip("�R���{�U���̎󂯕t������")] float _time;
    ////[SerializeField] bool _timeCountBool = false;
    //[SerializeField] bool _canCombo = false;
    //bool _canAttack_1; 
    //bool _canAttack_2;
    //bool _canAttack_3;
    //void Start()
    //{
    //    _animator = GetComponent<Animator>(); 
    //    _count = 0;
    //    _time = 0;
    //    ResetCanAttack(); 
    //}

    //void Update()
    //{
    //    // �U�����[�V�����ɏ��߂ē������� �� = �R���{�J�n���Ă��Ȃ�  
    //    if(Input.GetKeyDown(KeyCode.N) && _count == 0 && !_canCombo)
    //    {
    //        _canCombo = true;
    //        ResetTrigger(); 
    //        //_canAttack_1 = true; 
    //    }
        
    //    if (_canCombo)
    //    {
    //        if (Input.GetKeyDown(KeyCode.N))
    //        {
    //            _count ++; 
    //        }

    //        if (_count == 1)
    //        {
    //            _animator.SetBool("isAtt_1", true); 
                
    //            _time += Time.deltaTime;
    //            if (_time < 1 && _time > 0.5 && Input.GetKeyDown(KeyCode.N))
    //            {
    //                ResetTrigger();
    //                _count++;
                    
    //                //_canAttack_2 = true; 
                    
    //            }
    //            if (_time > 1)
    //            {
    //                StartCoroutine("CanceleCombo");
    //            }
    //            Debug.Log("Attack�Q1");
    //        }
    //        else if (_count == 2)
    //        {
    //            _animator.SetBool("isAtt_2", true); 

    //            _time += Time.deltaTime;
    //            if (_time < 1.3 && _time > 0.7 && Input.GetKeyDown(KeyCode.N))
    //            {
    //                ResetTrigger();
    //                _count++;
                    
                    
    //                //_canAttack_3 = true;
    //            }
    //            if (_time > 1)
    //            {
    //                StartCoroutine("CanceleCombo"); 
    //            }
    //        }
    //        else if (_count == 3)
    //        {
    //            _animator.SetBool("isAtt_3", true);
    //            _time += Time.deltaTime; 
    //            StartCoroutine("CanceleCombo"); 
    //        }
    //    }
    //    Debug.Log(_count);
    //}
    

    //IEnumerator CanceleCombo()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    _time = 0;
    //    _count = 0;
    //    _canCombo = false;
    //    ResetCanAttack(); 
    //}

    ///// <summary>
    ///// �A�j���[�V�������J��Ԃ��Ȃ��悤�ɂ���Bool
    ///// </summary>
    //void ResetCanAttack()
    //{
    //    //_canAttack_1 = false; 
    //    //_canAttack_2 = false; 
    //    //_canAttack_3 = false; 
    //    ResetTrigger(); 
    //}

    //void ResetTrigger()
    //{
    //    _animator.SetBool("isAtt_1", false); 
    //    _animator.SetBool("isAtt_2", false); 
    //    _animator.SetBool("isAtt_3", false); 
    //}
//}
