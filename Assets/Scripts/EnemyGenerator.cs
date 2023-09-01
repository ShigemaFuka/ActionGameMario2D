using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    [Tooltip("生成した弾を格納するQueue")] Queue<GameObject> _prefabQueue; //**
    [SerializeField, Tooltip("最初の生成インターバル")] float _firstInterval = 3.0f; 

    void Awake() //**
    {
        //Queueの初期化
        _prefabQueue = new Queue<GameObject>();
        for (int i = 0; i < _maxCount; i++)
        {
            _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
            GameObject go = Instantiate(_prefabs[_randomPrefabsIndex], _generatePoses[_index].position, Quaternion.identity, gameObject.transform);
            //Queueに追加 
            _prefabQueue.Enqueue(go);
        }
    }
    public GameObject Launch(Vector3 _pos)
    {
        //Queueが空ならnull
        if (_prefabQueue.Count <= 0) return null;

        //Queueから弾を一つ取り出す
        GameObject go = _prefabQueue.Dequeue();
        //弾を表示する
        go.gameObject.SetActive(true);
        go.transform.position = _pos;
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
    WaitForSeconds _wfs = new WaitForSeconds(0.2f);
    IEnumerator CoroutineInstantiate()
    {
        for (var i = 0; i < (_maxCount - Count); i++)
        {
            //Instantiate(_prefabs[_randomPrefabsIndex], _generatePoses[_index].position, Quaternion.identity, gameObject.transform);
            Launch(_generatePoses[_index].position);  //**
            Count++;
            yield return _wfs;
        }
    }
    public void Collect(GameObject go) //**
    {
        // ゲームオブジェクトを非表示 
        go.gameObject.SetActive(false);
        //Queueに格納
        _prefabQueue.Enqueue(go);
        Count--; 
    }
}