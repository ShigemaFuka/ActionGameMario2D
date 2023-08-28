using UnityEngine;

/// <summary>
/// 攻撃の当たり判定をアニメーションを使って制御する 
/// アニメーションのイベントトリガーで使用
/// </summary>
public class AnimationEventController : MonoBehaviour
{
    Animator _anim = null;
    [SerializeField] Collider2D _rightColl = null;
    [SerializeField] Collider2D _leftColl = null; 
    [Tooltip("右武器の軌跡")] TrailRenderer _rightTrail = null; 
    [Tooltip("左武器の軌跡")] TrailRenderer _leftTrail = null;
    [Tooltip("攻撃SE")] AudioSource _audioSource = null; 

    void Start()
    {
        _rightTrail = GameObject.Find("RightTrail").GetComponent<TrailRenderer>(); 
        _leftTrail = GameObject.Find("LeftTrail").GetComponent<TrailRenderer>();
        ColliderOff();
        TrailOff(); 
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnSE()
    {
        _audioSource.PlayOneShot(_audioSource.clip); 
    }

    /// <summary> 攻撃判定ON </summary>
    void ColliderOn()
    {
        _rightColl.enabled = true; 
        _leftColl.enabled = true; 
    }

    /// <summary> 攻撃判定OFF </summary>
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
    /// 武器の軌跡ON
    /// </summary>
    void TrailOn()
    {
        _rightTrail.emitting = true; 
        _leftTrail.emitting = true; 
    }
    /// <summary>
    /// 武器の軌跡OFF 
    /// </summary>
    void TrailOff()
    {
        _rightTrail.emitting = false; 
        _leftTrail.emitting = false; 
    }

    /// <summary>
    /// 右手側の武器の軌跡ON 
    /// </summary>
    void RightTrailOn()
    {
        _rightTrail.emitting = true;
    }
    /// <summary>
    /// 左手側の武器の軌跡ON 
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
