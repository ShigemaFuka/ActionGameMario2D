using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("遷移したいシーン名")] string _sceneName = null; 
    GameManager _gameManager;

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }

    public void ChangeScene()
    {
        if (_sceneName != null)
        {
            SceneManager.LoadScene(_sceneName);
            //_gameManager.NowState = GameState.InGame;
        }
        //else if(_gameManager.NowState == )
        else
            Debug.LogWarning("シーン名を追加してください"); 
    }
}
