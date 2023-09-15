using UnityEngine;
using System.Collections;

/// <summary>
/// �U�����邽�тɃX�R�A�����Z�����
/// </summary>
public class ScoreItem : MonoBehaviour
{
    [SerializeField, Tooltip("�X�R�A")] int _starScore = 100;
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("�ڐG����SE�Đ�")] AudioSource _audioSource = default;
    [SerializeField, Tooltip("�ڐG���ɍĐ�")] GameObject _particleSystem = default;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!_gameManager) _gameManager = FindAnyObjectByType<GameManager>(); 
        if (coll.gameObject.CompareTag("Weapon"))
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
