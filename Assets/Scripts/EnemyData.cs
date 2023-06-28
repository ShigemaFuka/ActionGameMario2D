using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject   //ScriptableObject‚ğŒp³‚·‚é
{
    public string id;          //“o˜^ID

    public string charName;    //ƒLƒƒƒ‰ƒNƒ^[‚Ì–¼‘O

    public float hp;           //‘Ì—Í
    public float strength;     //UŒ‚—Í
    public float defence;      //–hŒä—Í
    public float speed;        //‘f‘‚³
}
