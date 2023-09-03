using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルトシーンで使用 
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
        ResetData();
        ScoreSaveData sdata = _saveManager.CreateSaveData(_gameManager.PlayerName, _gameManager.Score, _gameManager.KillCount, _gameManager.RemainingHp);
        //_saveManager.DataSave(sdata, _saveDirPath, $"{_gameManager.PlayerName}のデータ.data");
        Comparison(sdata, _gameManager.PlayerName, _gameManager.Score);
        SortAndAdd();
        ShowText();
    }
    /// <summary>
    /// スコアの記録が6つ以上になったら、削除 
    /// </summary>
    void Delete(int i, int j)
    {
        File.Delete(Path.Combine(_saveDirPath, _existFiles[j]));
        Debug.LogWarning($"{_existFiles[j]} を削除しました。");
        sortedList.RemoveAt(i);
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
    /// 上位5位までを表示する
    /// 同じスコアのときは名前順になる(数字＞英字) 
    /// </summary>
    void ShowText()
    {
        int count = 0;
        // スコアリストの上から順に、テキストデータとスコアが一致したらテキスト表示する 
        for (var i = 0; i < sortedList.Count; i++)
        {
            for (var j = 0; j < _existFiles.Length; j++)
            {
                Debug.Log(j);
                ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, _existFiles[j]);
                if (sortedList[i] == sdata._score)
                {
                    _rank++;
                    count++;
                    if(count < 6) _text.text = _text.text + $"{_rank}位           {sdata._playerName}           {sdata._score}     {sdata._killCount} \n";
                    else Delete(i, j);　//余分を削除
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

        return resultString + new String(CharsArr);
    }
    /// <summary>
    /// 同じプレイヤー名で、スコアがより高い方を保存する 
    /// 元々あったデータのスコアより低ければ、保存しない 
    /// </summary>
    void Comparison(ScoreSaveData data, string playerName, int newScore)
    {
        ScoreSaveData oldSData = _saveManager.DataLoad(_saveDirPath, $"{playerName}のデータ.data");
        if (oldSData != null)
        {
            if (oldSData._score < newScore)
            {
                Debug.Log($"「{playerName}」の記録を更新"); 
                _saveManager.DataSave(data, _saveDirPath, $"{playerName}のデータ.data");
            }
        }
        _saveManager.DataSave(data, _saveDirPath, $"{playerName}のデータ.data");
    }
}