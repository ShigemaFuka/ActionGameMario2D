using UnityEngine;

/// <summary>
/// �������ꂽ�u�ԁA�������ꂽ�����ʒu�i�I�_�j�E��������I�u�W�F�N�g�i�n�_�j�̕����ֈړ�����A
/// �j�������g�ōs�� 
/// �� �������Œe���q�I�u�W�F�N�g�ɂ��Ă��Ȃ��Ƃ����Ȃ�
/// �}�Y���̈ʒu���Œ肾�ƁA�����������Œ肳�ꂽ�܂�
/// �}�Y���̈ʒu���L�����N�^�̎΂߉E�ゾ������A�i�s�������΂߉E��
/// </summary>
public class BulletControllerStraight : BaseBulletController 
{
    [SerializeField, Tooltip("�e����ԑ���")] float _speed = 3f; 
    Rigidbody2D _rb; 
    [Tooltip("����̒e�𐶐������ԏ�̐e�I�u�W�F�N�g�̈ʒu")] Vector2 _startPointPos; 
    [Tooltip("�x�N�g���擾���Ɏg�����g�̐������̈ʒu")] Vector2 _endPointPos; 
    [Tooltip("�������̕������߂̃x�N�g��")] Vector2 _velo; 


    public override void MoveBullet()
    {
        _endPointPos = this.transform.position;
        GameObject parent = transform.parent.gameObject;
        // �Q��̐e�I�u�W�F�N�g 
        _startPointPos = parent.transform.parent.gameObject.transform.position;
        //_startPointPos = this.transform.root.gameObject.transform.position;
        _velo = _endPointPos - _startPointPos; 

        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _velo.normalized * _speed; 
    }

    void Update()
    {
        // �e�̌���������ɍ��킹�ĕς��� 
        transform.rotation = Quaternion.FromToRotation(Vector2.up, _velo);
    }
}
