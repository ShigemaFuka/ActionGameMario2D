using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowController : MonoBehaviour
{
    // 矢印が触れたＵＩのテキストの番号に応じてシーン遷移する

    // できたこと：ＵＩと矢印の接触
    // 作りたいこと：テキストの番号（文字列）を取得すること


    //[SerializeField, Tooltip("動かすUI")] GameObject _arrow;
    [SerializeField, Tooltip("シーン遷移のためのUI")] GameObject[] _images;
    //[SerializeField, Tooltip("シーン遷移を行うスクリプト")] SceneChanger _sceneChanger;
    [SerializeField] bool _isStage_1;
    [SerializeField] bool _isStage_2;
    int _num;



    void Start()
    {
        ResetbBools();
        SceneChanger.ResetBools();
        _num = 0;
        this.gameObject.transform.position = new Vector3(_images[0].transform.position.x, _images[0].transform.position.y - 110, gameObject.transform.position.z);
    }

    void Update()
    {
        if (_num < _images.Length -1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _num = _num + 1;
                MoveArrow();
            }
        }
        if(_num >= 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _num = _num - 1;
                MoveArrow();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_isStage_1)
            {
                SceneChanger._isStage_1 = true;
            }
            else if(_isStage_2)
            {
                // 追々追加
                Debug.LogWarning("追々追加");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // 追々楽にしたい
        if(col.gameObject.name == "1")
        {
            ResetbBools();
            _isStage_1 = true;
            //Debug.Log(1);
        }
        else if(col.gameObject.name == "2")
        {
            ResetbBools();
            _isStage_2 = true;
            //Debug.Log(2);
        }
        else if(col.gameObject.name == "3")
        {
            //Debug.Log(3);
        }
    }

    void MoveArrow()
    {
        // objのXポジを取得してクリックのたびに、ずれるようにする
        // objの位置が変わっても。objのｘポジのまま、ｙポジより110下に矢印が来る
        this.gameObject.transform.position = new Vector3(_images[_num].transform.position.x, _images[_num].transform.position.y - 110, this.gameObject.transform.position.z);
    }

    void ResetbBools()
    {
        _isStage_1 = false;
        _isStage_2 = false;
    }

}
