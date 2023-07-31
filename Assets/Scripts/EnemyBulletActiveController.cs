using UnityEngine;

/// <summary>
/// �v���C���[����苗���ȉ��ɁA�ڋ߂���� 
/// �e�𐶐�����X�N���v�g���A�N�e�B�u�ɂ��A 
/// ��苗���������ꂽ���A�N�e�B�u�ɂ��� 
/// </summary>
public class EnemyBulletActiveController : MonoBehaviour
{
    [SerializeField, Tooltip("��A�N�e�B�u�ɂ���X�N���v�g")] BulletInstantiate _script = default;
    [SerializeField, Tooltip("��������͈�")] float _range = 5.0f;
    [Tooltip("�v���C���[")] GameObject _target = default;
    [Tooltip("�v���C���[�Ƃ̋���(^2)")]  float _distance = default; 

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
            //2�_�Ԃ̃x�N�g���� 2 ��̒�����Ԃ��܂��B
            //Vector3.Distance ��2�_�Ԃ̋������Z�o����ꍇ�A���[�g�̌v�Z�Ɏ��Ԃ�������܂��B
            //���̂��߁A�P���ɋ����̉��߂��r�������ꍇ�� sqrMagnitude �𗘗p���A
            //2��l�Ŕ�r����悤�ɂ���Ə����������ɍs���܂��B

            Vector2 offset = _target.transform.position - transform.position;
            _distance = offset.sqrMagnitude;

            // sqrMagnitude�͒�����2��Ȃ̂� 
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
