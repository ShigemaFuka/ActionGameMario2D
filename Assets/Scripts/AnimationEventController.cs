using UnityEngine;

/// <summary>
/// UΜ½θ»θπAj[VπgΑΔ§δ·ι 
/// Aj[VΜCxggK[Εgp
/// </summary>
public class AnimationEventController : MonoBehaviour
{
    Animator _anim = null;
    [SerializeField] Collider2D _rightColl = null;
    [SerializeField] Collider2D _leftColl = null; 
    [Tooltip("EνΜOΥ")] TrailRenderer _rightTrail = null; 
    [Tooltip("ΆνΜOΥ")] TrailRenderer _leftTrail = null; 

    void Start()
    {
        _rightTrail = GameObject.Find("RightTrail").GetComponent<TrailRenderer>(); 
        _leftTrail = GameObject.Find("LeftTrail").GetComponent<TrailRenderer>();
        ColliderOff();
        TrailOff(); 
        _anim = GetComponent<Animator>();
    }

    /// <summary> U»θON </summary>
    void ColliderOn()
    {
        _rightColl.enabled = true; 
        _leftColl.enabled = true; 
    }

    /// <summary> U»θOFF </summary>
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
    /// A±Aj[Vπh~ 
    /// </summary>
    void HitAnimToFalse()
    {
       _anim.SetBool("isHit", false);
    }

    /// <summary>
    /// νΜOΥON
    /// </summary>
    void TrailOn()
    {
        _rightTrail.emitting = true; 
        _leftTrail.emitting = true; 
    }
    /// <summary>
    /// νΜOΥOFF 
    /// </summary>
    void TrailOff()
    {
        _rightTrail.emitting = false; 
        _leftTrail.emitting = false; 
    }

    /// <summary>
    /// Eθ€ΜνΜOΥON 
    /// </summary>
    void RightTrailOn()
    {
        _rightTrail.emitting = true;
    }
    /// <summary>
    /// Άθ€ΜνΜOΥON 
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

    void AnimAttaAndWalkOff()
    {
        _anim.SetBool("isAttAndWalk_1", false);
        _anim.SetBool("isAttAndWalk_2", false);
        _anim.SetBool("isAttAndWalk_3", false);
        _anim.ResetTrigger("isAttAndWalk_1Tri");
        _anim.ResetTrigger("isAttAndWalk_2Tri");
        _anim.ResetTrigger("isAttAndWalk_3Tri");
    }
}
