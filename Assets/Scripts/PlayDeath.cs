using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの死亡演出のみ行う
/// プレハブにしておき、プレイヤーがDestroyされるときに生成し、
/// 死亡モーションを行う
/// </summary>
public class PlayDeath : MonoBehaviour
{
    Animator m_animator; 

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.Play("Death"); 
        Destroy(gameObject, 2f);
    }
}
