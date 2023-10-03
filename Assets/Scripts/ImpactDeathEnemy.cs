using UnityEngine;
using System.Collections;
/// <summary>
/// �G�L�����̎��S���o�p�̃I�u�W�F�N�g�𐧌䂷��
/// 3�p�^�[���̃A�j���[�V�����������_���ōĐ��A�v���C���[�ɂ���Đ�����΂����悤��
/// �͂�����������i���E�j��؂�ւ��Ă���
/// �R���[�`����1�b�قǃA�j���[�V�����Đ��̎��Ԃ�����Ă���
/// EnemyGenerator�ŃI�u�W�F�N�g�v�[�����g���A�\�ߓG�L�����Ɠ��������A
/// �K�v�ɉ����ďo�����ꂵ�Ă���
/// </summary>
public class ImpactDeathEnemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb= default;
    Vector2 _position = Vector2.zero;
    Vector2 _targetPosition = Vector2.zero;
    Vector2 _distance = Vector2.zero; 
    Vector2 _direction = new Vector2(-1, 1);
    [SerializeField] float _power = 10f;
    [SerializeField] Animator _animator = default;
    int _num = 0;
    EnemyGenerator _enemyGenerator = default;
    void OnEnable()
    {
        var randomY = Random.Range(0, 2.0f);
        _direction.y = randomY; 
        _enemyGenerator = FindAnyObjectByType<EnemyGenerator>(); 
        _position = this.transform.position;
        _targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _distance = (_targetPosition - _position).normalized;
        // Player���G���E�ɋ���Ƃ� 
        if (_distance.x != 0)
        {
            _direction.x = -_direction.x;
            Vector2 _localScale = this.transform.localScale;
            this.transform.localScale = new Vector3(-_localScale.x, this.transform.localScale.y);
        }
        var randomNum = Random.Range(0, 20);
        // 9�����isDead_1���Đ� 
        if (randomNum > 1) _num = 1;
        else _num = 2; 
        _animator.SetTrigger($"isDead_{_num}");
        _rb.AddForce(_direction * _power, ForceMode2D.Impulse);
        StartCoroutine(CoroutineCollect());
    }
    WaitForSeconds _wfs = new WaitForSeconds(1f);
    IEnumerator CoroutineCollect()
    {
        yield return _wfs;
        _enemyGenerator.Collect(_enemyGenerator.DeathPrefabQueue, this.gameObject);
    }
}
