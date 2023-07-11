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
    [SerializeField, Tooltip("�R���{�U���̒i�K")] int _count;
    [SerializeField, Tooltip("�R���{�U���̎󂯕t������")] float _time;
    //[SerializeField] bool _timeCountBool = false;
    bool _canCombo = false; 
    void Start()
    {
        _animator = GetComponent<Animator>(); 
        _count = 0;
        _time = 0;
    }

    void Update()
    {
        // �U�����[�V�����ɏ��߂ē������� �� = �R���{�J�n���Ă��Ȃ�  
        if(Input.GetKeyDown(KeyCode.N) && _count == 0)
        {
            _canCombo = true; 
        }
        
        if (_canCombo)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                //_timeCountBool = true; 
                _count ++; 
            }

            if (_count == 1)
            {
                // Play Anim_3
                _animator.Play("Right Hand");

                _time += Time.deltaTime;
                if (_time < 1 && _time > 0.1 && Input.GetKeyDown(KeyCode.N))
                {
                    _count++;
                }
                if (_time > 1)
                {
                    StartCoroutine("CanceleCombo");
                }
                Debug.Log("a");
            }
            else if (_count == 2)
            {
                // Play Anim_2
                _animator.Play("Left Hand");

                _time += Time.deltaTime;
                if (_time < 1 && _time > 0.1 && Input.GetKeyDown(KeyCode.N))
                {
                    _count++;
                }
                if (_time > 1)
                {
                    StartCoroutine("CanceleCombo"); 
                }
                Debug.Log("b");
            }
            else if (_count == 3)
            {
                // Play Anim_1
                _animator.Play("Both Hands");

                _time += Time.deltaTime; 
                if (_time > 1)
                {
                    StartCoroutine("CanceleCombo");
                }
                Debug.Log("c");
            }
        }
    }

    IEnumerator CanceleCombo()
    {
        _time = 0; 
        _count = 0; 
        yield return new WaitForSeconds(0.5f);
        _canCombo = false; 
    }
}
