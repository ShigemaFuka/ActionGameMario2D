using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 敵キャラを生成する 
/// 一定時間おきに生成 
/// 生成位置のリスト内でプレイヤーに一番近いジェネレータから生成する 
/// オブジェクトプールで、事前に一定数の敵キャラを生成しておき、必要時にSetActiveを切り替える 
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("生成したいプレハブ")] GameObject[] _prefabs = default;
    [SerializeField, Tooltip("生成インターバル")] float _intervalTime = 0;
    [Tooltip("時間")] float _timer = 0;
    [SerializeField, Tooltip("生成場所")] Transform[] _generatePoses = default;
    int _index = 0;
    int _randomPrefabsIndex = 0;
    [SerializeField, Tooltip("何体まで生成しておくか")] int _maxCount = 20;
    [Tooltip("個体数")] static int _count = 0;
    public int Count { get => _count; set => _count = value; }
    Transform _playerPosition = default;
    [Tooltip("生成した敵キャラを格納するQueue")] static Queue<GameObject> _prefabQueue; //**
    public Queue<GameObject> PrefabQueue { get => _prefabQueue; set => _prefabQueue = value; }
    [SerializeField, Tooltip("最初の生成インターバル")] float _firstInterval = 3.0f;
    [SerializeField, Tooltip("生成したいプレハブ")] GameObject _deathPrefab = default;
    [Tooltip("生成した死亡演出用の敵キャラを格納するQueue")] Queue<GameObject> _deathPrefabQueue;
    public Queue<GameObject> DeathPrefabQueue { get => _deathPrefabQueue; set => _deathPrefabQueue = value; }
    [SerializeField, Tooltip("生成範囲X")] float _x = 2;
    [SerializeField, Tooltip("生成範囲Y")] float _y = 2;

    void Awake() 
    {
        //Queueの初期化
        PrefabQueue = new Queue<GameObject>();
        DeathPrefabQueue = new Queue<GameObject>();
        //事前に規定数のオブジェクトを生成して、非アクティブにして用意 
        for (int i = 0; i < _maxCount; i++)
        {
            _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
            GameObject go = Instantiate(_prefabs[_randomPrefabsIndex], gameObject.transform);
            //Queueに追加 
            PrefabQueue.Enqueue(go);
            GameObject go2 = Instantiate(_deathPrefab, gameObject.transform);
            DeathPrefabQueue.Enqueue(go2);
        }
    }
    /// <summary>
    /// Nullでなければqueueから取り出す
    /// </summary>
    /// <param name="queue">PrefabQueueか、DeathPrefabQueue</param>
    /// <param name="pos">取り出したときに配置するポジション</param>
    /// <returns>取り出したオブジェクトかNull</returns>
    public GameObject Launch(Queue<GameObject> queue, Vector3 pos)
    {
        //Queueが空ならnull
        if (queue.Count <= 0) return null;

        //Queueからオブジェクトを一つ取り出す
        GameObject go = queue.Dequeue();
        //オブジェクトを表示する
        go.gameObject.SetActive(true);
        go.transform.position = pos;
        //呼び出し元に渡す
        return go;
    }
    void Start()
    {
        _timer = _firstInterval;
        Count = 0;
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _intervalTime)
        {
            FindNearGenerator();
            GeneratePrefabs();
            _timer = 0;
        }
    }
    /// <summary>
    /// 20体まで生成 
    /// </summary>
    void GeneratePrefabs()
    {
        _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
        StartCoroutine(CoroutineInstantiate());
    }
    void FindNearGenerator()
    {
        float closeDist = 200;
        for(var i = 0; i < _generatePoses.Length; i++)
        {
            // プレイヤーとジェネレータまでの距離を計測
            float tDist = Vector3.Distance(_playerPosition.transform.position, _generatePoses[i].transform.position);
            // もしも「初期値」よりも「計測したジェネレータまでの距離」の方が近いならば、
            if (closeDist > tDist)
            {
                //「closeDist」を「tDist（その敵までの距離）」に置き換える
                closeDist = tDist;
                // 一番近いジェネレータの情報を変数に格納する
                _index = i;
            }
        }
    }
    WaitForSeconds _wfs = new WaitForSeconds(0.1f);
    IEnumerator CoroutineInstantiate()
    {
        for (var i = 0; i < (_maxCount - Count); i++)
        {
            float x = Random.Range(-_x, _x);
            float y = Random.Range(-_y, _y);
            Vector2 pos = _generatePoses[_index].position;
            pos.x += x;
            pos.y += y;
            Launch(PrefabQueue, pos);  
            Count++;
            yield return _wfs;
        }
    }
    /// <summary>
    /// queueへ格納する
    /// 格納時に非アクティブにする
    /// </summary>
    /// <param name="queue">PrefabQueueか、DeathPrefabQueue</param>
    /// <param name="go">対象のオブジェクト</param>
    public void Collect(Queue<GameObject> queue, GameObject go) 
    {
        go.gameObject.SetActive(false);
        //Queueに格納
        queue.Enqueue(go);
        Count--; 
    }
}