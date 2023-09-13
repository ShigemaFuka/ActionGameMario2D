using UnityEngine;

/// <summary>
/// 矢印が触れたUIに応じてシーン遷移する、 
/// シーン遷移の制御はUIに付けている 
/// 遷移先のシーンが増えても登録したUIがあれば
/// 簡単に遷移できる 
/// </summary>
public class ArrowController : MonoBehaviour
{
    [SerializeField, Tooltip("シーン遷移のために設置しているUI")] GameObject[] _images = null; 
    int _num = 0; 
    SceneChanger _sceneChanger = null;
    [SerializeField, Tooltip("エンター押下時にのみ再生")] AudioSource _audioSource = default;  

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
        // objのXポジを取得してクリックのたびに、ずれるようにする
        // objの位置が変わっても。objのｘポジのまま、ｙポジより110下に矢印が来る
        this.gameObject.transform.position = new Vector3(_images[_num].transform.position.x, _images[_num].transform.position.y - 110, this.gameObject.transform.position.z);
    }
}
