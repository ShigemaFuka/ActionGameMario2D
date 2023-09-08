using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// V�L�[���͂ōU������ 
/// Circle�R���C�_�͈̔͂ɂ����G�L�����N�^���ꌂ�K�E����
/// �����蔻��͈̔͂Ƀp�[�e�B�N���V�X�e���ŉ��o���o��
/// Kill�������܂邲�Ƃ�enabled��^�ɂ���
/// ����10kill�A2��ڈȍ~��10kill+�T�̔{������
/// n��ځF10+5�in-1�j
/// </summary>
public class RangeAttackController : MonoBehaviour
{
    int _count = 1;
    Collider2D _coll = default;
    GameManager _gameManager = default;
    [SerializeField] ParticleSystem _particleSystem = default;
    bool _active = false;
    [SerializeField, Tooltip("�I���I�t����摜�̃L�����o�X")] Canvas _canvas = default;
    [SerializeField, Tooltip("Vkey�������邱�Ƃ�����")] Text _text = default;
    [SerializeField] Slider _slider = default;
    int _value = default;
    void Start()
    {
        _coll = GetComponent<Collider2D>(); 
        _coll.enabled = false;
        _gameManager = FindObjectOfType<GameManager>();
        _canvas.enabled = false;
        _text.enabled = false;
        _slider.value = 0;
        _value = 10;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("value : " + _value);

            //_coll.enabled = true;
            //_particleSystem.Play();
            //_canvas.enabled = true;
            //StartCoroutine(CoroutineCollect());
        }
        OnCount(_gameManager.KillCount);
        _slider.value = _gameManager.KillCount;
        if (Input.GetKeyDown(KeyCode.V))
        {
            //Debug.Log(_active + "  _gameManager.KillCount : " + _gameManager.KillCount);
            if (_active)
            {
                _coll.enabled = true;
                _particleSystem.Play();
                _canvas.enabled = true;
                _count++;
                var oldValue = _value;
                _slider.minValue = oldValue; 
                _value += 5 * (_count - 1);
                StartCoroutine(CoroutineCollect());
            }
            //Debug.Log("_count : " + _count);
        }
    }
    void OnCount(int kCount)
    {
        if (kCount >= _value)
        {
            _active = true;
            _text.enabled = true;
        }
        else 
        { 
            _active = false;
            _text.enabled = false;
        }
        _slider.maxValue = _value;
        //Debug.Log("value : " + _value);
    }

    WaitForSeconds _wfs = new WaitForSeconds(0.1f);
    IEnumerator CoroutineCollect()
    {
        yield return _wfs;
        _coll.enabled = false;
        _canvas.enabled = false;
        _particleSystem.Stop();
    }
}
