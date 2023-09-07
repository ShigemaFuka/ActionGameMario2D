using UnityEngine;
using System.Collections;

/// <summary>
/// Vキー入力で攻撃発動 
/// Circleコライダの範囲にいた敵キャラクタを一撃必殺する
/// 当たり判定の範囲にパーティクルシステムで演出を出す
/// Kill数が貯まるごとにenabledを真にする
/// 初回10kill、2回目以降は10kill+５の倍数ごと
/// n回目：10+5（n-1）
/// </summary>
public class RangeAttackController : MonoBehaviour
{
    int _count = 1;
    Collider2D _coll = default;
    GameManager _gameManager = default;
    [SerializeField] ParticleSystem _particleSystem = default;
    bool _active = false;
    [SerializeField] Canvas _canvas = default;
    void Start()
    {
        _coll = GetComponent<Collider2D>(); 
        _coll.enabled = false;
        _gameManager = FindObjectOfType<GameManager>();
        _canvas.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _coll.enabled = true;
            _particleSystem.Play();
            _canvas.enabled = true;
            StartCoroutine(CoroutineCollect());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            OnCount(_gameManager.KillCount);
            Debug.Log(_active + "  _gameManager.KillCount : " + _gameManager.KillCount);
            if (_active)
            {
                _coll.enabled = true;
                _particleSystem.Play();
                _canvas.enabled = true;
                _count++;
                value += 10 + 5 * (_count - 1);
                StartCoroutine(CoroutineCollect());
            }
            Debug.Log("_count : " + _count);
        }
    }
    int value = 0;
    void OnCount(int kCount)
    {
        if (kCount >= value)
        {
            _active = true;
        }
        else _active = false;
        Debug.Log("value : " + value);
    }

    WaitForSeconds _wfs = new WaitForSeconds(0.1f);
    IEnumerator CoroutineCollect()
    {
        yield return _wfs;
        _coll.enabled = false;
        _canvas.enabled = false;
        _particleSystem.Stop();
    }
}
