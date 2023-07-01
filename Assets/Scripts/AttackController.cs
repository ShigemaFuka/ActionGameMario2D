using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 接触し次第、その接触した側で参照する
/// これをアタッチするだけでいい
/// </summary>
public class AttackController : MonoBehaviour
{
    [Header("5以上を推奨")][SerializeField, Tooltip("「_attackValue」を決めるための値")] int _atVa;
    [Header("入力不要")][Tooltip("プレイヤー相手に使う攻撃値")] public int _attackValue;


    public void Attack()
    {
        // +-2で攻撃
        _attackValue = Random.Range(_atVa - 2, _atVa + 3);
        Debug.Log(this.gameObject.name + "  :  this.gameObject.name" + "      攻撃値 :  " + _attackValue);
    }
}
