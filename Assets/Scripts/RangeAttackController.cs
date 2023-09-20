using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField, Tooltip("コライダと同じ範囲に表示")] ParticleSystem _particleSystem = default;
    [SerializeField, Tooltip("スライダが満タンになったら再生（粒子）")] ParticleSystem _sliderPSystem = default; 
    [SerializeField, Tooltip("スライダが満タンになったら再生（リング）")] ParticleSystem _sliderPSystem2 = default; 
    bool _active = false;
    [SerializeField, Tooltip("オンオフする画像のキャンバス")] Canvas _canvas = default;
    [SerializeField] Slider _slider = default;
    int _value = default;
    [SerializeField, Tooltip("ゲージ満タンのサインUI")] GameObject _gameObject = default;
    void Start()
    {
        _coll = GetComponent<Collider2D>(); 
        _coll.enabled = false;
        _gameManager = FindObjectOfType<GameManager>();
        _canvas.enabled = false;
        _gameObject.SetActive(false);
        _slider.value = 0;
        _value = 10;
        _sliderPSystem.Stop(); 
        _sliderPSystem2.Stop(); 
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _count++;
            _value += 5 * (_count - 1);

            Debug.Log("value : " + _value);

            //_coll.enabled = true;
            //_particleSystem.Play();
            //_canvas.enabled = true;
            //StartCoroutine(CoroutineCollect());
        }
        OnCount(_gameManager.KillCount);
        _slider.value = _gameManager.KillCount;
        if (Input.GetKeyDown(KeyCode.V))
        {
            //Debug.Log(_active + "  _gameManager.KillCount : " + _gameManager.KillCount);
            if (_active)
            {
                _coll.enabled = true;
                _particleSystem.Play();
                _sliderPSystem2.Play();
                _canvas.enabled = true;
                _count++;
                var oldValue = _value;
                _slider.minValue = oldValue; 
                _value += 5 * (_count - 1);
                StartCoroutine(CoroutineCollect());
            }
            //Debug.Log("_count : " + _count);
        }
    }
    void OnCount(int kCount)
    {
        if (kCount >= _value)
        {
            _active = true;
            _sliderPSystem.Play();
            _sliderPSystem2.Play();
            _gameObject.SetActive(true);
        }
        else 
        { 
            _active = false;
            _sliderPSystem.Stop();
            _sliderPSystem2.Stop();
            _gameObject.SetActive(false);
        }
        _slider.maxValue = _value;
        //Debug.Log("_value - kCount : " + (_value - kCount) + "//  _value : " + _value);
    }

    WaitForSeconds _wfs = new WaitForSeconds(0.1f);
    IEnumerator CoroutineCollect()
    {
        yield return _wfs;
        _coll.enabled = false;
        _particleSystem.Stop();
        _canvas.enabled = false;
    }
}
