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
    Animator _animator = default; 
    [SerializeField, Tooltip("第1攻撃フラグ")] bool _attack1Enable = false;
    [SerializeField, Tooltip("第2攻撃フラグ")] bool _attack2Enable = false;
    [SerializeField, Tooltip("第3攻撃フラグ")] bool _attack3Enable = false;
    [SerializeField, Tooltip("第1攻撃の連続使用防止＆必ずAtt_1から始めるためのフラグ")] bool _startCombo = true;
    [SerializeField, Tooltip("コンボ攻撃使用可能かのフラグ")] bool _canCombo = false;
    [SerializeField, Tooltip("第2攻撃受け付け時間＆リセット時間")] float _time1 = 0;
    [SerializeField, Tooltip("第3攻撃受け付け時間＆リセット時間")] float _time2 = 0;
    [SerializeField, Tooltip("リセット時間")] float _time3 = 0;
    [SerializeField] int count = 0; 
    void Start()
    {
        _animator = GetComponent<Animator>();
        _attack1Enable = true; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !_canCombo && !_attack2Enable && !_attack3Enable)
        {
            _canCombo = true;

            count = 0;
        }
           
        // コンボ１ 
        if (_canCombo && !_attack2Enable && _startCombo)
        {
            //入力したら一定期間入力を受け付け、入力があったらコンボ２へ移行、なかったらキャンセル
            if (Input.GetAxis("Horizontal") != 0 && count == 0)
            {
                //_animator.SetBool("isAttAndWalk_1", true);
                _animator.SetTrigger("isAttAndWalk_1Tri"); 

                count = 1;
            }
            else _animator.SetBool("isAtt_1", true);


            _time1 += Time.deltaTime;
            if (_time1 > 0.1 && _time1 < 1)
            {
                //入力受付期間
                if (Input.GetKeyDown(KeyCode.N))
                {
                    // 第2攻撃に遷移 
                    _time1 = 0;
                    _attack2Enable = true;
                    _startCombo = false; 

                    count = 0;
                }
            }
            if (_time1 > 1)
            {
                //入力されなかったときの処理(第2攻撃に遷移しない)
                // アニメーションが大体実行されたら(1秒くらい)第1攻撃使用不可にする
                StartCoroutine(nameof(CanComboCorutine)); 
            }
        }
        // コンボ２ 
        if (_attack2Enable)
        {
            if (_animator.GetBool("isAttAndWalk_1")) _animator.SetBool("isAttAndWalk_1", false);
            else if (_animator.GetBool("isAtt_1")) _animator.SetBool("isAtt_1", false);

            if (Input.GetAxis("Horizontal") != 0 && count == 0)
            {
                //_animator.SetBool("isAttAndWalk_2", true);
                _animator.SetTrigger("isAttAndWalk_2Tri");

                count = 1; 
            }
            else _animator.SetBool("isAtt_2", true);
            

            _time2 += Time.deltaTime;
            // 「_time2 > 0.1」だと早すぎて、アニメーションが途中でも遷移する 
            if (_time2 > 0.4 && _time2 < 1)
            {
                if (Input.GetKeyDown(KeyCode.N))
                {
                    // 第3攻撃に遷移 
                    _time2 = 0;
                    _attack3Enable = true;
                    _attack2Enable = false;

                    count = 0; 
                }
            }
            if (_time2 > 1)
            {
                //入力されなかったときの処理(第3攻撃に遷移しない)
                // アニメーションが大体実行されたら(1秒くらい)第2攻撃使用不可にする
                _attack2Enable = false;
                StartCoroutine(nameof(CanComboCorutine));
            }
        }
        // コンボ３ 
        if (_attack3Enable)
        {
            if (_animator.GetBool("isAttAndWalk_2")) _animator.SetBool("isAttAndWalk_2", false);
            else if (_animator.GetBool("isAtt_2")) _animator.SetBool("isAtt_2", false);


            if (Input.GetAxis("Horizontal") != 0 && count == 0)
            {
                //_animator.SetBool("isAttAndWalk_3", true);
                _animator.SetTrigger("isAttAndWalk_3Tri");

                count = 1; 
            }
            else _animator.SetBool("isAtt_3", true);



            _time3 += Time.deltaTime; 
            if (_time3 > 1)
            {
                // アニメーションが大体実行されたら(1秒くらい)第3攻撃使用不可にする  
                StartCoroutine(nameof(CanComboCorutine));
                _attack3Enable = false;
            }
        }
    }

    IEnumerator CanComboCorutine()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetBool("isWalk", true);
        }
        _canCombo = false;
        _startCombo = false;
        _attack2Enable = false;
        _attack3Enable = false;
        AttackReset();
        yield return new WaitForSeconds(0.2f);
        _startCombo = true;

        _attack1Enable = true; 

    }

    /// <summary>
    /// アニメーションとタイマーを初期化
    /// </summary>
    void AttackReset()
    {
        _animator.SetBool("isAttAndWalk_1", false);
        _animator.SetBool("isAttAndWalk_2", false);
        _animator.SetBool("isAttAndWalk_3", false);
        //_animator.ResetTrigger("isAttAndWalk_1Tri"); 
        //_animator.ResetTrigger("isAttAndWalk_2Tri"); 
        //_animator.ResetTrigger("isAttAndWalk_3Tri"); 
        _animator.SetBool("isAtt_1", false);
        _animator.SetBool("isAtt_2", false);
        _animator.SetBool("isAtt_3", false);
        _time1 = 0;
        _time2 = 0;
        _time3 = 0;

        count = 0; 
    }
}
