using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_2 : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField, Tooltip("水平方向の入力値")] float _h;
    [SerializeField, Tooltip("移動速度の計算で使う")] float _movePower;
    //[SerializeField, Tooltip("歩きの速さ")] float _movePowerDef = 30f;

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
        // 力を加えるのは FixedUpdate で行う
        _rb.AddForce(Vector2.right * _h * _movePower, ForceMode2D.Force);
        Debug.Log("AddForce");
    }
}

