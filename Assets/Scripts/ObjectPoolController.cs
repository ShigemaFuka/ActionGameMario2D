using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [Tooltip("¶¬‚µ‚½’e‚ğŠi”[‚·‚éQueue")] Queue<GameObject> _bulletQueue;
    //[Tooltip("¶¬‚µ‚½’e‚ğŠi”[‚·‚éQueue")] static Queue<GameObject> _bulletQueue;
    //public Queue<GameObject> BulletQueue { get => _bulletQueue; set => _bulletQueue = value; }
    [SerializeField] int maxCount;

    private void Awake()
    {
        //Queue‚Ì‰Šú‰»
        _bulletQueue = new Queue<GameObject>();
        //’e‚ğ¶¬‚·‚éƒ‹[ƒv
        for (int i = 0; i < maxCount; i++)
        {
            //¶¬
            //BulletController tmpBullet = Instantiate(bullet, setPos, Quaternion.identity, transform);
            //Queue‚É’Ç‰Á
            //_bulletQueue.Enqueue(tmpBullet);
        }
    }
        void Start()
    {
        
    }

    void Update()
    {
        
    }
    //’e‚ğ‘İ‚µo‚·ˆ—
    public GameObject UsePrefab(GameObject go)
    {
        //Queue‚ª‹ó‚È‚çnull
        if (_bulletQueue.Count <= 0) return null;
        //Queue‚©‚ç’e‚ğˆê‚Âæ‚èo‚·
        GameObject tmpBullet = _bulletQueue.Dequeue();
        //’e‚ğ•\¦‚·‚é
        tmpBullet.gameObject.SetActive(true);
        //ŒÄ‚Ño‚µŒ³‚É“n‚·
        return tmpBullet;
    }
    //’e‚Ì‰ñûˆ—
    public void Collect(GameObject _prefab)
    {
        //’e‚ÌƒQ[ƒ€ƒIƒuƒWƒFƒNƒg‚ğ”ñ•\¦
        _prefab.gameObject.SetActive(false);
        //Queue‚ÉŠi”[
        _bulletQueue.Enqueue(_prefab);
    }
}
