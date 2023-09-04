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
    [SerializeField, Header("���͕s�v")] int _enemyHp = default;
    SpriteRenderer _spriteRenderer = default;
    Animator _anim = default;
    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDates _characterData = default;
    AttackValueController _playerAttackValueController = default;
    [SerializeField, Tooltip("�_���[�W�l�����g�̓���ɕ\�����邽�߂̃e�L�X�g")] Text _damageText = default;
    [SerializeField, Tooltip("�_���[�W�l��\�����Â��鎞��")] float _showTextTime = 0.2f;
    [Tooltip("�v�Z����")] float _timer = 0f;
    [Tooltip("�^�C�}�[�������")] bool _isTimer = false;
    EnemyGenerator _enemyGenerator = default;
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
        _enemyGenerator = FindAnyObjectByType<EnemyGenerator>();
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        if (_spriteRenderer) _spriteRenderer.color = Color.white;
        _enemyHp = _characterData.Maxhp;
        _timer = 0f;
        _damageText.text = "";
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
        if (coll.gameObject.CompareTag("Weapon")) 
        {
            // �U���l 
            _playerAttackValueController.Attack();
            _damageValue = _playerAttackValueController._attackValue;

            // HP���炵�Ă���
            _enemyHp = _enemyHp - _damageValue;
            ShowTextDamage(_damageValue); 
            if (_spriteRenderer) _spriteRenderer.color = Color.red;
            if(isActiveAndEnabled) _anim.Play("Hit");

            if (_enemyHp < _damageValue)
            {
                // �X�R�A���Z 
                _gameManager.AddScore(_characterData.Score);
                // �L�������Z  
                _gameManager.KillCount += 1;
                _enemyGenerator.Count -= 1;
                GameObject go = _enemyGenerator.Launch(_enemyGenerator.DeathPrefabQueue, this.gameObject.transform.position);
                _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, this.gameObject);
            }
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

    void OnBecameInvisible() 
    {
        _enemyGenerator.Collect(_enemyGenerator.PrefabQueue, this.gameObject);
    }
}
