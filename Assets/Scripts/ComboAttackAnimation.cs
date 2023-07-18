using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ３段階のコンボ攻撃のアニメーション制御 
/// 
/// Nキーを押してから、
/// 攻撃モーション１に移行し、0.5秒以内にNキーの入力があれば
/// 攻撃モーション２に移行し、さらに0.5秒以内にNキーの入力があれば
/// 攻撃モーション３に移行する
/// 各モーション時、0.5秒以内に入力がなければキャンセル扱いになる 
/// </summary>
public class ComboAttackAnimation : MonoBehaviour
{
    Animator _animator; 
    [SerializeField, Tooltip("第2攻撃フラグ")] bool _attack2Enable;
    [SerializeField, Tooltip("第3攻撃フラグ")] bool _attack3Enable;
    [SerializeField, Tooltip("必ずAtt_1から始めるためのフラグ")] bool combocombo = true;
    [SerializeField] bool _canCombo = false;
    [SerializeField] float _time1;
    [SerializeField] float _time2;
    [SerializeField] float _time3;
    void Start()
    {
        _animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !_canCombo && !_attack2Enable && !_attack3Enable)
            _canCombo = true; 

        if (_canCombo && !_attack2Enable && combocombo)
        {
            //入力したら一定期間入力を受け付け、入力があったらコンボ２へ移行、なかったらキャンセル
            //コンボ１
            _animator.SetBool("isAtt_1", true);

            _time1 += Time.deltaTime;
            if (_time1 > 0.1 && _time1 < 1)
            {
                //入力受付期間
                if (Input.GetKeyDown(KeyCode.N))
                {
                    _time1 = 0;
                    _attack2Enable = true;
                    combocombo = false;
                    //タイマーを初期化し、コンボ１をオフにして、コンボ２をTrueにする
                }
            }
            if (_time1 > 1)
            {
                //入力されなかったときの処理
                StartCoroutine(nameof(cancombocorutine)); 
            }
        }

        if (_attack2Enable)
        {
            Debug.Log("コンボ２");
            _animator.SetBool("isAtt_1", false);
            //コンボ２
            _animator.SetBool("isAtt_2", true);
            _time2 += Time.deltaTime; 
            if (_time2 > 0.5 && _time2 < 1)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    _time2 = 0;
                    _attack3Enable = true;
                    _attack2Enable = false;
                }
            }
            if (_time2 > 1)
            {
                _attack2Enable = false;
                StartCoroutine(nameof(cancombocorutine));
            }
        }
        // コンボ３ 
        if (_attack3Enable)
        {
            _animator.SetBool("isAtt_2", false);

            //コンボ３
            _animator.SetBool("isAtt_3", true);
            _time3 += Time.deltaTime;
            if (_time3 > 1)
            {
                StartCoroutine(nameof(cancombocorutine));
                _attack3Enable = false;
            }
        }
    }

    IEnumerator cancombocorutine()
    {
        Debug.Log("こーるちん");
        _canCombo = false;
        combocombo = false;
        _attack2Enable = false;
        _attack3Enable = false;
        AttackReset();
        yield return new WaitForSeconds(0.5f);
        combocombo = true;
    }

    void AttackReset()
    {
        _animator.SetBool("isAtt_1", false);
        _animator.SetBool("isAtt_2", false);
        _animator.SetBool("isAtt_3", false);
        _time1 = 0;
        _time2 = 0;
        _time3 = 0;
    }
}
