using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Context _context;
    [SerializeField] private WindowManager _windowManager;
    [SerializeField] private DialogWindow _dialog;
    [SerializeField] private ChoiceWindow _choiceWindiw;
    [SerializeField] private Background _background;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private Characters _characters;

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
        Register(new Keybinds());
        //_saver = new JSONSaver();
        _dialog.Init();
        Register(_dialog);
        _choiceWindiw.Init();
        Register(_choiceWindiw);
        _background.Init();
        Register(_background);
        _musicPlayer.Init();
        Register(_musicPlayer);
        _characters.Init();
        Register(_characters);
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
