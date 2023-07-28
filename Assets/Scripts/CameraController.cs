using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラがプレイヤーに追従して動く
/// ※Playerタグに設定している前提 
/// ただし、Y軸移動しても追従はしない
/// </summary>
public class CameraController : MonoBehaviour
{
    [Tooltip("追尾する対象")] GameObject _target;

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
