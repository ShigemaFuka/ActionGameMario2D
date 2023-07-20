using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("�ǔ�����Ώ�")] GameObject _target;
    //[SerializeField, Tooltip("�ǔ�����Ώۂ̖��O")] string _targetName;

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
