using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class Storyline : IService
{
    private DialogManager _dialogManager;
    private ChoiceLabelManager _choiceLabelManager;
    private BackgroundManager _backgroundManager;
    private MusicPlayer _musicPlayer;
    private StoryScene _currentScene;
    private PrefabsPaths _prefabsPaths;
    private CharacterManager _characterManager;
    private bool _isAwating;

    public Storyline()
    {
        _dialogManager = ServiceLocator.Instance.Get<DialogManager>();
        _choiceLabelManager = ServiceLocator.Instance.Get<ChoiceLabelManager>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _backgroundManager = ServiceLocator.Instance.Get<BackgroundManager>();
        _musicPlayer = ServiceLocator.Instance.Get<MusicPlayer>();
        _characterManager = ServiceLocator.Instance.Get<CharacterManager>();
        _dialogManager.DialogSkiped += OnDialogSkiped;
        _choiceLabelManager.ChoiceSelected += OnChoiceSelected;
    }

    public async void CreateScenesAsync()
    {
        Debug.Log("CreateScenes");
        ShowBackground(_prefabsPaths.ForestBackground);
        PlayMusic(_prefabsPaths.MainMusic);
        ShowCharacter("Sprites/Characters/Lena/Calm", EnumPosition.Left);
        await ShowDialog("Даня", "пися").WaitCompletingAsync();
        await ShowDialog("Даня", "попа").WaitCompletingAsync();
        await ShowDialog("Даня", "какашечки").WaitCompletingAsync();
        HideDialog();
        HideCharacter(EnumPosition.Left);
        StopMusic();
        HideBackground();
    }

    public void ShowCharacter(string path, EnumPosition position)
    {
        _characterManager.Show(path, position);
    }

    public void HideCharacter(EnumPosition position)
    {
        _characterManager.Hide(position);
    }

    public void HideEveryCharacter()
    {
        _characterManager.HideEveryone();
    }

    public Dialog ShowDialog(string characterName, string speech)
    {
        return _dialogManager.Show(characterName, speech);
    }

    public void HideDialog()
    {
        _dialogManager.Hide();
    }

    public void PlayMusic(string path)
    {
        _musicPlayer.Play(path);
    }

    public void StopMusic()
    {
        _musicPlayer.Stop();
    }

    public void ShowBackground(string path)
    {
        _backgroundManager.Show(path);
    }

    public void HideBackground()
    {
        _backgroundManager.Hide();
    }

    private async Task WaitSceneEndAsync()
    {
        await Task.Run(() =>
        {
            while (_isAwating)
            {
                Task.Delay(100);
            }
        });
    }

    private void OnDialogSkiped(Dialog dialog)
    {
        GoToNextScene();
    }

    private void OnChoiceSelected(ChoiceLabel choiceLabel, Choice choice)
    {
        GoToNextScene();
    }

    public void GoToNextScene()
    {
        _isAwating = false;
    }
}