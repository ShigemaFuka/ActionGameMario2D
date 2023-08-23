using UnityEngine;

/// <summary>
/// プレイヤーがデッドゾーンに接触したらゲームオーバー関数を呼び出す
/// エネミーが接触したら、Destroyする
/// これをアタッチし、空オブジェクトにボックスコライダを付けて判定
/// </summary>
public class DeadZoneController : MonoBehaviour
{
    GameManager _gameManager = default;
    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _gameManager.GameOver();
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            Destroy(coll.gameObject); 
        }
    }
}
