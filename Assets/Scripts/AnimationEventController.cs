using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �U���̓����蔻����A�j���[�V�������g���Đ��䂷�� </summary>
public class AnimationEventController : MonoBehaviour
{
    Collider2D _collider;
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider2D>(); 
        _collider.enabled = false;
        _anim = GetComponent<Animator>();
    }

    /// <summary> �U������ON </summary>
    void ColliderOn()
    {
        _collider.enabled = true; 
    }

    /// <summary> �U������OFF </summary>
    void ColliderOff()
    {
        _collider.enabled = false;
    }

    void HitAnimToFalse()
    {
       _anim.SetBool("isHit", false);
    }
}
