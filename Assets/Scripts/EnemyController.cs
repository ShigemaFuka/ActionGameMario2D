using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 敵キャラの行動制御 
/// 「　Weapon　」に接触したときのみダメージを受ける
/// </summary>
public class EnemyController : MonoBehaviour
{
    GameManager _gameManager = default; 
    int _damageValue = default; 
    // HP
    [SerializeField, Header("入力不要")] int _enemyHp = default;
    
    // その他
    SpriteRenderer _spriteRenderer = default;
    Animator _anim = default;

    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDates _characterData = default;
    [SerializeField, Tooltip("エフェクト")] GameObject _effectPrefab = default;
    AttackValueController _playerAttackValueController = default;
    [SerializeField, Tooltip("ダメージ値を自身の頭上に表示するためのテキスト")] Text _damageText = default;
    [SerializeField, Tooltip("ダメージ値を表示しつづける時間")] float _showTextTime = 0.2f;
    [Tooltip("計算時間")] float _timer = 0f;
    [Tooltip("タイマー制御条件")] bool _isTimer = false; 

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); 

        // エネミーのHPの初期化
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
            // 攻撃値 
            _playerAttackValueController.Attack();
            _damageValue = _playerAttackValueController._attackValue;

            // HP減らしていく
            _enemyHp = _enemyHp - _damageValue;
            ShowTextDamage(_damageValue); 
            if (_spriteRenderer) _spriteRenderer.color = Color.red;
            if(_anim) _anim.Play("Hit");

            if (_enemyHp < _damageValue)
            {
                // エフェクトとなるプレハブが設定されていたら、それを生成する
                if (_effectPrefab)
                {
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
                }
                // スコア加算 
                _gameManager.AddScore(_characterData.Score);
                // キル数加算  
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
