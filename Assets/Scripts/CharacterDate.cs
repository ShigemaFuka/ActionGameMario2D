using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterDate")]

// インスペクター上に表示 
[Serializable]
public class CharacterDate : ScriptableObject
{
    [SerializeField] CharacterKind characterKind; 
    [SerializeField] int maxHp;
    public int Maxhp { get { return maxHp; } }
    [SerializeField, Tooltip("固定のATK値")] int attack;
    public int Attack { get { return attack; } } 
    [SerializeField] float speed;
    public float Speed { get { return speed; } }
    [SerializeField, Tooltip("このキャラをキルしたときのスコア")] int score;
    public float Score { get { return score; } }
    [SerializeField, Tooltip("説明文"), TextArea(1, 5)] string info; 
}

/// <summary>
/// キャラ分け 
/// </summary>
enum CharacterKind
{
    Player,
    Nomal,
    Midiam,
    Hard,
    Bullet,
}
