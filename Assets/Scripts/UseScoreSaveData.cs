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
public class UseSaveData : MonoBehaviour
{
    [SerializeField] InputField[] _inputFields;

    SaveManager _saveManager = new SaveManager();
    string _saveDirPath;
    // Directory.GetFiles が string[] を返す 
    [Tooltip("ソートされていないテキストデータ")] string[] _existFiles = null;
    [Tooltip("ソート前の複数のスコアが入ったリスト")] List<int> numberList = new List<int>();
    [Tooltip("ソート済みの複数のスコアが入ったリスト")] List<int> sortedList = new List<int>();
    [Tooltip("何位")] int _rank = 0;
    [SerializeField] Text _text = default;

    private void Awake()
    {
        // 保存先 
        _saveDirPath = Application.persistentDataPath;
        Debug.Log(_saveDirPath);
    }

    private void Update()
    {
        // データを作って保存  
        if (Input.GetKeyDown(KeyCode.P))
        {
            string playerName = _inputFields[0].text;
            ScoreSaveData sdata = _saveManager.CreateSaveData(playerName, int.Parse(_inputFields[1].text), int.Parse(_inputFields[2].text), int.Parse(_inputFields[3].text));
            _saveManager.DataSave(sdata, _saveDirPath, $"{playerName}のデータ.data"); 
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
        Debug.LogWarning(string.Join(", ", sortedList));
    }
    /// <summary>
    /// テキスト表示 
    /// </summary>
    void ShowText()
    {
        // スコアリストの上から順に、テキストデータとスコアが一致したらテキスト表示する 
        for (var i = 0; i < sortedList.Count; i++)
        {
            for (var j = 0; j < _existFiles.Length; j++)
            {
                ScoreSaveData sdata = _saveManager.DataLoad(_saveDirPath, _existFiles[j]);
                if (sortedList[i] == sdata._score)
                {
                    _rank++;
                    _text.text = _text.text + $"{_rank}位：名前 : {sdata._playerName} スコア : {sdata._score} \n";
                }
            }
        }
    }
}