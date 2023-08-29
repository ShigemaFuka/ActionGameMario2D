using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵キャラを生成する 
/// 一定時間おきに生成 
/// 生成位置のリスト内からランダムに生成位置が決まる 
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    //[SerializeField, Tooltip("生成したいプレハブ")] GameObject _prefab = default; 
    [SerializeField, Tooltip("生成したいプレハブ")] GameObject[] _prefabs = default; 
    [SerializeField, Tooltip("生成インターバル")] float _intervalTime = 0;
    [Tooltip("時間")] float _timer = 0;
    [SerializeField, Tooltip("生成場所")] Transform[] _generatePoses = default;
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
