using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�L�����N�^�[�̃p�����[�^�������Őݒ肵�A
/// �ǂ�����ł��Q�Ƃł���悤�ɂ��邪�A
/// �ǂݎ���p�ł���
/// </summary>

[CreateAssetMenu(menuName = "ScriptableObject/CharacterDate")]
public class CharacterDate : ScriptableObject   //ScriptableObject���p������
{
    // �����N���X���� 
    public List<Achievement> achievementList = new List<Achievement>(); 
}

// �C���X�y�N�^�[��ɕ\�� 
[Serializable]
public class Achievement
{
    //[SerializeField] string charName; 
    //public string CharName { get { return charName; } }
    [SerializeField, Tooltip("�����Őݒ肷���ł̂ݎg�p")] CharacterType characterType;
    [SerializeField] int maxHp; 
    public int Maxhp { get { return maxHp; } }
    [SerializeField] float speed; 
    public float Speed { get { return speed; } }
    //[SerializeField] int attack;
    //public float Attack { get { return attack; } }
}

/// <summary>
/// �L�����̋������^�C�v���� 
/// </summary>
enum CharacterType
{
    Player,
    Nomal,
    Midiam,
    Hard,
}


