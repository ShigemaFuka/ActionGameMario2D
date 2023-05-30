using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    SceneChanger _sceneChanger;

    void Start()
    {
        GameObject _SceneChangerObject = GameObject.Find("SceneChanger");
        _sceneChanger = _SceneChangerObject.GetComponent<SceneChanger>();
        SceneChanger.ResetBools();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            // シーンをロードする処理
            SceneChanger._isClearScene = true;
            SceneChanger._isSelectScene = true;
            Debug.Log("ゴール");
        }
    }
}