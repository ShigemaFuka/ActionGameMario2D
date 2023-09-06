using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ATK値を変動させるためのスクリプト 
/// 接触し次第、その接触した側で参照する
/// これをアタッチするだけでいい
/// </summary>
public class AttackValueController : MonoBehaviour
{
    [SerializeField, Tooltip("該当するキャラのデータ")] CharacterDates _characterDate = default;  
    int _atVa;
    [SerializeField, Tooltip("ATK値のプラマイ")] int _num = 0; 
    [Header("入力不要")][Tooltip("プレイヤー相手に使う攻撃値")] public int _attackValue = 0;

    void Start()
    {
        if(_characterDate)
            _atVa = _characterDate.Attack; 
    }

    /// <summary>
    /// ATK値を変動させる
    /// +-numの範囲で攻撃のたびに変動 
    /// </summary>
    public void Attack()
    {
        // +-2で攻撃
        _attackValue = Random.Range(_atVa - _num, _atVa + _num +1);
    }
}
