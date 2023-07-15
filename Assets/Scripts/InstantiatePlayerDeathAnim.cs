using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空のオブジェクトにこれをアタッチし、
/// プレイヤーの死亡アニメーションを生成する
/// ※PlayerDeathスクリプト内でDestroy関数を入れている 
/// </summary>
public class InstantiatePlayerDeathAnim : MonoBehaviour
{
    PlayerHp _playerHp;
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _player;
    bool _isDeath = false; 

    // Start is called before the first frame update
    void Start()
    {
        _playerHp = FindAnyObjectByType<PlayerHp>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerHp.PlayerCurrentHp <= 0 && !_isDeath)
        {
            Instantiate(_prefab, _player.transform.position, Quaternion.identity, gameObject.transform);
            _isDeath = true; 
        }
    }
}
