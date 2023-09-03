using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 結果を表示
/// </summary>
public class Result : MonoBehaviour
{
    [SerializeField, Tooltip("GM")] GameManager _gameManager; 
    [SerializeField, Tooltip("キル数 Text")] Text _killCountText;
    [SerializeField, Tooltip("スコア Text")] Text _scoreText;
    ///[SerializeField, Tooltip("残りHP Text")] Text _remainingHpText; 

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        _killCountText.text = _gameManager.KillCount.ToString("00000");
        _scoreText.text = _gameManager.Score.ToString("00000");
        //_remainingHpText.text = _gameManager.RemainingHp.ToString("00000"); 
    }

}
