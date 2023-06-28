using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 攻撃の当たり判定をアニメーションを使って制御する </summary>
public class AnimationCollider : MonoBehaviour
{
    Collider2D _collider; 

    // Start is called before the first frame update
    void Start()
    {
        _collider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider2D>(); 
        _collider.enabled = false; 
    }

    /// <summary> 攻撃判定ON </summary>
    void ColliderOn()
    {
        _collider.enabled = true; 
    }

    /// <summary> 攻撃判定OFF </summary>
    void ColliderOff()
    {
        _collider.enabled = false;
    }
}
