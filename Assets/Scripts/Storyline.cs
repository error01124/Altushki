using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Storyline : IService
{
    private DialogManager _dialogManager;
    private ChoiceLabelManager _choiceLabelManager;
    private StoryScene _currentScene;
    private bool _isAwating;

    public Storyline()
    {
        _dialogManager = ServiceLocator.Instance.Get<DialogManager>();
        _choiceLabelManager = ServiceLocator.Instance.Get<ChoiceLabelManager>();
        _dialogManager.DialogSkiped += OnDialogSkiped;
        _choiceLabelManager.ChoiceSelected += OnChoiceSelected;
    }

    public void CreateScenes()
    {
        Debug.Log("CreateScenes");
        ShowDialog("danya", "s");
    }

    public void ShowDialog(string characterName, string speech)
    {
        _isAwating = true;
        _dialogManager.Show(characterName, speech);
        WaitSceneEndAsync();
    }

    private async void WaitSceneEndAsync()
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