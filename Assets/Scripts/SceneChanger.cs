using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //[SerializeField, Tooltip("�J�ڂ������V�[����")] string _sceneName;
    [SerializeField, Tooltip("�G���^�[�����ăV�[���J�ڂ��Ă������i�N���A�V�[���ł̂ݐ^�j")] public static bool _isSelectScene;
    [SerializeField, Tooltip("�G���^�[�����ăV�[���J�ڂ��Ă������i�N���A�V�[���ł̂ݐ^�j")] public static bool _isClearScene;
    [SerializeField, Tooltip("�G���^�[�����ăV�[���J�ڂ��Ă������iselect�V�[���ł̂ݐ^�j")] public static bool _isStage_1;
    [SerializeField, Tooltip("�G���^�[�����ăV�[���J�ڂ��Ă������iselect�V�[���ł̂ݐ^�j")] public static bool _isStage_2;
    [SerializeField, Tooltip("�G���^�[�����ăV�[���J�ڂ��Ă������iselect�V�[���ł̂ݐ^�j")] public static bool _isStage_3;
    [Header("�V�[���������")]
    [SerializeField, Tooltip("�Z���N�g�V�[���̖��O")] string _nameSelectScene;
    [SerializeField, Tooltip("�Z���N�g�V�[���̖��O")] string _nameClearScene;
    [SerializeField, Tooltip("�X�e�[�W1�̖��O")] string _nameStage_1;
    [SerializeField, Tooltip("�X�e�[�W2�̖��O")] string _nameStage_2;
    [SerializeField, Tooltip("�X�e�[�W3�̖��O")] string _nameStage_3;





    void Update()
    {
        if (_isClearScene)
        {
            //Debug.LogWarning("�ǁX�ǉ�    sc : SceneChanger");
            SceneManager.LoadScene(_nameClearScene);
            // �J��Ԃ����
            _isClearScene = false;
        }
        if (Input.GetKey(KeyCode.Return))
        {
            if (_isSelectScene)
            {
                SceneManager.LoadScene(_nameSelectScene);
                // �֐�Test1��n�b��Ɏ��s
                Invoke("ResetBools", 0.1f);
               
            }
            else if (_isStage_1)
            {
                SceneManager.LoadScene(_nameStage_1);
                Invoke("ResetBools", 0.1f);
            }
            else if (_isStage_2)
            {
                Debug.LogWarning("�ǁX�ǉ�    sc : SceneChanger");
                //SceneManager.LoadScene(_nameStage_2);
                Invoke("ResetBools", 0.1f);
            }
            else if (_isStage_3)
            {
                Debug.LogWarning("�ǁX�ǉ�    sc : SceneChanger");
                //SceneManager.LoadScene(_nameStage_3);
                Invoke("ResetBools", 0.1f);
            }
        }
    }

    public static void ResetBools()
    {
        _isSelectScene = false;
        _isClearScene = false;
        _isStage_1 = false;
        _isStage_2 = false;
        _isStage_3 = false;
    }
    
}
