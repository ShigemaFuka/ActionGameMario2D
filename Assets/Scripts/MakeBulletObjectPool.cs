using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// destroyせずに使い回して最適化する
/// 弾の方で、Start関数に書いた処理のいくつかはonEnabled関数に書き直す必要があるかもしれない 
/// </summary>
public class MakeBulletObjectPool : MonoBehaviour
{
    [Tooltip("敵キャラの弾を格納するためのQueue")] Queue<GameObject> _bulletPrefabQueue;
    public Queue<GameObject> BulletPrefabQueue { get => _bulletPrefabQueue; set => _bulletPrefabQueue = value; }
    [SerializeField, Tooltip("事前に生成しておく個数")] int _maxCount = 20;
    [SerializeField, Tooltip("生成物の親にする空オブジェクト")] Transform _transformParentBullet = default;
    [SerializeField, Tooltip("弾丸のプレハブ")] GameObject _bulletPrefab = default;
    void Awake()
    {
        MakeBullet(); 
    }

    void MakeBullet()
    {
        //Queueの初期化
        BulletPrefabQueue = new Queue<GameObject>();
        //事前に規定数のオブジェクトを生成して、非アクティブにして用意 
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject go = Instantiate(_bulletPrefab, _transformParentBullet);
            //Queueに追加 
            BulletPrefabQueue.Enqueue(go);
        }
    }

    /// <summary>
    /// Nullでなければqueueから取り出す
    /// </summary>
    /// <param name="pos">取り出したときに配置するポジション</param>
    /// <returns>取り出したオブジェクトかNull</returns>
    public GameObject Launch(Vector3 pos)
    {
        //Queueが空ならnull
        if (BulletPrefabQueue.Count <= 0) return null;

        //Queueからオブジェクトを一つ取り出す
        GameObject go = BulletPrefabQueue.Dequeue();
        //オブジェクトを表示する
        go.gameObject.SetActive(true);
        go.transform.position = pos;
        //呼び出し元に渡す
        return go;
    }

    /// <summary>
    /// queueへ格納する
    /// 格納時に非アクティブにする
    /// </summary>
    /// <param name="go">対象のオブジェクト</param>
    public void Collect(GameObject go)
    {
        go.gameObject.SetActive(false);
        //Queueに格納
        BulletPrefabQueue.Enqueue(go);
    }
}
