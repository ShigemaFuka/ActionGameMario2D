using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// BinaryFormatterで値をテキストデータで保存
/// </summary>
public class SaveManager
{
    public void DataSave(ScoreSaveData sdata, string fpath, string fname)
    {
        // バイナリで保存
        BinaryFormatter bf = new BinaryFormatter();
        string saveFilePath = Path.Combine(fpath, fname);
        using (FileStream file = File.Create(saveFilePath))
        {
            bf.Serialize(file, sdata);
        }
    }

    public ScoreSaveData DataLoad(string fpath, string fname)
    {
        string loadFilePath = Path.Combine(fpath, fname);
        // 保存データがない・検索に該当しないとき 
        if (!File.Exists(loadFilePath))
        {
            Debug.Log("No Load Data. " + loadFilePath);
            return null;
        }
        BinaryFormatter bf = new BinaryFormatter();
        ScoreSaveData sdata;
        using (FileStream file = File.Open(loadFilePath, FileMode.Open))
        {
            sdata = (ScoreSaveData)bf.Deserialize(file);
        }
        return sdata;
    }
    public ScoreSaveData CreateSaveData(string name, int score, int killCount, int remainingHp)
    {
        ScoreSaveData sdata = new ScoreSaveData();
        sdata._playerName = name;
        sdata._score = score;
        sdata._killCount = killCount;
        sdata._remainingHp = remainingHp;
        return sdata;
    }
}


