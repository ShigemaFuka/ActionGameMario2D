using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CharacterDate")]

// �C���X�y�N�^�[��ɕ\�� 
[Serializable]

/// <summary>
/// �L�����̃f�[�^ 
/// </summary>
public class CharacterDates : ScriptableObject
{
    //[SerializeField, Header("�C���X�y�N�^��Ō������邽�߂̂���")] CharacterKind characterKind; 
    [SerializeField] int maxHp;
    public int Maxhp { get { return maxHp; } }
    [SerializeField, Tooltip("�Œ��ATK�l")] int attack;
    public int Attack { get { return attack; } } 
    [SerializeField] float speed;
    public float Speed { get { return speed; } }
    [SerializeField, Tooltip("���̃L�������L�������Ƃ��̃X�R�A")] int score;
    public int Score { get { return score; } }
    [SerializeField, Tooltip("������"), TextArea(1, 5)] string info; 
}

/// <summary>
/// �L�������� 
/// </summary>
enum CharacterKind
{
    Player,
    Nomal,
    Midiam,
    Hard,
    Bullet,
}
