using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのHPを制御
/// 「　Bullet　」に接触したときのみダメージを受ける 
/// </summary>
public class PlayerHp : MonoBehaviour
{
    // HP
    [SerializeField, Header("入力不要")] int _playerHp; 
    [SerializeField, Tooltip("プレイヤーが受けるダメージ")] int _damageValue;

    // その他
    [SerializeField, Tooltip("ScriptableObjectな敵のパラメータ")] CharacterDate characterDate;
    [SerializeField, Tooltip("タイプを選択")] CharacterType characterType;


    void Start()
    {
        // プレイヤーのHPの初期化
        if (characterType == CharacterType.Player)
            _playerHp = characterDate.achievementList[0].Maxhp;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")        
        {
            AttackController scr = coll.gameObject.GetComponent<AttackController>(); 
            scr.Attack(); 
            _damageValue = scr._attackValue; 

            // HP減らしていく
            _playerHp = _playerHp - _damageValue;
            
            if (_playerHp < _damageValue)
            {
                //Destroy(gameObject);

                Debug.LogWarning("プレイヤー死んだよ"); 
            }
            Debug.Log(_playerHp);
        }
    }
}