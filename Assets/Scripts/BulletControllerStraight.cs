using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成された瞬間、生成された初期位置（終点）・生成するオブジェクト（始点）の方向へ移動する、
/// 破棄も自身で行う 
/// ※ 生成側で弾を子オブジェクトにしていないといけない
/// </summary>
public class BulletControllerStraight : BaseBulletController 
{
    [SerializeField, Tooltip("弾が飛ぶ速さ")] float _speed = 3f; 
    Rigidbody2D _rb; 
    [Tooltip("これの弾を生成する一番上の親オブジェクトの位置")] Vector2 _startPointPos; 
    [Tooltip("ベクトル取得時に使う自身の生成時の位置")] Vector2 _endPointPos; 
    [Tooltip("生成時の方向決めのベクトル")] Vector2 _velo; 


    public override void MoveBullet()
    {
        _endPointPos = this.transform.position;
        _startPointPos = this.transform.root.gameObject.transform.position;
        _velo = _endPointPos - _startPointPos; 

        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _velo.normalized * _speed; 
    }

    void Update()
    {
        // 弾の向きを方向に合わせて変える 
        transform.rotation = Quaternion.FromToRotation(Vector2.up, _velo);
    }
}
