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

    public void Init(ChoiceLabelManager manager)
    {
        _manager = manager;
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
    }

    public void Setup(string[] rows)
    {
        if (rows == null || rows.Length == 0)
        {
            Debug.LogError("Empty choices texts");
            return;
        }

        _choices = _prefabInstantiater.InstantiateMany<Choice>(_prefabsPaths.ChoiceButton, rows.Length).ToList();

        for (int i = 0; i < _choices.Count; i++)
        {
            _choices[i].Init(this);
            _choices[i].Setup(rows[i]);
        }
    }

    public void OnChoiceSelected(Choice choice)
    {
        _manager.OnChoiceSelected(this, choice);
    }
}
