using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> �Q�[���}�l�[�W���[�������Ԃ�X�R�A���Ǘ����� 
/// HP�[���ŃQ�[���I�[�o�[�A���U���g�V�[���ւ͍s���Ȃ�
/// ���Ԃ����ς��܂Ő����c��΃N���A�����ƂȂ�A�K�����U���g�V�[���֍s��
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField, Tooltip("���݂̃X�R�A")] static int _score = 0;
    public int Score { get => _score; set => value = _score; }
    [SerializeField, Tooltip("��������(�����ݒ�)")] float _initialLimit = 0f;
    [SerializeField, Tooltip("���݂̎c�莞��")] float _nowTime = 0f;
    [SerializeField, Tooltip("���ԃe�L�X�g")] Text _timerText ;
    [SerializeField, Tooltip("�X�R�A�e�L�X�g")] Text _scoreText ;
    [SerializeField, Tooltip("�X�R�A�̃J���X�g�l")] float _maxScore = 100000;
    [Tooltip("�O�t���[���̃X�e�[�g"), SerializeField] GameState _oldState = GameState.InGame;
    //[Tooltip("�N���A���̎c�莞��")] public static float _leftTime = 0f;
    //public float LeftTime { get { return _leftTime; } }
    [SerializeField, Tooltip("���݂̃L����")] static int _killCount = 0; 
    public int KillCount { get => _killCount; set => value = _killCount; }
    [SerializeField, Tooltip("�c���HP")] static int _remainingHp = 0; 
    public int RemainingHp { get => _remainingHp; set => value = _remainingHp; }

    /// <summary> �Q�[���̏�Ԃ��Ǘ�����񋓌^ </summary>
    public enum GameState
    {
        InGame, //�Q�[����
    //    Clear,  //�N���A
        GameOver,   //�Q�[���I�[�o�[
        None,
        Result,
    }

    [SerializeField] GameState _nowState = GameState.InGame;
    /// <summary> GameState�̃v���p�e�B </summary>
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
         

        // ���݂̃X�e�[�g�Ǝ��s���̃V�[�� 
        if (SceneManager.GetActiveScene().name == "MainScene_1")
        {
            InGame(); 
            Timer(); 
        }
        if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            None(); 
        }
        // �Q�[���� �� �N���A �J�ڂ����u�� 
        if (_nowState == GameState.Result && _oldState == GameState.InGame)
        {
            SceneManager.LoadScene("Result");
        }
        // �Q�[���� �� �Q�[���I�[�o�[ �J�ڂ����u�� 
        if(_nowState == GameState.GameOver && _oldState == GameState.InGame)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        // ���U���g �� �Q�[������A�Q�[���I�[�o�[ �� �Q�[�����ɑJ�ڂ�������
        // �X�R�A�⎞�Ԃ����������� 
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

    /// <summary> �X�R�A�����Z���郁�\�b�h </summary>
    /// <param name="score"> ���Z�������X�R�A </param>
    public void AddScore(int score)
    {
        if (_score < _maxScore)
        {
            _score += score;
        }
        _scoreText.text = "Score:" + _score.ToString("00000");
    }

    /// <summary> �c�莞�Ԃ����炵�ĕ\�����郁�\�b�h  </summary>
    private void Timer()
    {
        _nowTime -= Time.deltaTime;
        if(_timerText)
            _timerText.text = "Time:" + _nowTime.ToString("00000");
        if (_nowTime <= 0f)
        {
            Result();
            // _nowTime������������K�v������ 
            Reset(); 
        }
    }

    /// <summary> �Q�[���I�[�o�[�ɂ��郁�\�b�h  </summary>
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

    /// <summary> ���݂̎��ԁA�X�R�A�A�L�����A�c��HP�������� </summary>
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
