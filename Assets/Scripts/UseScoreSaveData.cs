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
public class UseSaveData : MonoBehaviour
{
    [SerializeField] InputField[] _inputFields;

    SaveManager _saveManager = new SaveManager();
    string _saveDirPath;
    // Directory.GetFiles �� string[] ��Ԃ� 
    [Tooltip("�\�[�g����Ă��Ȃ��e�L�X�g�f�[�^")] string[] _existFiles = null;
    [Tooltip("�\�[�g�O�̕����̃X�R�A�����������X�g")] List<int> numberList = new List<int>();
    [Tooltip("�\�[�g�ς݂̕����̃X�R�A�����������X�g")] List<int> sortedList = new List<int>();
    [Tooltip("����")] int _rank = 0;
    [SerializeField] Text _text = default;

    private void Awake()
    {
        // �ۑ��� 
        _saveDirPath = Application.persistentDataPath;
        Debug.Log(_saveDirPath);
    }

    private void Update()
    {
        // �f�[�^������ĕۑ�  
        if (Input.GetKeyDown(KeyCode.P))
        {
            string playerName = _inputFields[0].text;
            ScoreSaveData sdata = _saveManager.CreateSaveData(playerName, int.Parse(_inputFields[1].text), int.Parse(_inputFields[2].text), int.Parse(_inputFields[3].text));
            _saveManager.DataSave(sdata, _saveDirPath, $"{playerName}�̃f�[�^.data"); 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            numberList.Clear();
            sortedList.Clear();
            _rank = 0;
            _text.text = null;
            SortAndAdd();
            ShowText();
            Delete();
        }
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
        Debug.LogWarning(string.Join(", ", sortedList));
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
                    _text.text = _text.text + $"{_rank}�ʁF���O : {sdata._playerName} �X�R�A : {sdata._score} \n";
                }
            }
        }
    }
}