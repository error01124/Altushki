using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Context _context;
    [SerializeField] private WindowManager _windowManager;
    [SerializeField] private Dialog _dialog;
    [SerializeField] private ChoiceLabel _choiceLabel;
    [SerializeField] private Background _background;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private Characters _characters;

    private void Start()
    { 
        Init();
        _windowManager.Open(_windowManager.StartMenuWindow);
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
        Register(new Saver());
        _dialog.Init();
        Register(_dialog);
        _choiceLabel.Init();
        Register(_choiceLabel);
        _background.Init();
        Register(_background);
        _musicPlayer.Init();
        Register(_musicPlayer);
        _characters.Init();
        Register(_characters);
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
