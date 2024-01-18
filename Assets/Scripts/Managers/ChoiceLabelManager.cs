using System;
using UnityEngine;

public class ChoiceLabelManager : MonoBehaviour, IService
{
    public event Action<ChoiceLabel, Choice> ChoiceSelected;

    [SerializeField] private ChoiceLabel _choiceLabel;

    public void Init()
    {
        _choiceLabel.Init(this);
    }

    public void Show(string[] rows)
    {
        _choiceLabel.gameObject.SetActive(true);
        _choiceLabel.Setup(rows);
    }

    public void OnChoiceSelected(ChoiceLabel choiceLabel, Choice choice)
    {
        ChoiceSelected?.Invoke(choiceLabel, choice);
        Hide(choiceLabel);
    }

    public void Hide(ChoiceLabel choiceLabel)
    {
        choiceLabel.gameObject.SetActive(false);
    }
}
