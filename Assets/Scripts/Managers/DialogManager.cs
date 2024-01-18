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

    public void Show(string characterName, string speech)
    {
        _dialog.gameObject.SetActive(true);
        _dialog.Setup(characterName, speech);
    }

    public void OnDialogSkiped(Dialog dialog)
    {
        DialogSkiped?.Invoke(dialog);
        Hide(dialog);
    }

    public void Hide(Dialog dialog)
    {
        dialog.gameObject.SetActive(false);
    }
}
