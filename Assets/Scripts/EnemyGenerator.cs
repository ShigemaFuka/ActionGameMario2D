using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �G�L�����𐶐����� 
/// ��莞�Ԃ����ɐ��� 
/// �����ʒu�̃��X�g���Ńv���C���[�Ɉ�ԋ߂��W�F�l���[�^���琶������ 
/// �I�u�W�F�N�g�v�[���ŁA���O�Ɉ�萔�̓G�L�����𐶐����Ă����A�K�v����SetActive��؂�ւ��� 
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("�����������v���n�u")] GameObject[] _prefabs = default;
    [SerializeField, Tooltip("�����C���^�[�o��")] float _intervalTime = 0;
    [Tooltip("����")] float _timer = 0;
    [SerializeField, Tooltip("�����ꏊ")] Transform[] _generatePoses = default;
    int _index = 0;
    int _randomPrefabsIndex = 0;
    [SerializeField, Tooltip("���̂܂Ő������Ă�����")] int _maxCount = 20;
    [Tooltip("�̐�")] static int _count = 0;
    public int Count { get => _count; set => _count = value; }
    Transform _playerPosition = default;

    [Tooltip("���������e���i�[����Queue")] Queue<GameObject> _prefabQueue; //**
    [SerializeField, Tooltip("�ŏ��̐����C���^�[�o��")] float _firstInterval = 3.0f; 

    void Awake() //**
    {
        //Queue�̏�����
        _prefabQueue = new Queue<GameObject>();
        for (int i = 0; i < _maxCount; i++)
        {
            _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
            GameObject go = Instantiate(_prefabs[_randomPrefabsIndex], _generatePoses[_index].position, Quaternion.identity, gameObject.transform);
            //Queue�ɒǉ� 
            _prefabQueue.Enqueue(go);
        }
    }
    public GameObject Launch(Vector3 _pos)
    {
        //Queue����Ȃ�null
        if (_prefabQueue.Count <= 0) return null;

        //Queue����e������o��
        GameObject go = _prefabQueue.Dequeue();
        //�e��\������
        go.gameObject.SetActive(true);
        go.transform.position = _pos;
        //�Ăяo�����ɓn��
        return go;
    }

    void Start()
    {
        _timer = _firstInterval;
        Count = 0;
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _intervalTime)
        {
            FindNearGenerator();
            GeneratePrefabs();
            _timer = 0;
        }
    }
    /// <summary>
    /// 20�̂܂Ő��� 
    /// </summary>
    void GeneratePrefabs()
    {
        _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
        StartCoroutine(CoroutineInstantiate());
    }
    void FindNearGenerator()
    {
        float closeDist = 200;
        for(var i = 0; i < _generatePoses.Length; i++)
        {
            // �v���C���[�ƃW�F�l���[�^�܂ł̋������v��
            float tDist = Vector3.Distance(_playerPosition.transform.position, _generatePoses[i].transform.position);
            // �������u�����l�v�����u�v�������W�F�l���[�^�܂ł̋����v�̕����߂��Ȃ�΁A
            if (closeDist > tDist)
            {
                //�ucloseDist�v���utDist�i���̓G�܂ł̋����j�v�ɒu��������
                closeDist = tDist;
                // ��ԋ߂��W�F�l���[�^�̏���ϐ��Ɋi�[����
                _index = i;
            }
        }
    }
    WaitForSeconds _wfs = new WaitForSeconds(0.2f);
    IEnumerator CoroutineInstantiate()
    {
        for (var i = 0; i < (_maxCount - Count); i++)
        {
            //Instantiate(_prefabs[_randomPrefabsIndex], _generatePoses[_index].position, Quaternion.identity, gameObject.transform);
            Launch(_generatePoses[_index].position);  //**
            Count++;
            yield return _wfs;
        }
    }
    public void Collect(GameObject go) //**
    {
        // �Q�[���I�u�W�F�N�g���\�� 
        go.gameObject.SetActive(false);
        //Queue�Ɋi�[
        _prefabQueue.Enqueue(go);
        Count--; 
    }
}