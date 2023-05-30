using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("遷移したいシーン名")] string _sceneName;
    [SerializeField, Tooltip("エンター押してシーン遷移していいか（クリアシーンでのみ真）")] bool _isEnter;

    void Update()
    {
        if (_isEnter)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneChange(_sceneName);
            }
        }


    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
