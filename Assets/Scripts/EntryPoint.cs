using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Context _context;
    [SerializeField] private WindowManager _windowManager;
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private ChoiceLabelManager _choiceLabelManager;

    private void Start()
    { 
        Init();
        _windowManager.Show(_windowManager.StartMenuWindow);
    }

    private void Init()
    {
        ServiceLocator.Init();
        Register(new PrefabsPaths());
        Register(new PrefabInstantiater());
        Register(new ResourceLocator());
        Register(_context);
        Register(new GameSettings());
        //_saver = new JSONSaver();
        _dialogManager.Init();
        Register(_dialogManager);
        _choiceLabelManager.Init();
        Register(_choiceLabelManager);
        Register(new Storyline());
        Register(new GameController());
        _windowManager.Init();
        Register(_windowManager);
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
