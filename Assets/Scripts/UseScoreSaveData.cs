using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����L���O�@�\
/// �v���C���[�����������A�f�[�^���㏑�������
/// �X�R�A���d��������Â��f�[�^���ɁA�~���ɂȂ�
/// </summary>
public class UseScoreSaveData : MonoBehaviour
{
    SaveManager _saveManager = new SaveManager();
    string _saveDirPath = default;
    // Directory.GetFiles �� string[] ��Ԃ� 
    [Tooltip("�\�[�g����Ă��Ȃ��e�L�X�g�f�[�^")] string[] _existFiles = null;
    [Tooltip("�\�[�g�O�̕����̃X�R�A�����������X�g")] List<int> numberList = new List<int>();
    [Tooltip("�\�[�g�ς݂̕����̃X�R�A�����������X�g")] List<int> sortedList = new List<int>();
    [Tooltip("����")] int _rank = 0;
    [SerializeField] Text _text = default;
    GameManager _gameManager = default;


    private void Awake()
    {
        // �ۑ��� 
        _saveDirPath = Application.persistentDataPath;
        Debug.Log(_saveDirPath);
    }

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        if (String.IsNullOrEmpty(_gameManager.PlayerName))
            _gameManager.PlayerName = MakeName("Unknown_");
        // �t�H���_���̑S�Ẵt�@�C���̌��� 
        //_existFiles = Directory.GetFiles(_saveDirPath, "*");
        ScoreSaveData sdata = _saveManager.CreateSaveData(_gameManager.PlayerName, _gameManager.Score, _gameManager.KillCount, _gameManager.RemainingHp);
        _saveManager.DataSave(sdata, _saveDirPath, $"{_gameManager.PlayerName}�̃f�[�^.data");
        ResetData();
        SortAndAdd();
        ShowText();
    }
    /// <summary>
    /// �e�L�X�g�f�[�^�̐���11�ȏ�ɂȂ�����A�폜 
    /// </summary>
    void Delete()
    {
        if (sortedList.Count >= 11)
        {
            for (var i = 10; i < sortedList.Count; i++)
            {
                for (var j = 0; j < _existFiles.Length; j++)
                {
                    ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, _existFiles[j]);
                    // �X�R�A���X�g�̏ォ�珇�ɁA�e�L�X�g�f�[�^�ƃX�R�A����v������폜���� 
                    if (sortedList[i] == sdata._score)
                    {
                        File.Delete(Path.Combine(_saveDirPath, _existFiles[j]));
                        Debug.LogWarning($"{_existFiles[j]} ���폜���܂����B");
                        sortedList.RemoveAt(i);
                    }
                }
            }
        }
    }
    /// <summary>
    /// �S�����A�\�[�g�A�ǉ� 
    /// </summary>
    void SortAndAdd()
    {
        // �t�H���_���̑S�Ẵt�@�C���̌���  
        _existFiles = Directory.GetFiles(_saveDirPath, "*");
        foreach (string str in _existFiles)
        {
            ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, str);
            // �l���d�������Ȃ� 
            if (!numberList.Contains(sdata._score))
                numberList.Add(sdata._score);
            // ���X�g���~���\�[�g
            sortedList = numberList.OrderByDescending(i => i).ToList();
        }
        Debug.Log($"<color=orange>{string.Join(", ", sortedList)}</color> ");
    }
    /// <summary>
    /// �e�L�X�g�\�� 
    /// </summary>
    void ShowText()
    {
        int count = 0;
        // �X�R�A���X�g�̏ォ�珇�ɁA�e�L�X�g�f�[�^�ƃX�R�A����v������e�L�X�g�\������ 
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < _existFiles.Length; j++)
            {
                ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, _existFiles[j]);
                if (sortedList[i] == sdata._score && count < 5)
                {
                    _rank++;
                    count++;
                    _text.text = _text.text + $"{_rank}��     {sdata._playerName}     {sdata._score}     {sdata._killCount}     {sdata._remainingHp} \n";
                }
            }
        }
    }
    /// <summary>
    /// ������
    /// </summary>
    void ResetData()
    {
        numberList.Clear();
        sortedList.Clear();
        _rank = 0;
        _text.text = null;
    }

    /// <summary>
    /// �u Unknown_jwa5k �v�̂悤�ȃ��[�U�[������� 
    /// </summary>
    /// <param name="resultString">�uUnknown_�v</param>
    /// <returns></returns>
    string MakeName(string resultString)
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var CharsArr = new char[5];
        var random = new System.Random();

        for (int i = 0; i < CharsArr.Length; i++)
        {
            CharsArr[i] = characters[random.Next(characters.Length)];
        }

        return resultString = resultString + new String(CharsArr);
    }
}