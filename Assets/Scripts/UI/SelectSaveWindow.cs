using System.Collections.Generic;
using UnityEngine;

public class SelectSaveWindow : Window
{
    private List<SaveChoice> _choices;
    private PrefabsPaths _prefabsPaths;
    private ISaver _saver;

    private void Awake()
    {
        _saver = ServiceLocator.Instance.Get<ISaver>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        InitChoices();
    }

    private void InitChoices()
    {
        string path = _prefabsPaths.ChoiceButton;
        _choices = new List<SaveChoice>();   
        SaveChoice prefabChoice = Resources.Load<SaveChoice>(path);

        for (int i = 0; i < _saver.GetSlotsCount(); i++)
        {
            GameObject obj = Instantiate(prefabChoice.gameObject);
            SaveChoice choice = obj.GetComponent<SaveChoice>();
            choice.Init(this);
        }
    }

    public void OnChoiceSelected(SaveChoice choice)
    {
        //choice.SaveData;
    }
}
