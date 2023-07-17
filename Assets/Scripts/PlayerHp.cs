using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのHPを制御
/// 「　Bullet　」に接触したときのみダメージを受ける 
/// </summary>
public class PlayerHp : MonoBehaviour
{

    [SerializeField] GameManager _gameManager = default; 

    // HP
    [Tooltip("現在のプレイヤーのHP")] int _playerCurrentHp;
    public int PlayerCurrentHp { get { return _playerCurrentHp; } }

    [SerializeField, Tooltip("プレイヤーが受けるダメージ")] int _damageValue;

    // その他
    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDates characterDate; 
    [SerializeField, Tooltip("エフェクト")] GameObject _effectPrefab;


    void Start()
    {
        // プレイヤーのHPの初期化
        if (characterDate)
            _playerCurrentHp = characterDate.Maxhp;
        _gameManager = FindAnyObjectByType<GameManager>(); 
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")        
        {
            AttackValueController scr = coll.gameObject.GetComponent<AttackValueController>(); 
            scr.Attack(); 
            _damageValue = scr._attackValue; 

            // HP減らしていく 
            _playerCurrentHp = _playerCurrentHp - _damageValue;
            // 残りHPをリザルト用に記録 
            _gameManager.RemainingHp = _playerCurrentHp; 
            
            if (_playerCurrentHp <= 0)
            {
                // エフェクトとなるプレハブが設定されていたら、それを生成する
                if (_effectPrefab)
                    Instantiate(_effectPrefab, this.transform.position, this.transform.rotation); 
                //_gameManager.GameOver();  
                Debug.LogWarning("プレイヤー死んだよ"); 
            }
            Debug.Log(_playerCurrentHp); 
        }
    }
}