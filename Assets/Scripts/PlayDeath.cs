using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̎��S���o�̂ݍs��
/// �v���n�u�ɂ��Ă����A�v���C���[��Destroy�����Ƃ��ɐ������A
/// ���S���[�V�������s��
/// </summary>
public class PlayDeath : MonoBehaviour
{
    Animator m_animator; 

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.Play("Death"); 
        Destroy(gameObject, 2f);
    }
}
