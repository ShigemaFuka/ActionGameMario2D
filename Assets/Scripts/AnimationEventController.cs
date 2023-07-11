using UnityEngine;

/// <summary>
/// 攻撃の当たり判定をアニメーションを使って制御する 
/// アニメーションのイベントトリガーで使用
/// </summary>
public class AnimationEventController : MonoBehaviour
{
    //[SerializeField] Collider2D[] _colliders;   // 要らなくね、、、？　
    [SerializeField] Collider2D _rightColl;
    [SerializeField] Collider2D _leftColl; 
    Animator _anim;
    [SerializeField, Tooltip("右武器の軌跡")] TrailRenderer _rightTrail; 
    [SerializeField, Tooltip("左武器の軌跡")] TrailRenderer _leftTrail; 

    void Start()
    {
        ColliderOff(); 
        _rightTrail = GameObject.Find("RightTrail").GetComponent<TrailRenderer>(); 
        _rightTrail.emitting = false; 
        _leftTrail = GameObject.Find("LeftTrail").GetComponent<TrailRenderer>(); 
        _leftTrail.emitting = false; 

        _anim = GetComponent<Animator>();
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
    /// 連続アニメーションを防止 
    /// </summary>
    void HitAnimToFalse()
    {
       _anim.SetBool("isHit", false);
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
