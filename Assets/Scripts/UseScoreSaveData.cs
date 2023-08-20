using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ランキング機能
/// プレイヤー名が被ったら、データが上書きされる
/// スコアが重複したら古いデータ順に、降順になる
/// </summary>
public class UseScoreSaveData : MonoBehaviour
{
    SaveManager _saveManager = new SaveManager();
    string _saveDirPath = default;
    // Directory.GetFiles が string[] を返す 
    [Tooltip("ソートされていないテキストデータ")] string[] _existFiles = null;
    [Tooltip("ソート前の複数のスコアが入ったリスト")] List<int> numberList = new List<int>();
    [Tooltip("ソート済みの複数のスコアが入ったリスト")] List<int> sortedList = new List<int>();
    [Tooltip("何位")] int _rank = 0;
    [SerializeField] Text _text = default;
    GameManager _gameManager = default;


    private void Awake()
    {
        // 保存先 
        _saveDirPath = Application.persistentDataPath;
        Debug.Log(_saveDirPath);
    }

    void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        if (String.IsNullOrEmpty(_gameManager.PlayerName))
            _gameManager.PlayerName = MakeName("Unknown_");
        // フォルダ内の全てのファイルの検索 
        //_existFiles = Directory.GetFiles(_saveDirPath, "*");
        ScoreSaveData sdata = _saveManager.CreateSaveData(_gameManager.PlayerName, _gameManager.Score, _gameManager.KillCount, _gameManager.RemainingHp);
        _saveManager.DataSave(sdata, _saveDirPath, $"{_gameManager.PlayerName}のデータ.data");
        ResetData();
        SortAndAdd();
        ShowText();
    }
    /// <summary>
    /// テキストデータの数が11以上になったら、削除 
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
                    // スコアリストの上から順に、テキストデータとスコアが一致したら削除する 
                    if (sortedList[i] == sdata._score)
                    {
                        File.Delete(Path.Combine(_saveDirPath, _existFiles[j]));
                        Debug.LogWarning($"{_existFiles[j]} を削除しました。");
                        sortedList.RemoveAt(i);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 全検索、ソート、追加 
    /// </summary>
    void SortAndAdd()
    {
        // フォルダ内の全てのファイルの検索  
        _existFiles = Directory.GetFiles(_saveDirPath, "*");
        foreach (string str in _existFiles)
        {
            ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, str);
            // 値を重複させない 
            if (!numberList.Contains(sdata._score))
                numberList.Add(sdata._score);
            // リストを降順ソート
            sortedList = numberList.OrderByDescending(i => i).ToList();
        }
        Debug.Log($"<color=orange>{string.Join(", ", sortedList)}</color> ");
    }
    /// <summary>
    /// テキスト表示 
    /// </summary>
    void ShowText()
    {
        int count = 0;
        // スコアリストの上から順に、テキストデータとスコアが一致したらテキスト表示する 
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < _existFiles.Length; j++)
            {
                ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, _existFiles[j]);
                if (sortedList[i] == sdata._score && count < 5)
                {
                    _rank++;
                    count++;
                    _text.text = _text.text + $"{_rank}位     {sdata._playerName}     {sdata._score}     {sdata._killCount}     {sdata._remainingHp} \n";
                }
            }
        }
    }
    /// <summary>
    /// 初期化
    /// </summary>
    void ResetData()
    {
        numberList.Clear();
        sortedList.Clear();
        _rank = 0;
        _text.text = null;
    }

    /// <summary>
    /// 「 Unknown_jwa5k 」のようなユーザー名を作る 
    /// </summary>
    /// <param name="resultString">「Unknown_」</param>
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