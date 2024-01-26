using System;
using UnityEngine;

public class SaveData : ISaveData
{
    public StoryData GetGameData()
    {
        return null;
    }

    public TimeSpan GetSaveTime()
    {
        throw new NotImplementedException();
    }

    public int GetSceneId()
    {
        return 0;
    }

    public Sprite GetScreen()
    {
        return null;
    }
}
