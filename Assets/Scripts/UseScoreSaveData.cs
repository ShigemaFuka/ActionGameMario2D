using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����L���O�@�\
/// �v���C���[�����������A�f�[�^���㏑�������
/// �X�R�A���d��������Â��f�[�^���ɁA�~���ɂȂ�
/// </summary>
public class UseScoreSaveData : MonoBehaviour
{
    //[SerializeField] InputField _inputField;

    SaveManager _saveManager = new SaveManager();
    string _saveDirPath;
    // Directory.GetFiles �� string[] ��Ԃ� 
    [Tooltip("�\�[�g����Ă��Ȃ��e�L�X�g�f�[�^")] string[] _existFiles = null;
    [Tooltip("�\�[�g�O�̕����̃X�R�A�����������X�g")] List<int> numberList = new List<int>();
    [Tooltip("�\�[�g�ς݂̕����̃X�R�A�����������X�g")] List<int> sortedList = new List<int>();
    [Tooltip("����")] int _rank = 0;
    [SerializeField] Text _text = default;
    string playerName;
    GameManager _gameManager;


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
        _existFiles = Directory.GetFiles(_saveDirPath, "*");
        ScoreSaveData sdata = _saveManager.CreateSaveData(_gameManager.PlayerName, _gameManager.Score, _gameManager.KillCount, _gameManager.RemainingHp);
        //int count = 0;
        //for (var i = 0; i < _existFiles.Length; i++)
        //{

        //    if (_existFiles[i].Contains("Unknown"))
        //    {
        //        //var str = MakeName("Unknown_");
        //        _saveManager.DataSave(sdata, _saveDirPath, $"{_gameManager.PlayerName}�̃f�[�^.data");
        //    }
        //    else
                _saveManager.DataSave(sdata, _saveDirPath, $"{_gameManager.PlayerName}�̃f�[�^.data");
        //}
        ResetData();
        SortAndAdd();
        ShowText();
        //Delete();
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
        // �X�R�A���X�g�̏ォ�珇�ɁA�e�L�X�g�f�[�^�ƃX�R�A����v������e�L�X�g�\������ 
        for (var i = 0; i < sortedList.Count; i++)
        {
            for (var j = 0; j < _existFiles.Length; j++)
            {
                ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, _existFiles[j]);
                if (sortedList[i] == sdata._score)
                {
                    _rank++;
                    
                    _text.text = _text.text + $"{_rank}�ʁF{sdata._playerName} �X�R�A : {sdata._score} \n";
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
        //_gameManager.PlayerName = null;
    }

    string MakeName(string resultString)
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var Charsarr = new char[5];
        var random = new System.Random();

        for (int i = 0; i < Charsarr.Length; i++)
        {
            Charsarr[i] = characters[random.Next(characters.Length)];
        }

        return resultString = resultString + new String(Charsarr);
        //var resultString = new String(Charsarr);
        //Console.WriteLine(resultString);
    }
}