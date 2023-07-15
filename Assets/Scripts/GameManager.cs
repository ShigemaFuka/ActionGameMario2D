using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> ゲームマネージャー制限時間やスコアを管理する 
/// HPゼロでゲームオーバー、リザルトシーンへは行かない
/// 時間いっぱいまで生き残ればクリア扱いとなり、必ずリザルトシーンへ行く
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("現在のスコア")] static int _score = 0;
    public int Score { get => _score; set => _score = value; }
    [SerializeField, Tooltip("制限時間(初期設定)")] float _initialLimit = 0f;
    [SerializeField, Tooltip("現在の残り時間")] float _nowTime = 0f;
    [SerializeField, Tooltip("時間テキスト")] Text _timerText = default;
    [SerializeField, Tooltip("スコアテキスト")] Text _scoreText = default;
    [SerializeField, Tooltip("スコアのカンスト値")] float _maxScore = 100000;
    [Tooltip("前フレームのステート"), SerializeField] GameState _oldState = GameState.InGame;
    [SerializeField, Tooltip("現在のキル数")] static int _killCount = 0; 
    public int KillCount { get => _killCount; set => _killCount = value; }
    [SerializeField, Tooltip("残りのHP")] static int _remainingHp = 0; 
    public int RemainingHp { get => _remainingHp; set => _remainingHp = value; } 
    PlayerHp _playerHp = default;
    [SerializeField] FadeOut _fadeOut; 

    /// <summary> ゲームの状態を管理する列挙型 </summary>
    public enum GameState
    {
        InGame, //ゲーム中
        GameOver,   //ゲームオーバー
        None,
        Result,
    }

    [SerializeField] GameState _nowState = GameState.InGame;
    /// <summary> GameStateのプロパティ </summary>
    public GameState NowState { get => _nowState; set => _nowState = value; }

    void Start()
    {
        // デバッグ時メインシーンから開始してもいいように  
        if (_nowState == GameState.InGame)
        {
            // シーンに1つだけ存在する 
            _playerHp = FindAnyObjectByType<PlayerHp>();
            Reset();
            _timerText = GameObject.Find("CurrentTimer").GetComponent<Text>();
            _scoreText = GameObject.Find("CurrentScore").GetComponent<Text>(); 
        }
    }

    void Update()
    {
        // ゲーム中 → ゲームオーバー 遷移した瞬間 
        if (_nowState == GameState.GameOver && _oldState == GameState.InGame)
        {
            //SceneManager.LoadScene("GameOverScene"); 
            //_fadeOut.ToFadeOut("GameOverScene"); 
        }
        // 現在のステートと実行中のシーン 
        else if (SceneManager.GetActiveScene().name == "MainScene_1")
        {
            InGame();
            if (_scoreText)
                _scoreText.text = "Score:" + _score.ToString("00000");
            Timer();
        }
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            None();
            Reset();
        }
        // ゲーム中 → クリア 遷移した瞬間 
        if (_nowState == GameState.Result && _oldState == GameState.InGame)
        {
            //SceneManager.LoadScene("Result");
            _fadeOut.ToFadeOut("Result"); 
        }
        // Select → ゲーム中 遷移した瞬間だけ取得 
        if (_nowState == GameState.InGame && _oldState == GameState.None)
        {
            _timerText = GameObject.Find("CurrentTimer").GetComponent<Text>();
            _scoreText = GameObject.Find("CurrentScore").GetComponent<Text>();
        }

        _oldState = _nowState;
    }

    /// <summary> スコアを加算するメソッド </summary>
    /// <param name="score"> 加算したいスコア </param>
    public void AddScore(int score)
    {
        if (_score < _maxScore)
        {
            _score += score;
        }
        _scoreText.text = "Score:" + _score.ToString("00000");
    }

    /// <summary> 残り時間を減らして表示するメソッド  </summary>
    private void Timer()
    {
        _nowTime -= Time.deltaTime;
        if(_timerText)
            _timerText.text = "Time:" + _nowTime.ToString("00000");
        if (_nowTime <= 0f)
        {
            Result();
        }
    }

    /// <summary> ゲームオーバーにするメソッド  </summary>
    public void GameOver()
    {
        _nowState = GameState.GameOver;
    }

    public void InGame()
    {
        _nowState = GameState.InGame; 
    }

    public void Result()
    {
        _nowState = GameState.Result; 
    }

    public void None()
    {
        _nowState = GameState.None; 
    }

    /// <summary> 現在の時間、スコア、キル数、残りHPを初期化 </summary>
    void Reset()
    {
        _nowTime = _initialLimit; 
        if(_timerText)
            _timerText.text = "Time:" + _nowTime.ToString("00000");
        _score = 0; 
        if(_scoreText)
            _scoreText.text = "Score:" + _score.ToString("00000"); 
        _killCount = 0;
        _remainingHp = _playerHp.PlayerCurrentHp;
        _fadeOut.ToFadeIn(); 
    }
}
