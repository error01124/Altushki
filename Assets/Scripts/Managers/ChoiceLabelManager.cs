using System;
using UnityEngine;

public class ChoiceLabelManager : IService
{
    public event Action<ChoiceLabel, Choice> ChoiceSelected;

    private PrefabInstantiater _prefabInstantiater;
    private PrefabsPaths _prefabsPaths;

    public ChoiceLabelManager()
    {
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
    }

    public ChoiceLabel Show(string[] _choicesTexts)
    {
        ChoiceLabel choiceLabel = _prefabInstantiater.InstantiateUI<ChoiceLabel>(_prefabsPaths.ChoiceLabel);
        choiceLabel.Init(this, _choicesTexts);
        return choiceLabel;
    }

    public void OnChoiceSelected(ChoiceLabel choiceLabel, Choice choice)
    {
        ChoiceSelected?.Invoke(choiceLabel, choice);
        Hide(choiceLabel);
    }

    public void Hide(ChoiceLabel choiceLabel)
    {
        GameObject.Destroy(choiceLabel.gameObject);
    }
}
