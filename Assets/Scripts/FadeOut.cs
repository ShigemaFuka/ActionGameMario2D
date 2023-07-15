using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeOut : MonoBehaviour
{
    /// <summary>フェード用 Image</summary>
    [SerializeField] Image _fadeImage = default;
    /// <summary>フェードアウト完了までにかかる時間（秒）/summary>
    [SerializeField] float _fadeTime = 1;
    float _timer = 0;
    Color c; 

    /// <summary>
    /// フェードアウトを開始する
    /// </summary>
    public void ToFadeOut(string _sceneName)
    {
        Debug.Log("コルーチンによる Fade 開始");
        StartCoroutine(FadeOutRoutine(_sceneName));
    }

    IEnumerator FadeOutRoutine(string _sceneName)
    {
        // Image から Color を取得し、時間の進行に合わせたアルファを設定して Image に戻す
        while (true)
        {
            _timer += Time.deltaTime;
            c = _fadeImage.color; // 現在の Image の色を取得する
            c.a = _timer / _fadeTime;   // 色のアルファを 1 に近づけていく
            // TODO: 色を Image にセットする
            _fadeImage.color = c;

            // _fadeTime が経過したら処理は終了する
            if (_timer > _fadeTime)
            {
                SceneManager.LoadScene(_sceneName); 
                yield break;
            }
            // 画面更新待ち 
            yield return new WaitForEndOfFrame();
        }
    }

    public void ToFadeIn()
    {
        c.a = 0;
        _fadeImage.color = c;
    }
}
