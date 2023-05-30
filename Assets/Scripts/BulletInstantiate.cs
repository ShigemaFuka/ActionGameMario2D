using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletInstantiate : MonoBehaviour
{
    
    [SerializeField, Tooltip("弾丸のプレハブ")] GameObject _bulletPrefab;
    GameObject _bullet;
    [SerializeField, Tooltip("時間をカウントする")] float _timeCount;
    [SerializeField, Tooltip("発射をカウントする")] int _shotCount;
    [SerializeField, Tooltip("時間")] float _timer;
    [SerializeField, Tooltip("発射フラグ")] bool _isShot;


    // Time.deltaTime で時間カウント、数秒毎に生成
    // そのために、エネミーにアタッチ
    // プレハブセット、削除を行う

    void Start()
    {
        _timeCount = 0;
        _timer = 0;       
        _isShot = true;
        _shotCount = 0;
    }

    void Update()
    {
        // 同時にカウント二つ作動
        // ひとつは1.5秒、もうひとつは3.5秒で終わり→この差が2秒
        // 1.5秒：一定間隔の弾丸（0.5秒毎に一発 * 3）、3.5秒：発射から待機までの一セット（bool）、
        // 2秒：次の発射までのインターバル

        // 時間の管理************
        // 3.5f秒を超えたらリセット
        if(_timer >= 3.5f)
        {
            //Debug.Log(_timer);
            _isShot = true;
            _timer = 0;
        }
        // 発射インターバル2秒
        else if (_timer >= 2.0f)
        {
            _isShot = false;
        }
        // 3.5秒をカウント
        if(_timer < 3.5f)
        {
            _timer = _timer + Time.deltaTime;
        }
        // **********************

        
        if(_isShot)
        {
            // 0.5秒周期で発射
            if(_timeCount >= 0.5f)
            {
                // 時間だけだと誤差が出るため、回数制限
                if(_shotCount >= 3)
                {
                    // ３回発射されたらカウントリセットで０から再開
                    _shotCount = 0;
                }
                else if (_shotCount <= 2)
                {
                    // ３回までは弾丸を生成
                    Instantiate(_bulletPrefab);
                    //Debug.Log("_timeCount:   " + _timeCount);
                    _shotCount++;
                }
                _timeCount = 0;
            }
            else if(_timeCount < 0.5f)
            {
                _timeCount = _timeCount + Time.deltaTime;
            }
        }

    }
}

