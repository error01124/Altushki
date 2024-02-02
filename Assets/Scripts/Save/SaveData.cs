using System;
using UnityEngine;

public class SaveData
{
    public Sprite Screenshot => _screenshot;
    public StoryData StoryData => _storyData;
    public DateTime SaveTime => _saveTime;

    private Sprite _screenshot;
    private StoryData _storyData;
    private DateTime _saveTime;

    public SaveData(Sprite screenshot, StoryData storyData, DateTime saveTime)
    {
        _screenshot = screenshot;
        _storyData = storyData;
        _saveTime = saveTime;
    }
}
