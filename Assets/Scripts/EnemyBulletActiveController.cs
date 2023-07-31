using UnityEngine;

/// <summary>
/// プレイヤーが一定距離以下に、接近すると 
/// 弾を生成するスクリプトをアクティブにし、 
/// 一定距離よりも離れたら非アクティブにする 
/// </summary>
public class EnemyBulletActiveController : MonoBehaviour
{
    [SerializeField, Tooltip("非アクティブにするスクリプト")] BulletInstantiate _script = default;
    [SerializeField, Tooltip("反応する範囲")] float _range = 5.0f;
    [Tooltip("プレイヤー")] GameObject _target = default;
    [Tooltip("プレイヤーとの距離(^2)")]  float _distance = default; 

    void Start()
    {
        _script = GetComponent<BulletInstantiate>(); 
        _script.enabled = false;
        _target = GameObject.FindGameObjectWithTag("Player"); 
    }

    void Update()
    {
        if (_target)
        {
            //2点間のベクトルの 2 乗の長さを返します。
            //Vector3.Distance で2点間の距離を算出する場合、ルートの計算に時間がかかります。
            //このため、単純に距離の遠近を比較したい場合は sqrMagnitude を利用し、
            //2乗値で比較するようにすると処理が高速に行えます。

            Vector2 offset = _target.transform.position - transform.position;
            _distance = offset.sqrMagnitude;

            // sqrMagnitudeは長さの2乗なので 
            if (_distance <= _range * _range)
            {
                _script.enabled = true;
            }
            else
            {
                _script.enabled = false; 
            }
        }
    }
}
