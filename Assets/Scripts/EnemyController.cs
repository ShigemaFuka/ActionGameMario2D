using UnityEngine;

/// <summary>
/// �G�L�����̍s������ 
/// �u�@Weapon�@�v�ɐڐG�����Ƃ��̂݃_���[�W���󂯂�
/// </summary>
public class EnemyController : MonoBehaviour
{
    GameManager _gameManager; 
    int _damageValue; 
    // HP
    [SerializeField, Header("���͕s�v")] int _enemyHp;
    
    // ���̑�
    SpriteRenderer _spriteRenderer;
    Animator _anim;

    [SerializeField, Tooltip("ScriptableObject�ȓG�̃p�����[�^")] CharacterDates _characterData;
    [SerializeField, Tooltip("�G�t�F�N�g")] GameObject _effectPrefab;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); 

        // �G�l�~�[��HP�̏�����
        if (_characterData)
            _enemyHp = _characterData.Maxhp;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if(_spriteRenderer) _spriteRenderer.color = Color.white;

        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameObject.transform.position.y <= -13)
            Destroy(gameObject); 
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Weapon") 
        {
            // �U���l 
            AttackValueController scr = coll.transform.root.gameObject.GetComponent<AttackValueController>();
            scr.Attack();
            _damageValue = scr._attackValue;

            // HP���炵�Ă���
            _enemyHp = _enemyHp - _damageValue;  
            if(_spriteRenderer) _spriteRenderer.color = Color.red;
            if (_anim) _anim.Play("Hit");

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
}
