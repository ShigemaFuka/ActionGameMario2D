using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowController : MonoBehaviour
{
    // ��󂪐G�ꂽ�t�h�̃e�L�X�g�̔ԍ��ɉ����ăV�[���J�ڂ���

    // �ł������ƁF�t�h�Ɩ��̐ڐG
    // ��肽�����ƁF�e�L�X�g�̔ԍ��i������j���擾���邱��


    //[SerializeField, Tooltip("������UI")] GameObject _arrow;
    [SerializeField, Tooltip("�V�[���J�ڂ̂��߂�UI")] GameObject[] _images;
    // �e�L�X�g�̔ԍ����ς���Ă��Ή��ł���悤��---���e�L�X�g�̕�������ł����āA�u_images�v�̔z�񏇂͉e���Ȃ��B
    //[SerializeField, Header("���͕s�v"), Tooltip("Text�R���|�[�l���g�����Ă���Objs")] GameObject[] _imageChilds;
    //[SerializeField, Header("���͕s�v"), Tooltip("Texts")] string[] _imageChildTexts;
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
                // �ǁX�ǉ�
                Debug.LogWarning("�ǁX�ǉ�");
            }
        }
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        // �ǁX�y�ɂ�����
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
        // obj��X�|�W���擾���ăN���b�N�̂��тɁA�����悤�ɂ���
        this.gameObject.transform.position = _images[_num].transform.position;

    }

    void ResetbBools()
    {
        _isStage_1 = false;
        _isStage_2 = false;
    }

}
