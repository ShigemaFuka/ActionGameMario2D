using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("í«îˆÇ∑ÇÈëŒè€")] GameObject _target;

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Vector3 _pos = _target.transform.position;
        this.gameObject.transform.position = new Vector3(_pos.x, 0, -10);
    }
}
