using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのHPを制御
/// 「 Bullet 」に接触したときのみダメージを受ける 
/// Deathアニメーション内でGameOver関数を呼び出している 
/// </summary>
public class PlayerHp : MonoBehaviour
{
    GameManager _gameManager = default; 

    // HP
    [Tooltip("現在のプレイヤーのHP")] int _playerCurrentHp;
    public int PlayerCurrentHp { get { return _playerCurrentHp; } }

    [SerializeField, Tooltip("プレイヤーが受けるダメージ")] int _damageValue = 0;

    // その他
    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDates characterDate = null; 
    [SerializeField, Tooltip("エフェクト")] GameObject _effectPrefab = null; 
    [Tooltip("Hpのスライダー")] Slider _slider = null;
    [SerializeField, Tooltip("デバッグ用の無敵モード")] bool _isInvincible = false; 


    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        // プレイヤーのHPの初期化
        if (characterDate)
        {
            _playerCurrentHp = characterDate.Maxhp;
            _slider = GameObject.Find("HpSlider").GetComponent<Slider>(); 
            _slider.maxValue = characterDate.Maxhp; 
            _slider.value = characterDate.Maxhp;
            _gameManager.RemainingHp = _playerCurrentHp;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))       
        {
            AttackValueController scr = coll.gameObject.GetComponent<AttackValueController>(); 
            scr.Attack(); 
            _damageValue = scr._attackValue; 

            // HP減らしていく 
            if(!_isInvincible) _playerCurrentHp = _playerCurrentHp - _damageValue; 
            // 残りHPをリザルト用に記録 
            _gameManager.RemainingHp = _playerCurrentHp;
            _slider.value = _playerCurrentHp;

            if (_playerCurrentHp <= 0) 
            {
                // エフェクトとなるプレハブが設定されていたら、それを生成する 
                if (_effectPrefab)
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation);
                // プレイヤーを消したい 
                PlayerHide(); 
                Debug.LogWarning("プレイヤー死んだよ"); 
            }
            Debug.Log("_playerCurrentHp : " + _playerCurrentHp); 
        }
    }

    /// <summary>
    /// 死亡時にDeathプレイヤーを生成し、代わりにプレイヤーを非アクティブにする 
    /// SetActive(true)にしなくてもシーンロードされたときに復活する 
    /// </summary>
    void PlayerHide()
    {
        this.gameObject.SetActive(false); 
    }
}