using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas; 

    private void Start()
    { 
        Init();
        Get<WindowManager>().Show<StartMenuWindow>();
    }

    private void Init()
    {
        ServiceLocator.Init();
        Register(new PrefabsPaths());
        Register(new PrefabInstantiater(_canvas));
        Register(new ResourceLocator());
        Register(Get<PrefabInstantiater>().Instantiate<Context>(Get<PrefabsPaths>().Context));
        Register(new GameSettings());
        //_saver = new JSONSaver();
        Register(new WindowManager());
        Register(new ChoiceLabelManager());
        Register(new DialogManager());
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
