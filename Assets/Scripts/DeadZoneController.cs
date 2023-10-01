using UnityEngine;

/// <summary>
/// プレイヤーがデッドゾーンに接触したらゲームオーバー関数を呼び出す
/// エネミーが接触したら、Collet(SetActiveを偽に)する
/// これをアタッチし、空オブジェクトにボックスコライダを付けて判定
/// </summary>
public class DeadZoneController : MonoBehaviour
{
    [SerializeField] GameManager _gameManager = default;
    [SerializeField] EnemyGenerator _enemyGenerator = default;
    [SerializeField, Tooltip("プレイヤー接触時のSE")] AudioSource _audioSource = default;

    void OnEnable()
    {
        _audioSource.Stop();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(!_gameManager) _gameManager = FindAnyObjectByType<GameManager>();
        if (coll.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            _gameManager.GameOver();
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
        }
    }
}