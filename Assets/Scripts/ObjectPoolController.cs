using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [Tooltip("���������e���i�[����Queue")] Queue<GameObject> _bulletQueue;
    //[Tooltip("���������e���i�[����Queue")] static Queue<GameObject> _bulletQueue;
    //public Queue<GameObject> BulletQueue { get => _bulletQueue; set => _bulletQueue = value; }
    [SerializeField] int maxCount;

    private void Awake()
    {
        //Queue�̏�����
        _bulletQueue = new Queue<GameObject>();
        //�e�𐶐����郋�[�v
        for (int i = 0; i < maxCount; i++)
        {
            //����
            //BulletController tmpBullet = Instantiate(bullet, setPos, Quaternion.identity, transform);
            //Queue�ɒǉ�
            //_bulletQueue.Enqueue(tmpBullet);
        }
    }
        void Start()
    {
        
    }

    void Update()
    {
        
    }
    //�e��݂��o������
    public GameObject UsePrefab(GameObject go)
    {
        //Queue����Ȃ�null
        if (_bulletQueue.Count <= 0) return null;
        //Queue����e������o��
        GameObject tmpBullet = _bulletQueue.Dequeue();
        //�e��\������
        tmpBullet.gameObject.SetActive(true);
        //�Ăяo�����ɓn��
        return tmpBullet;
    }
    //�e�̉������
    public void Collect(GameObject _prefab)
    {
        //�e�̃Q�[���I�u�W�F�N�g���\��
        _prefab.gameObject.SetActive(false);
        //Queue�Ɋi�[
        _bulletQueue.Enqueue(_prefab);
    }
}
