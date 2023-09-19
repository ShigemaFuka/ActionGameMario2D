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

    void Start()
    {
        _playerHp = FindAnyObjectByType<PlayerHp>(); 
    }

    void Update()
    {
        if(_playerHp.PlayerCurrentHp <= 0 && !_isDeath)
        {
            Instantiate(_prefab, _player.transform.position, Quaternion.identity, gameObject.transform);
            _isDeath = true; 
        }
    }
}
