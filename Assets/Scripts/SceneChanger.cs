using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Tooltip("�J�ڂ������V�[����")] string _sceneName = null; 

    public void ChangeScene()
    {
        if (_sceneName != null)
            SceneManager.LoadScene(_sceneName);
        else
            Debug.LogWarning("�V�[������ǉ����Ă�������"); 
    }
}
