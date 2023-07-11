using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ３段階のコンボ攻撃のアニメーション制御 
/// 
/// Nキーを押してから、0.5秒以内にNキーの入力があれば
/// 攻撃モーション２に移行し、さらに0.5秒以内にNキーの入力があれば
/// 攻撃モーション３に移行する
/// 各モーション時、0.5秒以内に入力がなければキャンセル扱いになる 
/// </summary>
public class ComboAttackAnimation : MonoBehaviour
{
    Animator _animator; 
    [SerializeField, Tooltip("コンボ攻撃の段階")] int _count;
    [SerializeField, Tooltip("コンボ攻撃の受け付け時間")] float _time;
    //[SerializeField] bool _timeCountBool = false;
    bool _canCombo = false; 
    void Start()
    {
        _animator = GetComponent<Animator>(); 
        _count = 0;
        _time = 0;
    }

    void Update()
    {
        // 攻撃モーションに初めて入ったら ※ = コンボ開始していない  
        if(Input.GetKeyDown(KeyCode.N) && _count == 0)
        {
            _canCombo = true; 
        }
        
        if (_canCombo)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                //_timeCountBool = true; 
                _count ++; 
            }

            if (_count == 1)
            {
                // Play Anim_3
                _animator.Play("Right Hand");

                _time += Time.deltaTime;
                if (_time < 1 && _time > 0.1 && Input.GetKeyDown(KeyCode.N))
                {
                    _count++;
                }
                if (_time > 1)
                {
                    StartCoroutine("CanceleCombo");
                }
                Debug.Log("a");
            }
            else if (_count == 2)
            {
                // Play Anim_2
                _animator.Play("Left Hand");

                _time += Time.deltaTime;
                if (_time < 1 && _time > 0.1 && Input.GetKeyDown(KeyCode.N))
                {
                    _count++;
                }
                if (_time > 1)
                {
                    StartCoroutine("CanceleCombo"); 
                }
                Debug.Log("b");
            }
            else if (_count == 3)
            {
                // Play Anim_1
                _animator.Play("Both Hands");

                _time += Time.deltaTime; 
                if (_time > 1)
                {
                    StartCoroutine("CanceleCombo");
                }
                Debug.Log("c");
            }
        }
    }

    IEnumerator CanceleCombo()
    {
        _time = 0; 
        _count = 0; 
        yield return new WaitForSeconds(0.5f);
        _canCombo = false; 
    }
}
