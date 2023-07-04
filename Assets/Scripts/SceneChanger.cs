using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("遷移したいシーン名")] string _sceneName = null; 

    public void ChangeScene()
    {
        if (_sceneName != null)
            SceneManager.LoadScene(_sceneName);
        else
            Debug.LogWarning("シーン名を追加してください"); 
    }
}
