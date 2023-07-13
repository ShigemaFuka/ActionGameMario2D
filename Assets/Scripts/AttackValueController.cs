using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ATK�l��ϓ������邽�߂̃X�N���v�g 
/// �ڐG������A���̐ڐG�������ŎQ�Ƃ���
/// ������A�^�b�`���邾���ł���
/// </summary>
public class AttackValueController : MonoBehaviour
{
    [SerializeField, Tooltip("�Y������L�����̃f�[�^")] CharacterDates _characterDate = default;  
    int _atVa;
    [SerializeField, Tooltip("ATK�l�̃v���}�C")] int _num; 
    [Header("���͕s�v")][Tooltip("�v���C���[����Ɏg���U���l")] public int _attackValue;

    void Start()
    {
        _atVa = _characterDate.Attack; 
    }

    public void Attack()
    {
        // +-2�ōU��
        _attackValue = Random.Range(_atVa - _num, _atVa + _num +1);
        Debug.Log(this.gameObject.name + "  :  this.gameObject.name" + "      �U���l :  " + _attackValue);
    }
}