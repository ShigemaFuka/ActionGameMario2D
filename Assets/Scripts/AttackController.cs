using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ڐG������A���̐ڐG�������ŎQ�Ƃ���
/// ������A�^�b�`���邾���ł���
/// </summary>
public class AttackController : MonoBehaviour
{
    [Header("5�ȏ�𐄏�")][SerializeField, Tooltip("�u_attackValue�v�����߂邽�߂̒l")] int _atVa;
    [Header("���͕s�v")][Tooltip("�v���C���[����Ɏg���U���l")] public int _attackValue;


    public void Attack()
    {
        // +-2�ōU��
        _attackValue = Random.Range(_atVa - 2, _atVa + 3);
        Debug.Log(this.gameObject.name + "  :  this.gameObject.name" + "      �U���l :  " + _attackValue);
    }
}
