using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ꂽ�u�Ԏ��͂ō��Ɉړ�����
/// �j�������g�ōs�� 
/// </summary>
public class BulletControllerToLeft : BaseBulletController 
{
    [SerializeField, Tooltip("�e����ԑ���")] float _speed = 3f;
    Rigidbody2D _rb; 

    public override void MoveBullet()
    {
        _rb = GetComponent<Rigidbody2D>(); 
        _rb.velocity = Vector2.left * _speed; 
    }
    
    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    // �v���C���[�ɍU�����ꂽ�������
    //    if (coll.gameObject.tag == "Weapon")        
    //    {
    //        /*
    //        // �����̉��Ɖ摜���o�� 
    //        AudioSource.PlayClipAtPoint(_clip, transform.position);
    //        */
    //        // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
    //        if (_effectPrefab)
    //        {
    //            Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
    //        }
    //        Destroy(gameObject);
    //    } 
    //}

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
    //    if (_effectPrefab)
    //    {
    //        Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
    //    }
    //    /*
    //    // �����̉��Ɖ摜���o�� 
    //    AudioSource.PlayClipAtPoint(_clip, transform.position);
    //    */
    //    // �폜
    //    Destroy(gameObject);
    //}
}
