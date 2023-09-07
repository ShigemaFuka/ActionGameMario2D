using UnityEngine;
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
    [SerializeField] Canvas _canvas = default;
    void Start()
    {
        _coll = GetComponent<Collider2D>(); 
        _coll.enabled = false;
        _gameManager = FindObjectOfType<GameManager>();
        _canvas.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _coll.enabled = true;
            _particleSystem.Play();
            _canvas.enabled = true;
            StartCoroutine(CoroutineCollect());
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            OnCount(_gameManager.KillCount);
            Debug.Log(_active + "  _gameManager.KillCount : " + _gameManager.KillCount);
            if (_active)
            {
                _coll.enabled = true;
                _particleSystem.Play();
                _canvas.enabled = true;
                _count++;
                value += 10 + 5 * (_count - 1);
                StartCoroutine(CoroutineCollect());
            }
            Debug.Log("_count : " + _count);
        }
    }
    int value = 0;
    void OnCount(int kCount)
    {
        if (kCount >= value)
        {
            _active = true;
        }
        else _active = false;
        Debug.Log("value : " + value);
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
