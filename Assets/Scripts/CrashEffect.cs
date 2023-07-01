using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 破壊の画像と音を出す 
/// 他のスクリプトから、これを生成する必要がある 
/// </summary>
public class CrashEffect : MonoBehaviour
{
    [SerializeField, Tooltip("生存期間（秒）")] float _lifetime = 0.7f;

    void Start()
    {
        Destroy(this.gameObject, _lifetime);
    }
}
