using UnityEngine;

/// <summary>
/// 弾丸の生成だけ
/// ３回生成したらインターバルが入る
/// 一発生成するときもインターバルが少し入る 
/// マズルを子オブジェクトにする必要がある 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BulletInstantiate_2 : MonoBehaviour
{
    [SerializeField, Tooltip("弾丸のプレハブ")] GameObject _bulletPrefab;
    [SerializeField, Tooltip("時間をカウントする")] float _timeCount;
    [SerializeField, Tooltip("発射をカウントする")] int _shotCount;
    [SerializeField, Tooltip("時間")] float _timer;
    [SerializeField, Tooltip("発射フラグ")] public bool _isShot;
    [SerializeField] GameObject _muzzle;
    //オブジェクトプール用コントローラー格納用変数宣言
    ObjectPoolController _objectPool;

    // Time.deltaTime で時間カウント、数秒毎に生成
    // そのために、エネミーにアタッチ
    // プレハブセット、削除を行う
    void OnEnable()
    {
        _timeCount = 0;
        _timer = 0;
        _shotCount = 0;
        _muzzle = transform.GetChild(0).gameObject;
        _objectPool = FindObjectOfType<ObjectPoolController>();
    }


    void Update()
    {
        // 同時にカウント二つ作動
        // ひとつは1.5秒、もうひとつは3.5秒で終わり→この差が2秒
        // 1.5秒：一定間隔の弾丸（0.5秒毎に一発 * 3）、3.5秒：発射から待機までの一セット（bool）、
        // 2秒：次の発射までのインターバル

        // 時間の管理************
        // 3.5f秒を超えたらリセット
        if (_timer >= 3.5f)
        {
            _isShot = true;
            _timer = 0;
        }
        // 発射インターバル2秒
        else if (_timer >= 2.0f)
        {
            _isShot = false;
        }
        // 3.5秒をカウント
        if (_timer < 3.5f)
        {
            _timer = _timer + Time.deltaTime;
        }
        // **********************


        if (_isShot)
        {
            // 0.5秒周期で発射
            if (_timeCount >= 0.5f)
            {
                // 時間だけだと誤差が出るため、回数制限
                if (_shotCount >= 3)
                {
                    // ３回発射されたらカウントリセットで０から再開
                    _shotCount = 0;
                }
                else if (_shotCount <= 2)
                {
                    // ３回までは弾丸を生成 
                    //if(_muzzle) Instantiate(_bulletPrefab, _muzzle.gameObject.transform);
                    if (_muzzle)
                    {
                        GameObject go = Instantiate(_bulletPrefab, _muzzle.transform.parent.gameObject.transform);
                        _objectPool.UsePrefab(go);
                    }
                    //_bulletPrefab.transform.position = _muzzle.gameObject.transform.position;
                    _bulletPrefab.transform.position = new Vector2(0, 0);
                    _shotCount++;
                }
                _timeCount = 0;
            }
            else if (_timeCount < 0.5f)
            {
                _timeCount = _timeCount + Time.deltaTime;
            }
        }
    }
}

