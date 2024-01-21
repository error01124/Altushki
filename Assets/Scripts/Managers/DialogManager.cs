using System;
using UnityEngine;

public class DialogManager : MonoBehaviour, IService
{
    public event Action<Dialog> DialogSkiped;

    [SerializeField] private Dialog _dialog;

    public void Init()
    {
        _dialog.Init(this);
    }

    public ISceneAction Show(string characterName, string speech)
    {
        _dialog.gameObject.SetActive(true);
        return _dialog.Show(characterName, speech);
    }

    public void OnDialogSkiped(Dialog dialog)
    {
        DialogSkiped?.Invoke(dialog);
        //Hide(dialog);
    }

    public void Hide()
    {
        _dialog.gameObject.SetActive(false);
    }
}
