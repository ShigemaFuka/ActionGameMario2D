using UnityEngine;

/// <summary>
/// ��󂪐G�ꂽUI�ɉ����ăV�[���J�ڂ���A 
/// �V�[���J�ڂ̐����UI�ɕt���Ă��� 
/// �J�ڐ�̃V�[���������Ă��o�^����UI�������
/// �ȒP�ɑJ�ڂł��� 
/// </summary>
public class ArrowController : MonoBehaviour
{
    [SerializeField, Tooltip("�V�[���J�ڂ̂��߂ɐݒu���Ă���UI")] GameObject[] _images = null; 
    int _num = 0; 
    SceneChanger _sceneChanger = null;
    [SerializeField, Tooltip("�G���^�[�������ɂ̂ݍĐ�")] AudioSource _audioSource = default;  

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
            if (_audioSource) _audioSource.PlayOneShot(_audioSource.clip);
            _sceneChanger.ChangeScene(); 
        }
    }

    //void OnTriggerStay2D(Collider2D col)
    //{
    //    _sceneChanger = col.gameObject.GetComponent<SceneChanger>();  
    //}
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(!_sceneChanger) _sceneChanger = coll.gameObject.GetComponent<SceneChanger>();
    }

    void MoveArrow()
    {
        _sceneChanger = null;
        // obj��X�|�W���擾���ăN���b�N�̂��тɁA�����悤�ɂ���
        // obj�̈ʒu���ς���Ă��Bobj�̂��|�W�̂܂܁A���|�W���110���ɖ�󂪗���
        this.gameObject.transform.position = new Vector3(_images[_num].transform.position.x, _images[_num].transform.position.y - 110, this.gameObject.transform.position.z);
    }
}
