using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ChoiceLabel : MonoBehaviour, ILabel
{
    public event Action<ChoiceLabel, Choice> ChoiceSelected;

    private List<Choice> _choices;
    private PrefabInstantiater _prefabInstantiater;
    private PrefabsPaths _prefabsPaths;
    private ChoiceLabelManager _manager;

    private void Awake()
    {
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
    }

    public void Init(ChoiceLabelManager manager, string[] choicesTexts)
    {
        if (choicesTexts == null || choicesTexts.Length == 0)
        {
            Debug.LogError("Empty choices texts");
            return;
        }

        _manager = manager;
        _choices = _prefabInstantiater.InstantiateMany<Choice>(_prefabsPaths.ChoiceButton, choicesTexts.Length).ToList();

        for (int i = 0; i < _choices.Count; i++)
        {
            _choices[i].Init(this, choicesTexts[i]);
        }
    }

    public void OnChoiceSelected(Choice choice)
    {
        _manager.OnChoiceSelected(this, choice);
    }
}
