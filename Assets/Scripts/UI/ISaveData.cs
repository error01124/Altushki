using System;
using UnityEngine;

public interface ISaveData
{
    public TimeSpan GetSaveTime();

    public Sprite GetScreen();

    public StoryData GetGameData();

    public int GetSceneId();
}
