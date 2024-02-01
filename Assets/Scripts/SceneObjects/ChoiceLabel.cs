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
        Debug.Log("pre init");
        base.Init();
        Debug.Log("post init");
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _choices = new List<Choice>();
        _rows = new List<string>();
        Clear();
    }

    public ChoiceLabel Setup(string[] names, string[] rows)
    {
        _rows = rows.ToList();
        Clear();
        return this;
    }

    public override void Clear()
    {
        base.Clear();
        _state = EnumState.None;

        for (int i = 0; i < _choices.Count; i++)
        {
            Destroy(_choices[i].gameObject);
        }

        _rows.Clear();
        _choices.Clear();
    }

    public override IEnumerator Show()
    {
        _enabled = true;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Show);
        }

        _choices = _prefabInstantiater.InstantiateMany<Choice>(_prefabsPaths.ChoiceButton, _rows.Count).ToList();

        for (int i = 0; i < _choices.Count; i++)
        {
            _choices[i].Init(this);
            _choices[i].Setup(_rows[i], _rows[i]);
            _choices[i].Show();
        }

        yield return new WaitUntil(() => _state != EnumState.Skiped);
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
