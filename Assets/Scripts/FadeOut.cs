using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FadeOut : MonoBehaviour
{
    /// <summary>�t�F�[�h�p Image</summary>
    [SerializeField] Image _fadeImage = default;
    /// <summary>�t�F�[�h�A�E�g�����܂łɂ����鎞�ԁi�b�j/summary>
    [SerializeField] float _fadeTime = 1;
    float _timer = 0;
    Color c; 

    /// <summary>
    /// �t�F�[�h�A�E�g���J�n����
    /// </summary>
    public void ToFadeOut(string _sceneName)
    {
        Debug.Log("�R���[�`���ɂ�� Fade �J�n");
        StartCoroutine(FadeOutRoutine(_sceneName));
    }

    IEnumerator FadeOutRoutine(string _sceneName)
    {
        // Image ���� Color ���擾���A���Ԃ̐i�s�ɍ��킹���A���t�@��ݒ肵�� Image �ɖ߂�
        while (true)
        {
            _timer += Time.deltaTime;
            c = _fadeImage.color; // ���݂� Image �̐F���擾����
            c.a = _timer / _fadeTime;   // �F�̃A���t�@�� 1 �ɋ߂Â��Ă���
            // TODO: �F�� Image �ɃZ�b�g����
            _fadeImage.color = c;

            // _fadeTime ���o�߂����珈���͏I������
            if (_timer > _fadeTime)
            {
                SceneManager.LoadScene(_sceneName); 
                yield break;
            }
            // ��ʍX�V�҂� 
            yield return new WaitForEndOfFrame();
        }
    }

    public void ToFadeIn()
    {
        c.a = 0;
        _fadeImage.color = c;
    }
}
