using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�L�����𐶐����� 
/// ��莞�Ԃ����ɐ��� 
/// �����ʒu�̃��X�g�����烉���_���ɐ����ʒu�����܂� 
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    //[SerializeField, Tooltip("�����������v���n�u")] GameObject _prefab = default; 
    [SerializeField, Tooltip("�����������v���n�u")] GameObject[] _prefabs = default; 
    [SerializeField, Tooltip("�����C���^�[�o��")] float _intervalTime = 0;
    [Tooltip("����")] float _timer = 0;
    [SerializeField, Tooltip("�����ꏊ")] Transform[] _generatePoses = default;
    int _randomIndex = 0; 
    int _randomPrefabsIndex = 0; 

    void Start()
    {
        _timer = 0;

    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _intervalTime)
        {
            _randomIndex = Random.Range(0, _generatePoses.Length);
            _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
            Instantiate(_prefabs[_randomPrefabsIndex], _generatePoses[_randomIndex].position, Quaternion.identity, gameObject.transform);
            _timer = 0;
        }

    }
}
