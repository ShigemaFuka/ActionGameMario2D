using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// �j��̉摜�Ɖ����o�� 
/// ���̃X�N���v�g����A����𐶐�����K�v������ 
/// </summary>
public class CrashEffect : MonoBehaviour
{
    [SerializeField, Tooltip("�������ԁi�b�j")] float _lifetime = 0.7f;

    void Start()
    {
        Destroy(this.gameObject, _lifetime);
    }
}
