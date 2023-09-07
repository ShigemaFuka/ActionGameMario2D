using UnityEngine;
using System.Collections;
/// <summary>
/// 敵キャラの死亡演出用のオブジェクトを制御する
/// 3パターンのアニメーションをランダムで再生、プレイヤーによって吹き飛ばされるように
/// 力を加える方向（左右）を切り替えている
/// コルーチンで1秒ほどアニメーション再生の時間を取っている
/// EnemyGeneratorでオブジェクトプールを使い、予め敵キャラと同数生成、
/// 必要に応じて出し入れしている
/// </summary>
public class ImpactDeathEnemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb= default;
    Vector2 _position = Vector2.zero;
    Vector2 _targetPosition = Vector2.zero;
    Vector2 _distance = Vector2.zero; 
    Vector2 _direction = new Vector2(-1, 1);
    [SerializeField] float _power = 10f;
    [SerializeField] Animator _animator = default;
    int _num = 0;
    EnemyGenerator _enemyGenerator = default;
    void OnEnable()
    {
        _enemyGenerator = FindAnyObjectByType<EnemyGenerator>(); 
        //_animator = GetComponent<Animator>(); 
        //_rb = GetComponent<Rigidbody2D>();
        _position = this.transform.position;
        _targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _distance = (_targetPosition - _position).normalized;
        // Playerが敵より右に居るとき 
        if (_distance.x > 0)
        {
            _direction.x = -_direction.x;
            Vector2 _localScale = this.transform.localScale;
            this.transform.localScale = new Vector3(-_localScale.x, this.transform.localScale.y);
        }
        //_num = Random.Range(1, 4);
        _num = Random.Range(1, 3);
        //if (_num == 3) _power = 20;
        _animator.SetTrigger($"isDead_{_num}"); 
        _rb.AddForce(_direction * _power, ForceMode2D.Impulse);
        StartCoroutine(CoroutineCollect());
    }
    WaitForSeconds _wfs = new WaitForSeconds(1f);
    IEnumerator CoroutineCollect()
    {
        yield return _wfs;
        _enemyGenerator.Collect(_enemyGenerator.DeathPrefabQueue, this.gameObject);
    }
}
