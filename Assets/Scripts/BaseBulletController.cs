using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

/// <summary>
/// �e�ۂ̐���̊��N���X
/// ���Ԍo�߁A����ڐG�A�ՓˁA�����玩�g��j��
/// </summary>
public abstract class BaseBulletController : MonoBehaviour
{
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _crashEffectPrefab = default;
    [SerializeField] float _destroyTime = 5f; 

    void Start()
    {
        MoveBullet();
        Destroy(gameObject, _destroyTime);
        
    }

    /// <summary>
    /// �v���C���[�ɍU�����ꂽ�玩�g��j�� 
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon")
        {
            // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
            if (_crashEffectPrefab)
            {
                Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ��������ɏՓ˂����玩�g��j������
    /// </summary>
    void OnCollisionEnter2D(Collision2D coll)
    {
        // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
        if (_crashEffectPrefab)
        {
            Instantiate(_crashEffectPrefab, this.transform.position, this.transform.rotation);
        }
        // �폜
        Destroy(gameObject);
    }
    
    /// <summary>
    /// Start�֐��ŌĂ΂�� 
    /// </summary>
    public abstract void MoveBullet(); 
}
