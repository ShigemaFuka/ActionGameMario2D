using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// destroy�����Ɏg���񂵂čœK������
/// �e�̕��ŁAStart�֐��ɏ����������̂�������onEnabled�֐��ɏ��������K�v�����邩������Ȃ� 
/// </summary>
public class MakeBulletObjectPool : MonoBehaviour
{
    [Tooltip("�G�L�����̒e���i�[���邽�߂�Queue")] Queue<GameObject> _bulletPrefabQueue;
    public Queue<GameObject> BulletPrefabQueue { get => _bulletPrefabQueue; set => _bulletPrefabQueue = value; }
    [SerializeField, Tooltip("���O�ɐ������Ă�����")] int _maxCount = 20;
    [SerializeField, Tooltip("�������̐e�ɂ����I�u�W�F�N�g")] Transform _transformParentBullet = default;
    [SerializeField, Tooltip("�e�ۂ̃v���n�u")] GameObject _bulletPrefab = default;
    void Awake()
    {
        MakeBullet(); 
    }

    void MakeBullet()
    {
        //Queue�̏�����
        BulletPrefabQueue = new Queue<GameObject>();
        //���O�ɋK�萔�̃I�u�W�F�N�g�𐶐����āA��A�N�e�B�u�ɂ��ėp�� 
        for (int i = 0; i < _maxCount; i++)
        {
            GameObject go = Instantiate(_bulletPrefab, _transformParentBullet);
            //Queue�ɒǉ� 
            BulletPrefabQueue.Enqueue(go);
        }
    }

    /// <summary>
    /// Null�łȂ����queue������o��
    /// </summary>
    /// <param name="pos">���o�����Ƃ��ɔz�u����|�W�V����</param>
    /// <returns>���o�����I�u�W�F�N�g��Null</returns>
    public GameObject Launch(Vector3 pos)
    {
        //Queue����Ȃ�null
        if (BulletPrefabQueue.Count <= 0) return null;

        //Queue����I�u�W�F�N�g������o��
        GameObject go = BulletPrefabQueue.Dequeue();
        //�I�u�W�F�N�g��\������
        go.gameObject.SetActive(true);
        go.transform.position = pos;
        //�Ăяo�����ɓn��
        return go;
    }

    /// <summary>
    /// queue�֊i�[����
    /// �i�[���ɔ�A�N�e�B�u�ɂ���
    /// </summary>
    /// <param name="go">�Ώۂ̃I�u�W�F�N�g</param>
    public void Collect(GameObject go)
    {
        go.gameObject.SetActive(false);
        //Queue�Ɋi�[
        BulletPrefabQueue.Enqueue(go);
    }
}
