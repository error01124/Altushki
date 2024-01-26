using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChoiceLabel : SceneObject<ChoiceLabel>, IService
{
    public Choice Result => _result;
    public event Action<ChoiceLabel, Choice> ChoiceSelected;

    private PrefabInstantiater _prefabInstantiater;
    private PrefabsPaths _prefabsPaths;
    private List<Choice> _choices;
    private List<string> _rows;
    private EnumState _state;
    private Choice _result;

    public override void Init()
    {
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _choices = new List<Choice>();
        _rows = new List<string>();
    }

    public ChoiceLabel Setup(params string[] rows)
    {
        Clear();
        _rows = rows.ToList();
        return this;
    }

    public void Clear()
    {
        _animation = EnumAnimation.None;
        _state = EnumState.None;

        for (int i = 0; i < _choices.Count; i++)
        {
            Destroy(_choices[i].gameObject);
        }

        _choices.Clear();
    }

    public override IEnumerator Show()
    {
        if (HasAnimation())
        {
            yield return StartCoroutine(PlayAnimation());
        }

        _choices = _prefabInstantiater.InstantiateMany<Choice>(_prefabsPaths.ChoiceButton, _rows.Count).ToList();

        for (int i = 0; i < _choices.Count; i++)
        {
            _choices[i].Init(this);
            _choices[i].Show(_rows[i]);
        }

        yield return new WaitUntil(() => _state != EnumState.Skiped);
    }

    public override IEnumerator Hide()
    {
        return null;
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
