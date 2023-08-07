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
        // 略 
        SaveDirPath = Application.persistentDataPath;
        Debug.Log(SaveDirPath);
    }

    private void Update()
    {
        // 略 
        if (Input.GetKeyDown(KeyCode.P))
        {
            ScoreSaveData scoreSaveData = saveManager.CreateSaveData(inputFields[0].text, int.Parse(inputFields[1].text), int.Parse(inputFields[2].text), int.Parse(inputFields[3].text));
            saveManager.DataSave(scoreSaveData, SaveDirPath, $"スコア{inputFields[1].text}のデータ.data");
            Debug.Log($"{scoreSaveData._playerName} _playerName : {scoreSaveData._score} _score {scoreSaveData._killCount} _killCount {scoreSaveData._remainingHp} _remainingHp    {SaveDirPath} : SaveDirPath");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            // スコアの数値 検索でデータを呼び出せる 
            ScoreSaveData scoreSaveData = saveManager.DataLoad(SaveDirPath, $"スコア{inputFields[1].text}のデータ.data");
            Debug.Log($"{scoreSaveData._playerName} _playerName : {scoreSaveData._score} _score {scoreSaveData._killCount} _killCount {scoreSaveData._remainingHp} _remainingHp");
        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            numberList.Clear();
            // フォルダ内の全てのファイルの、検索のテスト 
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
            // スコア順に並び替え 
            SortScore();
        }
    }


    void SortScore()
    {
        _text.text = null;
        // リストを降順ソート
        var sortedList = numberList.OrderByDescending(i => i).ToList();
        Debug.LogWarning(string.Join(", ", sortedList));
        for (var i = 0; i < sortedList.Count; i++)
        {
            ScoreSaveData sdata = saveManager.DataLoad(SaveDirPath, $"スコア{sortedList[i]}のデータ.data");
            _text.text = _text.text + $"{i + 1}位：プレイヤー名 : {sdata._playerName} スコア : {sdata._score} \n";
            Delete(sortedList.Count, sortedList); 
        }
    }

    /// <summary>
    /// テキストデータの数が6以上になったら、削除 
    /// </summary>
    /// <param name="num">リストの要素数</param>
    /// <param name="list">スコアの値が入ったリスト</param>
    void Delete(int num, List<int> list)
    {
        if (num >= 6)
        {
            File.Delete(Path.Combine(SaveDirPath, $"スコア{list[5]}のデータ.data"));
            Debug.LogWarning($"スコア{list[5]}のデータ.data を削除しました。");
            list.RemoveAt(5);
        }
    }
}