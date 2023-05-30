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
    //[SerializeField, Tooltip("�V�[���J�ڂ��s���X�N���v�g")] SceneChanger _sceneChanger;
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
        // obj��X�|�W���擾���ăN���b�N�̂��тɁA�����悤�ɂ���
        // obj�̈ʒu���ς���Ă��Bobj�̂��|�W�̂܂܁A���|�W���110���ɖ�󂪗���
        this.gameObject.transform.position = new Vector3(_images[_num].transform.position.x, _images[_num].transform.position.y - 110, this.gameObject.transform.position.z);
    }

    void ResetbBools()
    {
        _isStage_1 = false;
        _isStage_2 = false;
    }

}
