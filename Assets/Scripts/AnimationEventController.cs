using UnityEngine;

/// <summary>
/// �U���̓����蔻����A�j���[�V�������g���Đ��䂷�� 
/// �A�j���[�V�����̃C�x���g�g���K�[�Ŏg�p
/// </summary>
public class AnimationEventController : MonoBehaviour
{
    //[SerializeField] Collider2D[] _colliders;   // �v��Ȃ��ˁA�A�A�H�@
    [SerializeField] Collider2D _rightColl;
    [SerializeField] Collider2D _leftColl; 
    Animator _anim;
    [SerializeField, Tooltip("�E����̋O��")] TrailRenderer _rightTrail; 
    [SerializeField, Tooltip("������̋O��")] TrailRenderer _leftTrail; 

    void Start()
    {
        ColliderOff(); 
        _rightTrail = GameObject.Find("RightTrail").GetComponent<TrailRenderer>(); 
        _rightTrail.emitting = false; 
        _leftTrail = GameObject.Find("LeftTrail").GetComponent<TrailRenderer>(); 
        _leftTrail.emitting = false; 

        _anim = GetComponent<Animator>();
    }

    /// <summary> �U������ON </summary>
    void ColliderOn()
    {
        _rightColl.enabled = true; 
        _leftColl.enabled = true; 
    }

    /// <summary> �U������OFF </summary>
    void ColliderOff()
    {
        _rightColl.enabled = false; 
        _leftColl.enabled = false; 
    }

    void RightColliderOn()
    {
        _rightColl.enabled = true; 
    }
    void LeftColliderOn()
    {
        _leftColl.enabled = true; 
    }

    void RightColliderOff()
    {
        _rightColl.enabled= false; 
    }

    void LeftColliderOff()
    {
        _leftColl.enabled= false; 
    }

    /// <summary>
    /// �A���A�j���[�V������h�~ 
    /// </summary>
    void HitAnimToFalse()
    {
       _anim.SetBool("isHit", false);
    }

    /// <summary>
    /// ����̋O��ON
    /// </summary>
    void TrailOn()
    {
        _rightTrail.emitting = true; 
        _leftTrail.emitting = true; 
    }
    /// <summary>
    /// ����̋O��OFF 
    /// </summary>
    void TrailOff()
    {
        _rightTrail.emitting = false; 
        _leftTrail.emitting = false; 
    }

    /// <summary>
    /// �E�葤�̕���̋O��ON 
    /// </summary>
    void RightTrailOn()
    {
        _rightTrail.emitting = true;
    }
    /// <summary>
    /// ���葤�̕���̋O��ON 
    /// </summary>
    void LeftTrailOn()
    {
        _leftTrail.emitting = true; 
    }

    void RightTrailOff()
    {
        _rightTrail.emitting = false; 
    }

    void LeftTrailOff()
    {
        _leftTrail.emitting = false; 
    }
}
