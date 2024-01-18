using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private WindowManager _windowManager;
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private ChoiceLabelManager _choiceLabelManager;

    private void Start()
    { 
        Init();
        Get<WindowManager>().Show<StartMenuWindow>();
    }

    private void Init()
    {
        ServiceLocator.Init();
        Register(new PrefabsPaths());
        Register(new PrefabInstantiater());
        Register(new ResourceLocator());
        Register(Get<PrefabInstantiater>().Instantiate<Context>(Get<PrefabsPaths>().Context));
        Register(new GameSettings());
        //_saver = new JSONSaver();
        _windowManager.Init();
        Register(_windowManager);
        _dialogManager.Init();
        Register(_dialogManager);
        _choiceLabelManager.Init();
        Register(_choiceLabelManager);
        Register(new Storyline());
        Register(new GameController());
    }

    private T Register<T>(T script) where T : IService
    { 
        return ServiceLocator.Instance.Register(script);
    }

    private T Get<T>() where T : IService
    {
        return ServiceLocator.Instance.Get<T>();
    }
}
