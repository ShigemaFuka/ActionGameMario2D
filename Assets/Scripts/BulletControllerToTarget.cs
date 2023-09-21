using UnityEngine;
/// <summary>
/// 生成された瞬間、自身で目標へ向かって移動し、
/// 時限で自身を破棄する 
/// 生成した瞬間に、最終到達地点が決まる
/// </summary>
public class BulletControllerToTarget : BaseBulletController
{
    GameObject _target = default;
    [Tooltip("ターゲットのポジション")] Vector3 _pos; 
    [SerializeField] float _speed = 5f;
    //[Tooltip("格納された弾丸を管理しているスクリプト")] MakeBulletObjectPool _makeBulletObjectPool = default;
    public override void MoveBullet()
    {
        //if (!_makeBulletObjectPool) _makeBulletObjectPool = FindAnyObjectByType<MakeBulletObjectPool>();

        if(!_target) _target = GameObject.FindGameObjectWithTag("Player");
        // ポジションをそのままにしてしまうと、見た目では足元を狙ってしまう 
        if(_target)
        {
            _pos = _target.transform.position;
            _pos.y += 1.5f;
            _pos = new Vector2(_pos.x, _pos.y);
        }
    }

    void Update()
    {
        // ターゲットへ向くようにする 
        if(_target)
        {
            transform.up = _target.transform.position;
            if (_pos == gameObject.transform.position)
            {
                //Destroy(gameObject);
                _makeBulletObjectPool.Collect(gameObject);
            }
            // 生成した瞬間に、最終到達地点が決まる  
            else gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _pos/*_target.transform.position*/, _speed * Time.deltaTime);
        }
    }
}
