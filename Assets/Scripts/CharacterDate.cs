using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵キャラクターのパラメータをここで設定し、
/// どこからでも参照できるようにするが、
/// 読み取り専用である
/// </summary>

[CreateAssetMenu(menuName = "ScriptableObject/CharacterDate")]
public class CharacterDate : ScriptableObject   //ScriptableObjectを継承する
{
    // 複数クラス作れる 
    public List<Achievement> achievementList = new List<Achievement>(); 
}

// インスペクター上に表示 
[Serializable]
public class Achievement
{
    //[SerializeField] string charName; 
    //public string CharName { get { return charName; } }
    [SerializeField, Tooltip("ここで設定する上でのみ使用")] CharacterType characterType;
    [SerializeField] int maxHp; 
    public int Maxhp { get { return maxHp; } }
    [SerializeField] float speed; 
    public float Speed { get { return speed; } }
    //[SerializeField] int attack;
    //public float Attack { get { return attack; } }
}

/// <summary>
/// キャラの強さをタイプ分け 
/// </summary>
enum CharacterType
{
    Player,
    Nomal,
    Midiam,
    Hard,
}


