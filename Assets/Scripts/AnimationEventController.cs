using UnityEngine;

/// <summary> �U���̓����蔻����A�j���[�V�������g���Đ��䂷�� </summary>
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

    /// <summary> �U������ON </summary>
    void ColliderOn()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }
    }

    /// <summary> �U������OFF </summary>
    void ColliderOff()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }
    /// <summary>
    /// �A���A�j���[�V������h�~ 
    /// </summary>
    void HitAnimToFalse()
    {
       _anim.SetBool("isHit", false);
    }
}
