using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField, Tooltip("���������̓��͒l")] float _h;
    [SerializeField, Tooltip("�ړ����x�̌v�Z�Ŏg��")] float _movePower;
    //[SerializeField, Tooltip("�����̑���")] float _movePowerDef = 30f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _h = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        // �͂�������̂� FixedUpdate �ōs��
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
        Debug.Log("AddForce");
    }
}

