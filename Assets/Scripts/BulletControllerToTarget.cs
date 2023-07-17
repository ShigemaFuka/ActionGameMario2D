using UnityEngine; 

/// <summary>
/// �������ꂽ�u�ԁA���g�ŖڕW�֌������Ĉړ����A
/// ���g��j������ 
/// </summary>
public class BulletControllerToTarget : BaseBulletController
{
    [SerializeField] GameObject _target = default;
    Vector3 _pos; 
    [SerializeField] float _speed = 5f; 
    public override void MoveBullet()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        // �|�W�V���������̂܂܂ɂ��Ă��܂��ƁA�����ڂł͑�����_���Ă��܂� 
        _pos = _target.transform.position;
        _pos.y += 1.5f;
    }

    void Update()
    {
        // �^�[�Q�b�g�֌����悤�ɂ��� 
        transform.up = _target.transform.position; 
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
}
