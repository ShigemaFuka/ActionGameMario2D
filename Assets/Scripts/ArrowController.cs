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
    // テキストの番号が変わっても対応できるように---＞テキストの文字次第であって、「_images」の配列順は影響なし。
    //[SerializeField, Header("入力不要"), Tooltip("TextコンポーネントがついているObjs")] GameObject[] _imageChilds;
    //[SerializeField, Header("入力不要"), Tooltip("Texts")] string[] _imageChildTexts;
    [SerializeField] SceneChanger _sceneChanger;
    [SerializeField] bool _isStage_1;
    [SerializeField] bool _isStage_2;
    int _num;



    void Start()
    {
        _isStage_1 = false;
        _num = 0;
    }

    void Update()
    {
        //Debug.Log(_images.Length);

        if (_num < _images.Length)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _num = _num + 1;
                Debug.Log(_num);

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _num = _num - 1;
                Debug.Log(_num);
            }
        }
        /*
        else
        {
            _num = 0;
            Debug.Log(_num);
        }
        */

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_isStage_1)
            {
                _sceneChanger.SceneChange("MainScene_1");
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
            // _sceneChanger.SceneChange("MainScene_1");
            ResetbBools();
            _isStage_1 = true;
            Debug.Log(1);
        }
        else if(col.gameObject.name == "2")
        {
            ResetbBools();
            _isStage_2 = true;
            Debug.Log(2);
        }
        else if(col.gameObject.name == "3")
        {
            Debug.Log(3);
        }
    }

    void MoveArrow()
    {
        // objのXポジを取得してクリックのたびに、ずれるようにする
        this.gameObject.transform.position = _images[_num].transform.position;

    }

    void ResetbBools()
    {
        _isStage_1 = false;
        _isStage_2 = false;
    }

}
