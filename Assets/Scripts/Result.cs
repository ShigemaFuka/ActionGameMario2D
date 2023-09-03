using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ʂ�\��
/// </summary>
public class Result : MonoBehaviour
{
    [SerializeField, Tooltip("GM")] GameManager _gameManager; 
    [SerializeField, Tooltip("�L���� Text")] Text _killCountText;
    [SerializeField, Tooltip("�X�R�A Text")] Text _scoreText;
    ///[SerializeField, Tooltip("�c��HP Text")] Text _remainingHpText; 

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _killCountText.text = _gameManager.KillCount.ToString("00000");
        _scoreText.text = _gameManager.Score.ToString("00000");
        //_remainingHpText.text = _gameManager.RemainingHp.ToString("00000"); 
    }

}
