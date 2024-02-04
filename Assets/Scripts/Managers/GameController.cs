using UnityEngine;
using System;
using Unity.VisualScripting;

public class GameController : IService
{
    public bool IsInGame => _isInGame;
    public bool IsPaused => _isPaused;

    private StartStoryline _startStoryline;
    private WindowManager _windowManager;
    private Saver _saver;
    private StoryData _storyData;
    private bool _isPaused;
    private Context _context;
    private Keybinds _keybinds;
    private bool _isInGame;
    private Coroutine _storylineCoroutine;

    public GameController()
    {
        _keybinds = ServiceLocator.Instance.Get<Keybinds>();
        _context = ServiceLocator.Instance.Get<Context>();
        _context.Updated += OnUpdated;
    }

    public void StartGame(SaveData saveData)
    {
        _isInGame = true;
        _storyData = saveData.StoryData;
        Scenarist scenarist = new Scenarist(saveData);
        _startStoryline = new StartStoryline(scenarist);
        _storylineCoroutine = _context.StartCoroutine(_startStoryline.InitScene());
    }

    public void CreateSave()
    {
        _saver.WriteSave(_storyData, DateTime.Now);
    }

    public void StopGame()
    {
        _isInGame = false;
        _context.StopCoroutine(_storylineCoroutine);
    }

    private void OnUpdated(float deltaTime)
    {
        if (IsInGame)
        {
            if (_keybinds.Quit())
            {
                if (_windowManager.IsOpen(_windowManager.InGameMenuWindow))
                {
                    _windowManager.Open(_windowManager.InGameMenuWindow);
                }
            }
        }
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }
}
