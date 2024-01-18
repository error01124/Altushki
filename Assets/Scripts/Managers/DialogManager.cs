using System;
using UnityEngine;

public class DialogManager : IService
{
    public event Action<Dialog> DialogSkiped;

    private PrefabInstantiater _prefabInstantiater;
    private PrefabsPaths _prefabsPaths;

    public DialogManager()
    {
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
    }

    public Dialog Show(string characterName, string speech)
    {
        Dialog dialog = _prefabInstantiater.InstantiateUI<Dialog>(_prefabsPaths.Dialog);
        dialog.Init(this, characterName, speech);
        return dialog;
    }

    public void OnDialogSkiped(Dialog dialog)
    {
        DialogSkiped?.Invoke(dialog);
        Hide(dialog);
    }

    public void Hide(Dialog dialog)
    {
        GameObject.Destroy(dialog.gameObject);
    }
}
