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
    public int Score { get => _score; set => value = _score; }
    [SerializeField, Tooltip("制限時間(初期設定)")] float _initialLimit = 0f;
    [SerializeField, Tooltip("現在の残り時間")] float _nowTime = 0f;
    [SerializeField, Tooltip("時間テキスト")] Text _timerText ;
    [SerializeField, Tooltip("スコアテキスト")] Text _scoreText ;
    [SerializeField, Tooltip("スコアのカンスト値")] float _maxScore = 100000;
    [Tooltip("前フレームのステート"), SerializeField] GameState _oldState = GameState.InGame;
    //[Tooltip("クリア時の残り時間")] public static float _leftTime = 0f;
    //public float LeftTime { get { return _leftTime; } }
    [SerializeField, Tooltip("現在のキル数")] static int _killCount = 0; 
    public int KillCount { get => _killCount; set => value = _killCount; }
    [SerializeField, Tooltip("残りのHP")] static int _remainingHp = 0; 
    public int RemainingHp { get => _remainingHp; set => value = _remainingHp; }

    /// <summary> ゲームの状態を管理する列挙型 </summary>
    public enum GameState
    {
        InGame, //ゲーム中
    //    Clear,  //クリア
        GameOver,   //ゲームオーバー
        None,
        Result,
    }

    [SerializeField] GameState _nowState = GameState.InGame;
    /// <summary> GameStateのプロパティ </summary>
    public GameState NowState { get => _nowState; set => _nowState = value; }

    void Awake()
    {
        //_nowState = GameState.InGame;
        //_leftTime = _initialLimit;
        //if (SceneManager.GetActiveScene().name == "MainScene_1")
        //{
        //    //_nowState = GameState.InGame;
        //    _timerText = GameObject.Find("CurrentTimer").GetComponent<Text>();
        //    _scoreText = GameObject.Find("CurrentScore").GetComponent<Text>();
        //}
        _nowTime = _initialLimit;
        _score = 0;
    }
    
    void Update()
    {
         

        // 現在のステートと実行中のシーン 
        if (SceneManager.GetActiveScene().name == "MainScene_1")
        {
            InGame(); 
            Timer(); 
        }
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            None(); 
        }
        // ゲーム中 → クリア 遷移した瞬間 
        if (_nowState == GameState.Result && _oldState == GameState.InGame)
        {
            SceneManager.LoadScene("Result");
        }
        // ゲーム中 → ゲームオーバー 遷移した瞬間 
        if(_nowState == GameState.GameOver && _oldState == GameState.InGame)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        // リザルト → ゲーム中や、ゲームオーバー → ゲーム中に遷移したあと
        // スコアや時間を初期化する 
        if(_nowState == GameState.InGame && ( _oldState == GameState.GameOver || _oldState == GameState.Result || _oldState == GameState.None ) )
        {
            Reset(); 
            _timerText = GameObject.Find("CurrentTimer").GetComponent<Text>(); 
            _scoreText = GameObject.Find("CurrentScore").GetComponent<Text>(); 
            if (_scoreText)
                _scoreText.text = "Score:" + _score.ToString("00000");
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
            // _nowTimeを初期化する必要がある 
            Reset(); 
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
        _remainingHp = 0; 
    }
}
