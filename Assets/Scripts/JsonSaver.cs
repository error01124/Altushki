using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver : ISaver
{
    private List<ISaveData> _data;
    private ResourceLocator _resourceLocator;

    public JsonSaver()
    {
        _resourceLocator = ServiceLocator.Instance.Get<ResourceLocator>();
        InitData();
    }

    private void InitData()
    {
        _data = new List<ISaveData>();
        string path = _resourceLocator.GetAbsolutePath("Saves");
        FileInfo[] files = new DirectoryInfo(path).GetFiles();

        for (int i = 0; i < files.Length; i++)
        {
            using (StreamReader streamReader = files[i].OpenText())
            {
                string json = streamReader.ReadToEnd();
                _data[i] = JsonUtility.FromJson<ISaveData>(json);
            }
        }
    }

    public ISaveData GetData(int id)
    {
       return _data[id];
    }

    public int GetSlotsCount()
    {
        return _data.Count;
    }

    public void SaveData(ISaveData data)
    {
        string fileName = $"save_{data.GetSaveTime()}";
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fileName, json);
    }
}
