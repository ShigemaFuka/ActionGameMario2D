using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> �Q�[���}�l�[�W���[
/// �������Ԃ�X�R�A�A�L�����A�c��HP�A�t�F�[�h�A�E�g�A(����)�V�[���J�ڂ��Ǘ����� 
/// HP�[���ŃQ�[���I�[�o�[�A���U���g�V�[���ւ͍s���Ȃ�
/// ���Ԃ����ς��܂Ő����c��΃N���A�����ƂȂ�A�K�����U���g�V�[���֍s��
/// </summary>
public class GameManager : MonoBehaviour
{
    [Tooltip("���݂̃X�R�A")] static int _score = 0;
    public int Score { get => _score; set => _score = value; }
    [SerializeField, Tooltip("��������(�����ݒ�)")] float _initialLimit = 0f;
    [SerializeField, Tooltip("���݂̎c�莞��")] float _nowTime = 0f;
    [SerializeField, Tooltip("���ԃe�L�X�g")] Text _timerText = default;
    [SerializeField, Tooltip("�X�R�A�e�L�X�g")] Text _scoreText = default;
    [SerializeField, Tooltip("�X�R�A�̃J���X�g�l")] float _maxScore = 100000;
    [Tooltip("�O�t���[���̃X�e�[�g"), SerializeField] GameState _oldState = GameState.InGame;
    [SerializeField, Tooltip("���݂̃L����")] static int _killCount = 0; 
    public int KillCount { get => _killCount; set => _killCount = value; }
    [SerializeField, Tooltip("�c���HP")] static int _remainingHp = 0; 
    public int RemainingHp { get => _remainingHp; set => _remainingHp = value; }
    PlayerHp _playerHp = default;
    [SerializeField] FadeOut _fadeOut;
    [SerializeField] GameState _nowState = GameState.Select;
    /// <summary> GameState�̃v���p�e�B </summary>
    public GameState NowState { get => _nowState; set => _nowState = value; }
    [Tooltip("���͂����v���C���[��")] static string _playerName = default;
    public string PlayerName { get => _playerName; set => _playerName = value; }
    [SerializeField] InputField _inputField = default;


    /// <summary> �Q�[���̏�Ԃ��Ǘ�����񋓌^ </summary>
    public enum GameState
    {
        InGame, //�Q�[����
        GameOver,   //�Q�[���I�[�o�[
        Select,
        Result,
    }
    void OnEnable()
    {
        if (_nowState == GameState.Select)
        {
            // ��񂵂��擾�ł��Ȃ����� 
            _inputField = GameObject.Find("InputFieldPlayerName").GetComponent<InputField>();
        }
        // �f�o�b�O�����C���V�[������J�n���Ă������悤��  
        if (_nowState == GameState.InGame)
        {
            // �V�[����1�������݂��� 
            _playerHp = FindAnyObjectByType<PlayerHp>();
            Reset();
            _timerText = GameObject.Find("CurrentTimer").GetComponent<Text>();
            _scoreText = GameObject.Find("CurrentScore").GetComponent<Text>();
        }
    }


    void Update()
    {

        // �Q�[���� �� �Q�[���I�[�o�[ �J�ڂ����u�� 
        if (_nowState == GameState.GameOver && _oldState == GameState.InGame)
        {
            _fadeOut.ToFadeOut("GameOverScene"); 
        }
        // ���݂̃X�e�[�g�Ǝ��s���̃V�[�� 
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

            if (!_inputField)
                _inputField = GameObject.Find("InputFieldPlayerName").GetComponent<InputField>();
            PlayerName = _inputField.text;
        }
        // �Q�[���� �� �N���A �J�ڂ����u�� 
        if (_nowState == GameState.Result && _oldState == GameState.InGame)
        {
            _fadeOut.ToFadeOut("Result"); 
        }
        // Select �� �Q�[���� �J�ڂ����u�Ԃ����擾 
        if (_nowState == GameState.InGame && _oldState == GameState.Select)
        {
            if(!_timerText) _timerText = GameObject.Find("CurrentTimer").GetComponent<Text>();
            if (!_scoreText) _scoreText = GameObject.Find("CurrentScore").GetComponent<Text>();
        }

        _oldState = _nowState;
    }

    /// <summary> �X�R�A�����Z���郁�\�b�h </summary>
    /// <param name="score"> ���Z�������X�R�A </param>
    public void AddScore(int score)
    {
        if (_score < _maxScore) _score += score;
        if (_scoreText) _scoreText.text = "Score:" + _score.ToString("00000");
    }

    /// <summary> �c�莞�Ԃ����炵�ĕ\�����郁�\�b�h  </summary>
    private void Timer()
    {
        _nowTime -= Time.deltaTime;
        if(_timerText)
            _timerText.text = "Time:" + _nowTime.ToString("00000");
        if (_nowTime <= 0f)
            Result();
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
        _nowState = GameState.Select; 
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
        _fadeOut.ToFadeIn();
    }
}
