using UnityEngine;
using System.Collections;

/// <summary>
/// 攻撃するたびにスコアが加算される
/// </summary>
public class ScoreItem : MonoBehaviour
{
    [SerializeField, Tooltip("スコア")] int _starScore = 100;
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("接触時にSE再生")] AudioSource _audioSource = default;
    [SerializeField, Tooltip("接触時に再生")] GameObject _particleSystem = default;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!_gameManager) _gameManager = FindAnyObjectByType<GameManager>(); 
        if (coll.gameObject.CompareTag("Weapon") || coll.gameObject.CompareTag("RangeWeapon"))
        {
            _gameManager.AddScore(_starScore);
            _particleSystem.SetActive(true);
            _audioSource.Play();
            StartCoroutine(nameof(ParticleToFalse));
        }
    }
    WaitForSeconds _wfs = new WaitForSeconds(0.2f);
    IEnumerator ParticleToFalse()
    {
        yield return _wfs; 
        _particleSystem.SetActive(false);
    }
}
