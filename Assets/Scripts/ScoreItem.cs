using UnityEngine;
using System.Collections;

/// <summary>
/// UŒ‚‚·‚é‚½‚Ñ‚ÉƒXƒRƒA‚ª‰ÁZ‚³‚ê‚é
/// </summary>
public class ScoreItem : MonoBehaviour
{
    [SerializeField, Tooltip("ƒXƒRƒA")] int _starScore = 100;
    [SerializeField] GameManager _gameManager = default;
    [SerializeField, Tooltip("ÚG‚ÉSEÄ¶")] AudioSource _audioSource = default;
    [SerializeField, Tooltip("ÚG‚ÉÄ¶")] GameObject _particleSystem = default;
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
