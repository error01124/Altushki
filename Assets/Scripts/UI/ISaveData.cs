using System;
using UnityEngine;

public interface ISaveData
{
    public TimeSpan GetSaveTime();

    public Sprite GetScreenshot();

    public StoryData GetStoryData();

    public int GetSceneId();
}
