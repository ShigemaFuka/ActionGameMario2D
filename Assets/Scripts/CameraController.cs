using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("’Ç”ö‚·‚é‘ÎÛ")] GameObject _target;
    //[SerializeField, Tooltip("’Ç”ö‚·‚é‘ÎÛ‚Ì–¼‘O")] string _targetName;

    void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _pos = _target.transform.position;
        this.gameObject.transform.position = new Vector3(_pos.x, 0, -10);
    }
}
