using UnityEngine;

/// <summary>
/// �v���C���[�̎��S���o�̂ݍs��
/// �v���n�u�ɂ��Ă����A�v���C���[��Destroy�����Ƃ��ɐ������A
/// ���S���[�V�������s��
/// </summary>
public class PlayDeath : MonoBehaviour
{
    Animator _animator = default;  
    GameManager _gameManager = default; 

    void Start()
    {
        _animator = GetComponent<Animator>(); 
        _gameManager = FindAnyObjectByType<GameManager>(); 
        _animator.Play("Death"); 
        Destroy(gameObject, 1.5f); 
    }

    /// <summary> �A�j���[�V�������̍Ō�̕��ŁA�C�x���g�g���K�[�Ŏg�p </summary>
    void AfterDeath()
    {
        _gameManager.GameOver(); 
    }
}
