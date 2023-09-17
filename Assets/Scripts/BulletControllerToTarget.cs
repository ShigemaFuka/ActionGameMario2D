using UnityEngine;
/// <summary>
/// �������ꂽ�u�ԁA���g�ŖڕW�֌������Ĉړ����A
/// �����Ŏ��g��j������ 
/// ���������u�ԂɁA�ŏI���B�n�_�����܂�
/// </summary>
public class BulletControllerToTarget : BaseBulletController
{
    GameObject _target = default;
    [Tooltip("�^�[�Q�b�g�̃|�W�V����")] Vector3 _pos; 
    [SerializeField] float _speed = 5f; 
    public override void MoveBullet()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        // �|�W�V���������̂܂܂ɂ��Ă��܂��ƁA�����ڂł͑�����_���Ă��܂� 
        if(_target)
        {
            _pos = _target.transform.position;
            _pos.y += 1.5f;
            _pos = new Vector2(_pos.x, _pos.y);
        }
    }

    void Update()
    {
        // �^�[�Q�b�g�֌����悤�ɂ��� 
        if(_target)
        {
            transform.up = _target.transform.position;
            if (_pos == gameObject.transform.position)
            {
                Destroy(gameObject);
            }
            // ���������u�ԂɁA�ŏI���B�n�_�����܂�  
            else gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _pos/*_target.transform.position*/, _speed * Time.deltaTime);
        }
    }
}
