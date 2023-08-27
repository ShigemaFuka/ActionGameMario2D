using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �G�L�����̍s������ 
/// �u�@Weapon�@�v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂�
/// </summary>
public class EnemyController : MonoBehaviour
{
    GameManager _gameManager = default; 
    int _damageValue = default; 
    // HP
    [SerializeField, Header("���͕s�v")] int _enemyHp = default;
    
    // ���̑�
    SpriteRenderer _spriteRenderer = default;
    Animator _anim = default;

    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDates _characterData = default;
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _effectPrefab = default;
    AttackValueController _playerAttackValueController = default;
    [SerializeField, Tooltip("�_���[�W�l�����g�̓���ɕ\�����邽�߂̃e�L�X�g")] Text _damageText = default;
    [SerializeField, Tooltip("�_���[�W�l��\�����Â��鎞��")] float _showTextTime = 0.2f;
    [Tooltip("�v�Z����")] float _timer = 0f;
    [Tooltip("�^�C�}�[�������")] bool _isTimer = false; 

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); 

        // �G�l�~�[��HP�̏�����
        if (_characterData)
            _enemyHp = _characterData.Maxhp;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer) _spriteRenderer.color = Color.white;

        _anim = GetComponent<Animator>();
        GameObject go = GameObject.FindWithTag("Player");
        if(go) _playerAttackValueController = go.GetComponent<AttackValueController>();
        _timer = 0f;
        if (_damageText) _damageText.text = "";
    }

    void Update()
    {
        if(_isTimer) _timer += Time.deltaTime;  
        if (_timer >= _showTextTime)
        {
            if (_damageText) _damageText.text = ""; 
            _isTimer = false;
            _timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon") 
        {
            // �U���l 
            _playerAttackValueController.Attack();
            _damageValue = _playerAttackValueController._attackValue;

            // HP���炵�Ă���
            _enemyHp = _enemyHp - _damageValue;
            ShowTextDamage(_damageValue); 
            if (_spriteRenderer) _spriteRenderer.color = Color.red;
            if(_anim) _anim.Play("Hit");

            if (_enemyHp < _damageValue)
            {
                // �G�t�F�N�g�ƂȂ�v���n�u���ݒ肳��Ă�����A����𐶐�����
                if (_effectPrefab)
                {
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
                }
                // �X�R�A���Z 
                _gameManager.AddScore(_characterData.Score);
                // �L�������Z  
                _gameManager.KillCount += 1;
                Destroy(gameObject);
            }
            Debug.Log($"_enemyHp : {_enemyHp}");
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Weapon")
        {
            if(_spriteRenderer) _spriteRenderer.color = Color.white;
        }
    }

    void ShowTextDamage(int damageValue)
    {
        if (_damageText) _damageText.text = damageValue.ToString();
        _isTimer = true;  
    }
}
