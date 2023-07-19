using UnityEngine;

/// <summary>
/// プレイヤーの死亡演出のみ行う
/// プレハブにしておき、プレイヤーがDestroyされるときに生成し、
/// 死亡モーションを行う
/// </summary>
public class PlayDeath : MonoBehaviour
{
    Animator _animator = default;  
    GameManager _gameManager = default; 

    void Start()
    {
        _animator = GetComponent<Animator>(); 
        _gameManager = FindAnyObjectByType<GameManager>(); 
        _animator.Play("Death"); 
        Destroy(gameObject, 1.5f); 
    }

    /// <summary> アニメーション内の最後の方で、イベントトリガーで使用 </summary>
    void AfterDeath()
    {
        _gameManager.GameOver(); 
    }
}
