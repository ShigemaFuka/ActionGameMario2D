using UnityEngine;

/// <summary>
/// 敵キャラの行動制御 
/// 「　Weapon　」に接触したときのみダメージを受ける
/// </summary>
public class EnemyController : MonoBehaviour
{
    GameManager _gameManager; 
    int _damageValue; 
    // HP
    [SerializeField, Header("入力不要")] int _enemyHp;
    
    // その他
    SpriteRenderer _spriteRenderer;
    Animator _anim;

    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDates _characterData;
    [SerializeField, Tooltip("エフェクト")] GameObject _effectPrefab;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>(); 

        // エネミーのHPの初期化
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
            // 攻撃値 
            AttackValueController scr = coll.transform.root.gameObject.GetComponent<AttackValueController>();
            scr.Attack();
            _damageValue = scr._attackValue;

            // HP減らしていく
            _enemyHp = _enemyHp - _damageValue;  
            if(_spriteRenderer) _spriteRenderer.color = Color.red;
            if (_anim) _anim.Play("Hit");

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
}
