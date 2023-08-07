using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UseScoreSaveData : MonoBehaviour
{
    [SerializeField] InputField[] inputFields;
    SaveManager saveManager = new SaveManager();
    string SaveDirPath;

    string[] _existFiles = null;
    [SerializeField] Text _text = default;

    List<int> numberList = new List<int>();

    private void Awake()
    {
        // �� 
        SaveDirPath = Application.persistentDataPath;
        Debug.Log(SaveDirPath);
    }

    private void Update()
    {
        // �� 
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScoreSaveData scoreSaveData = saveManager.CreateSaveData(inputFields[0].text, int.Parse(inputFields[1].text), int.Parse(inputFields[2].text), int.Parse(inputFields[3].text));
            saveManager.DataSave(scoreSaveData, SaveDirPath, $"�X�R�A{inputFields[1].text}�̃f�[�^.data");
            Debug.Log($"{scoreSaveData._playerName} _playerName : {scoreSaveData._score} _score {scoreSaveData._killCount} _killCount {scoreSaveData._remainingHp} _remainingHp    {SaveDirPath} : SaveDirPath");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            // �X�R�A�̐��l �����Ńf�[�^���Ăяo���� 
            ScoreSaveData scoreSaveData = saveManager.DataLoad(SaveDirPath, $"�X�R�A{inputFields[1].text}�̃f�[�^.data");
            Debug.Log($"{scoreSaveData._playerName} _playerName : {scoreSaveData._score} _score {scoreSaveData._killCount} _killCount {scoreSaveData._remainingHp} _remainingHp");
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            numberList.Clear();
            // �t�H���_���̑S�Ẵt�@�C���́A�����̃e�X�g 
            _existFiles = Directory.GetFiles(SaveDirPath, "*");
            foreach (string str in _existFiles)
            {
                ScoreSaveData scoreSaveData = saveManager.DataLoad(SaveDirPath, str);
                numberList.Add(scoreSaveData._score);
            }
            foreach (int chr in numberList)
                Debug.Log($"numberList : {chr}");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // �X�R�A���ɕ��ёւ� 
            SortScore();
        }
    }


    void SortScore()
    {
        _text.text = null;
        // ���X�g���~���\�[�g
        var sortedList = numberList.OrderByDescending(i => i).ToList();
        Debug.LogWarning(string.Join(", ", sortedList));
        for (var i = 0; i < sortedList.Count; i++)
        {
            ScoreSaveData sdata = saveManager.DataLoad(SaveDirPath, $"�X�R�A{sortedList[i]}�̃f�[�^.data");
            _text.text = _text.text + $"{i + 1}�ʁF�v���C���[�� : {sdata._playerName} �X�R�A : {sdata._score} \n";
            Delete(sortedList.Count, sortedList); 
        }
    }

    /// <summary>
    /// �e�L�X�g�f�[�^�̐���6�ȏ�ɂȂ�����A�폜 
    /// </summary>
    /// <param name="num">���X�g�̗v�f��</param>
    /// <param name="list">�X�R�A�̒l�����������X�g</param>
    void Delete(int num, List<int> list)
    {
        if (num >= 6)
        {
            File.Delete(Path.Combine(SaveDirPath, $"�X�R�A{list[5]}�̃f�[�^.data"));
            Debug.LogWarning($"�X�R�A{list[5]}�̃f�[�^.data ���폜���܂����B");
            list.RemoveAt(5);
        }
    }
}