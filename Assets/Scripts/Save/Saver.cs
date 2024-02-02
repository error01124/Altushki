using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver
{
    public int CachedSavesCount => _cachedSaves.Count;

    private List<SaveData> _cachedSaves;
    private ResourceLocator _resourceLocator;
    private string _savesPath;
    private string _gameSettingsPath;

    public Saver()
    {
        _resourceLocator = ServiceLocator.Instance.Get<ResourceLocator>();
        _savesPath = Path.Combine(Application.persistentDataPath, "Saves");
        _gameSettingsPath = Path.Combine(Application.persistentDataPath, "GameSettings");
    }

    public void ReadCachedSaves()
    {
        _cachedSaves = new List<SaveData>();
        DirectoryInfo savesDirectory = new DirectoryInfo(_savesPath);

        if (!savesDirectory.Exists)
        {
            savesDirectory.Create();
        }

        DirectoryInfo[] saveDirectories = savesDirectory.GetDirectories();

        for (int i = 0; i < saveDirectories.Length; i++)
        {
            string saveName = saveDirectories[i].Name;
            SaveData saveData = ReadSave(saveName);
            _cachedSaves.Add(saveData);
        }
    }

    private SaveData ReadSave(string saveName)
    {
        string savePath = Path.Combine(_savesPath, saveName);
        var screenshotBytes = File.ReadAllBytes(Path.Combine(savePath, "Screenshot"));
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(screenshotBytes);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        
        FileInfo fileInfo = new FileInfo(Path.Combine(savePath, "SaveData"));
        DateTime saveTime = fileInfo.LastWriteTime;

        string json = File.ReadAllText(Path.Combine(savePath, "SaveData.json"));
        StoryData storyData = JsonUtility.FromJson<StoryData>(json);

        SaveData saveData = new SaveData(sprite, storyData, saveTime);
        return saveData;
    }

    public void WriteSave(Sprite screenshot, StoryData storyData, DateTime time)
    {
        string saveDirectoryPath = Path.Combine(_savesPath, $"save_{DateTime.Now}");
        var saveDirectoryInfo = new DirectoryInfo(saveDirectoryPath);

        if (!saveDirectoryInfo.Exists)
        {
            saveDirectoryInfo.Create();
        }

        string screenshotFilePath = Path.Combine(_savesPath, "Screenshot");
        var screenshotBytes = screenshot.texture.EncodeToPNG();

        using (var fileStream = File.Open(screenshotFilePath, FileMode.Create))
        {
            var binary = new BinaryWriter(fileStream);
            binary.Write(screenshotBytes);
        }

        string saveDataFilePath = Path.Combine(_savesPath, "SaveData");
        string json = JsonUtility.ToJson(storyData);
        File.WriteAllText(saveDataFilePath, json);
    }

    public void WriteGameSetting()
    {
        var gameSettings = ServiceLocator.Instance.Get<GameSettings>();
        string json = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(_gameSettingsPath, json);
    }

    public GameSettings ReadGameSettings()
    {
        string json = File.ReadAllText(_gameSettingsPath);
        return JsonUtility.FromJson<GameSettings>(json);
    }

    public SaveData GetCachedSave(int number)
    {
       return _cachedSaves[number];
    }
}
