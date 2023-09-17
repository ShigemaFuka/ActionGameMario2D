using UnityEngine;

/// <summary>
/// �e�ۂ𐧌䂷��R���|�[�l���g
/// �G�L�����������̎q�I�u�W�F�N�g�ɂ��Đ������Ă���Ƃ�
/// </summary>
public class BulletControllerStraight_2 : BaseBulletController
{
    [SerializeField, Tooltip("�e����ԑ���")] float m_speed = 3f;
    [SerializeField, Tooltip("�e�ۂ𐶐�����I�u�W�F�N�g")] GameObject _shooterObject;
    [Tooltip("���[�J���X�P�[��(���]���)�����Ă���")]  Vector3 _bulletLoSc;

    public override void MoveBullet()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        GameObject parentObj = transform.parent.gameObject;
        string grandParentObjName = parentObj.transform.parent.gameObject.name;
        _shooterObject = GameObject.Find(grandParentObjName + "/Chara");

        _bulletLoSc = _shooterObject.transform.localScale;
        float _xSc = _bulletLoSc.x;

        // �ŏ���Chara�𔽓]�����Ă��邽�߁A���E��������]������K�v������  
        // ������
        if (_xSc >= -1)
        {
            // �������ɔ�΂�
            rb.velocity = Vector2.left * m_speed;
        }
        // �E����
        else if (_xSc <= 1)
        {
            // �E�����ɔ�΂�
            rb.velocity = Vector2.right * m_speed;
            transform.Rotate(0, 0, -180);
        }
    }
}
