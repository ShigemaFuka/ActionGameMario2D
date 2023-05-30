using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //[SerializeField, Tooltip("遷移したいシーン名")] string _sceneName;
    [SerializeField, Tooltip("エンター押してシーン遷移していいか（クリアシーンでのみ真）")] public static bool _isSelectScene;
    [SerializeField, Tooltip("エンター押してシーン遷移していいか（クリアシーンでのみ真）")] public static bool _isClearScene;
    [SerializeField, Tooltip("エンター押してシーン遷移していいか（selectシーンでのみ真）")] public static bool _isStage_1;
    [SerializeField, Tooltip("エンター押してシーン遷移していいか（selectシーンでのみ真）")] public static bool _isStage_2;
    [SerializeField, Tooltip("エンター押してシーン遷移していいか（selectシーンでのみ真）")] public static bool _isStage_3;
    [Header("シーン名入れろ")]
    [SerializeField, Tooltip("セレクトシーンの名前")] string _nameSelectScene;
    [SerializeField, Tooltip("セレクトシーンの名前")] string _nameClearScene;
    [SerializeField, Tooltip("ステージ1の名前")] string _nameStage_1;
    [SerializeField, Tooltip("ステージ2の名前")] string _nameStage_2;
    [SerializeField, Tooltip("ステージ3の名前")] string _nameStage_3;





    void Update()
    {
        if (_isClearScene)
        {
            //Debug.LogWarning("追々追加    sc : SceneChanger");
            SceneManager.LoadScene(_nameClearScene);
            // 繰り返し回避
            _isClearScene = false;
        }
        if (Input.GetKey(KeyCode.Return))
        {
            if (_isSelectScene)
            {
                SceneManager.LoadScene(_nameSelectScene);
                // 関数Test1をn秒後に実行
                Invoke("ResetBools", 0.1f);
               
            }
            else if (_isStage_1)
            {
                SceneManager.LoadScene(_nameStage_1);
                Invoke("ResetBools", 0.1f);
            }
            else if (_isStage_2)
            {
                Debug.LogWarning("追々追加    sc : SceneChanger");
                //SceneManager.LoadScene(_nameStage_2);
                Invoke("ResetBools", 0.1f);
            }
            else if (_isStage_3)
            {
                Debug.LogWarning("追々追加    sc : SceneChanger");
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
