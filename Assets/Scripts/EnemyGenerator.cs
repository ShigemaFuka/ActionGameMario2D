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
    [SerializeField, Tooltip("����")] float _timer = 0;
    [SerializeField, Tooltip("�����ꏊ")] Transform[] _generatePoses = default;
    int _index = 0;
    int _randomPrefabsIndex = 0;
    [SerializeField, Tooltip("���̂܂Ő������Ă�����")] int _maxCount = 20;
    Transform _playerPosition = default;
    [Tooltip("���������G�L�������i�[����Queue")] static Queue<GameObject> _prefabQueue; //**
    public Queue<GameObject> PrefabQueue { get => _prefabQueue; set => _prefabQueue = value; }
    [SerializeField, Tooltip("�ŏ��̐����C���^�[�o������o�߂����Ă�������")] float _firstInterval = 3.0f;
    [SerializeField, Tooltip("�����������v���n�u")] GameObject _deathPrefab = default;
    [Tooltip("�����������S���o�p�̓G�L�������i�[����Queue")] Queue<GameObject> _deathPrefabQueue;
    public Queue<GameObject> DeathPrefabQueue { get => _deathPrefabQueue; set => _deathPrefabQueue = value; }
    [SerializeField, Tooltip("�����͈�X")] float _x = 2;
    [SerializeField, Tooltip("�����͈�Y")] float _y = 2;
    [SerializeField, Tooltip("�G�L�����̐e�I�u�W�F�N�g")] GameObject _parentEnemy = default;
    [SerializeField, Tooltip("���S���o�̓G�L�����̐e�I�u�W�F�N�g")] GameObject _parentDeathEneymy = default;

    void Awake() 
    {
        //Queue�̏�����
        PrefabQueue = new Queue<GameObject>();
        DeathPrefabQueue = new Queue<GameObject>();
        //���O�ɋK�萔�̃I�u�W�F�N�g�𐶐����āA��A�N�e�B�u�ɂ��ėp�� 
        for (int i = 0; i < _maxCount; i++)
        {
            _randomPrefabsIndex = Random.Range(0, _prefabs.Length);
            GameObject go = Instantiate(_prefabs[_randomPrefabsIndex], _parentEnemy.transform);
            //Queue�ɒǉ� 
            PrefabQueue.Enqueue(go);
            GameObject go2 = Instantiate(_deathPrefab, _parentDeathEneymy.transform);
            DeathPrefabQueue.Enqueue(go2);
        }
    }
    /// <summary>
    /// Null�łȂ����queue������o��
    /// </summary>
    /// <param name="queue">PrefabQueue���ADeathPrefabQueue</param>
    /// <param name="pos">���o�����Ƃ��ɔz�u����|�W�V����</param>
    /// <returns>���o�����I�u�W�F�N�g��Null</returns>
    public GameObject Launch(Queue<GameObject> queue, Vector3 pos)
    {
        //Queue����Ȃ�null
        if (queue.Count <= 0) return null;

        //Queue����I�u�W�F�N�g������o��
        GameObject go = queue.Dequeue();
        //�I�u�W�F�N�g��\������
        go.gameObject.SetActive(true);
        go.transform.position = pos;
        //�Ăяo�����ɓn��
        return go;
    }
    void Start()
    {
        //CharaCount = 0;
        _timer = _intervalTime - _firstInterval;
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //Debug.Log(CharaCount);
        _timer += Time.deltaTime;
        if (_timer > _intervalTime)
        {
            FindNearGenerator();
            GeneratePrefabs();
        }
        if (PrefabQueue.Count > _maxCount - 5) IntervalController();
        //Debug.Log("PrefabQueue : " + PrefabQueue.Count);
    }
    /// <summary>
    /// �ő�20�̂܂Ő��� 
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
    WaitForSeconds _wfs = new WaitForSeconds(0.5f);
    IEnumerator CoroutineInstantiate()
    {
        for (var i = 0; i < /*(_maxCount - CharaCount)*/ 5; i++)
        {
            float x = Random.Range(-_x, _x);
            float y = Random.Range(-_y, _y);
            Vector2 pos = _generatePoses[_index].position;
            pos.x += x;
            pos.y += y;
            GameObject go = Launch(PrefabQueue, pos);
            yield return _wfs;
        }
        _timer = 0;
    }
    /// <summary>
    /// queue�֊i�[����
    /// �i�[���ɔ�A�N�e�B�u�ɂ���
    /// </summary>
    /// <param name="queue">PrefabQueue���ADeathPrefabQueue</param>
    /// <param name="go">�Ώۂ̃I�u�W�F�N�g</param>
    public void Collect(Queue<GameObject> queue, GameObject go)
    {
        go.gameObject.SetActive(false);
        //Queue�Ɋi�[
        queue.Enqueue(go);
    }
    /// <summary>
    /// �������Ԃ𑁂����� 
    /// </summary>
    public void IntervalController()
    {
        _timer *= 1.1f;
        //Debug.Log("IntervalController"); 
    }
}