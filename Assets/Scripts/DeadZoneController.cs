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
           
    //void OnCollisionEnter2D(Collision2D coll)
    //{
        //if (coll.gameObject.CompareTag("Player"))
        //{
        //    _gameManager.GameOver();
        //}
        //else if (coll.gameObject.CompareTag("Enemy"))
        //{
        //    _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
        //    _enemyGenerator.CharaCount--;
        //}
    //}
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(!_gameManager) _gameManager = FindAnyObjectByType<GameManager>();
        if (coll.gameObject.CompareTag("Player"))
        {
            _gameManager.GameOver();
        }
        else if (coll.gameObject.CompareTag("Enemy"))
        {
            _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
            _enemyGenerator.CharaCount--;
        }
    }
}
