using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject   //ScriptableObject���p������
{
    public string id;          //�o�^ID

    public string charName;    //�L�����N�^�[�̖��O

    public float hp;           //�̗�
    public float strength;     //�U����
    public float defence;      //�h���
    public float speed;        //�f����
}
