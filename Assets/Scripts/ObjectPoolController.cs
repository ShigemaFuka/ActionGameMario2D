using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [Tooltip("生成した弾を格納するQueue")] Queue<GameObject> _bulletQueue;
    //[Tooltip("生成した弾を格納するQueue")] static Queue<GameObject> _bulletQueue;
    //public Queue<GameObject> BulletQueue { get => _bulletQueue; set => _bulletQueue = value; }
    [SerializeField] int maxCount;

    private void Awake()
    {
        //Queueの初期化
        _bulletQueue = new Queue<GameObject>();
        //弾を生成するループ
        for (int i = 0; i < maxCount; i++)
        {
            //生成
            //BulletController tmpBullet = Instantiate(bullet, setPos, Quaternion.identity, transform);
            //Queueに追加
            //_bulletQueue.Enqueue(tmpBullet);
        }
    }
    //弾を貸し出す処理
    public GameObject UsePrefab(GameObject go)
    {
        //Queueが空ならnull
        if (_bulletQueue.Count <= 0) return null;
        //Queueから弾を一つ取り出す
        GameObject tmpBullet = _bulletQueue.Dequeue();
        //弾を表示する
        tmpBullet.gameObject.SetActive(true);
        //呼び出し元に渡す
        return tmpBullet;
    }
    //弾の回収処理
    public void Collect(GameObject _prefab)
    {
        //弾のゲームオブジェクトを非表示
        _prefab.gameObject.SetActive(false);
        //Queueに格納
        _bulletQueue.Enqueue(_prefab);
    }
}
