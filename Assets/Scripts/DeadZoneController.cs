using UnityEngine;

/// <summary>
/// プレイヤーがデッドゾーンに接触したらゲームオーバー関数を呼び出す
/// エネミーが接触したら、Collet(SetActiveを偽に)する
/// これをアタッチし、空オブジェクトにボックスコライダを付けて判定
/// </summary>
public class DeadZoneController : MonoBehaviour
{
    GameManager _gameManager = default;
    [SerializeField] EnemyGenerator _enemyGenerator = default;
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _gameManager.GameOver();
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, coll.gameObject);
            _enemyGenerator.CharaCount--;
        }
    }
}
