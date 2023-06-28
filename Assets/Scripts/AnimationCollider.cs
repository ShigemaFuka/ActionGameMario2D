using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> UΜ½θ»θπAj[VπgΑΔ§δ·ι </summary>
public class AnimationCollider : MonoBehaviour
{
    Collider2D _collider; 

    // Start is called before the first frame update
    void Start()
    {
        _collider = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Collider2D>(); 
        _collider.enabled = false; 
    }

    /// <summary> U»θON </summary>
    void ColliderOn()
    {
        _collider.enabled = true; 
    }

    /// <summary> U»θOFF </summary>
    void ColliderOff()
    {
        _collider.enabled = false;
    }
}
