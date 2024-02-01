using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceWindow : SceneObject<ChoiceWindow>, IService
{
    public Choice Result => _result;
    public event Action<ChoiceWindow, Choice> ChoiceSelected;

    private PrefabInstantiater _prefabInstantiater;
    private PrefabsPaths _prefabsPaths;
    private List<Choice> _choices;
    private EnumState _state;
    private Choice _result;

    public override void Init()
    {
        base.Init();
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _choices = new List<Choice>();
        Clear();
    }

    public IEnumerator Display(string[] names, string[] rows)
    {
        Clear();
        _choices = _prefabInstantiater.InstantiateMany<Choice>(_prefabsPaths.ChoiceButton, rows.Length).ToList();

        for (int i = 0; i < _choices.Count; i++)
        {
            _choices[i].Init(this);
            _choices[i].Show();
            _choices[i].Display(rows[i], rows[i]);
        }

        yield return new WaitUntil(() => _state != EnumState.Skiped);
    }

    public override void Clear()
    {
        base.Clear();
        _state = EnumState.None;

        for (int i = 0; i < _choices.Count; i++)
        {
            Destroy(_choices[i].gameObject);
        }

        _choices.Clear();
    }

    public void OnChoiceSelected(Choice choice)
    {
        Skip();
        _result = choice;
    }

    public void Skip()
    {
        _state = EnumState.Skiped;
    }

    public enum EnumState
    {
        None,
        Skiped
    }
}
