using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField, Tooltip("�e����ԑ���")] float _speed = 3f;
    [SerializeField, Tooltip("�e�̐������ԁi�b�j")] float _lifeTime = 5f;
    Rigidbody2D _rb;
    // [SerializeField, Tooltip("�}�Y��")] GameObject _muzzle;
    GameObject _enemy;
    EnemyController _enemyHpScript;
    //[SerializeField] AudioClip _clip;
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _effectPrefab; 


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        // �Q��(Attack�֐����g������)
        _enemy = GameObject.Find("Enemy");
        //_enemyHpScript = _enemy.GetComponent<EnemyHp>();

        /*
        _muzzle = GameObject.Find("Enemy/Muzzle");
        // �}�Y���̈ʒu����e�۔���
        this.transform.position = _muzzle.transform.position;
        */
        _rb.velocity = Vector2.left * _speed;

        // �������Ԃ��o�߂����玩�����g��j������
        Destroy(this.gameObject, _lifeTime);

        //_clip = GetComponent<AudioSource>().clip; 
    }

    // ���߂� 
    void OnTriggerEnter2D(Collider2D coll)
    {
        // �v���C���[�ɍU�����ꂽ�������
        if (coll.gameObject.tag == "Weapon")        
        {
            /*
            // �����̉��Ɖ摜���o�� 
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            */
            // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
            if (_effectPrefab)
            {
                Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
            }
            Destroy(gameObject);
        } 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
        if (_effectPrefab)
        {
            Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
        }
        /*
        // �����̉��Ɖ摜���o�� 
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        */
        // �폜
        Destroy(gameObject);
    }
}
