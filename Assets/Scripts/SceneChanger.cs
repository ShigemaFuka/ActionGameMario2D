using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("�J�ڂ������V�[����")] string _sceneName;
    [SerializeField, Tooltip("�G���^�[�����ăV�[���J�ڂ��Ă������i�N���A�V�[���ł̂ݐ^�j")] bool _isEnter;

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
