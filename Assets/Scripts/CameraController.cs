using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("追尾する対象")] GameObject _target;
    [SerializeField, Tooltip("追尾する対象の名前")] string _targetName;

    void Start()
    {
        _target = GameObject.Find(_targetName);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _pos = _target.transform.position;
        this.gameObject.transform.position = new Vector3(_pos.x, 0, -10);
    }
}
