using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 矢印が触れたUIに応じてシーン遷移する、 
/// シーン遷移の制御はUIに付けている 
/// </summary>
public class ArrowController : MonoBehaviour
{
    [SerializeField, Tooltip("シーン遷移のために設置しているUI")] GameObject[] _images = null; 
    int _num = 0; 
    SceneChanger _sceneChanger = null; 

    void Start()
    {
        _num = 0;
        this.gameObject.transform.position = new Vector3(_images[0].transform.position.x, _images[0].transform.position.y - 110, gameObject.transform.position.z);
    }

    void Update()
    {
        if (_num < _images.Length -1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                _num = _num + 1;
                MoveArrow(); 
            }
        }
        if(_num >= 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                _num = _num - 1;
                MoveArrow(); 
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _sceneChanger.ChangeScene(); 
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        _sceneChanger = col.gameObject.GetComponent<SceneChanger>();  
    }

    void MoveArrow()
    {
        // objのXポジを取得してクリックのたびに、ずれるようにする
        // objの位置が変わっても。objのｘポジのまま、ｙポジより110下に矢印が来る
        this.gameObject.transform.position = new Vector3(_images[_num].transform.position.x, _images[_num].transform.position.y - 110, this.gameObject.transform.position.z);
    }
}
