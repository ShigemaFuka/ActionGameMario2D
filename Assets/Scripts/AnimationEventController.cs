using UnityEngine;

/// <summary> 攻撃の当たり判定をアニメーションを使って制御する </summary>
public class AnimationEventController : MonoBehaviour
{
    [SerializeField]Collider2D[] _colliders;
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Weapon");
        for(var i = 0; i < gos.Length; i++)
        {
            _colliders[i] = gos[i].GetComponentInChildren<Collider2D>(); 
        }
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
        _anim = GetComponent<Animator>();
    }

    /// <summary> 攻撃判定ON </summary>
    void ColliderOn()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    }

    /// <summary> 攻撃判定OFF </summary>
    void ColliderOff()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }
    /// <summary>
    /// 連続アニメーションを防止 
    /// </summary>
    void HitAnimToFalse()
    {
       _anim.SetBool("isHit", false);
    }
}
