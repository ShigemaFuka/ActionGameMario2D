using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�������v���C���[�ɒǏ]���ē���
/// ��Player�^�O�ɐݒ肵�Ă���O�� 
/// �������AY���ړ����Ă��Ǐ]�͂��Ȃ�
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("�ǔ�����Ώ�")] GameObject _target;

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 _pos = _target.transform.position;
        this.gameObject.transform.position = new Vector3(_pos.x, 0, -10);
    }
}
