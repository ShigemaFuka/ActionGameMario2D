using UnityEngine; 

/// <summary>
/// 生成された瞬間、自身で目標へ向かって移動し、
/// 自身を破棄する 
/// </summary>
public class BulletControllerToTarget : BaseBulletController
{
    [SerializeField] GameObject _target = default;
    Vector3 _pos; 
    [SerializeField] float _speed = 5f; 
    public override void MoveBullet()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
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
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _pos/*_target.transform.position*/, _speed * Time.deltaTime);
        }
    }
}
