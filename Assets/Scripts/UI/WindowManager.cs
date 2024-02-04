using UnityEngine;

public class WindowManager : MonoBehaviour, IService
{
    public Window CurrentWindow => _currentWindow; 
    public StartMenuWindow StartMenuWindow => _startMenuWindow;
    public InGameMenuWindow InGameMenuWindow => _inGameMenuWindow;

    [SerializeField] private StartMenuWindow _startMenuWindow;
    [SerializeField] private InGameMenuWindow _inGameMenuWindow;

    private Window _currentWindow;

    public void Init()
    {
        _startMenuWindow.Init(this);
    }

    public bool IsOpen<T>(T window) where T : Window => _currentWindow == window;

    public void Open<T>(T window) where T : Window
    {
        _currentWindow = window;
        _currentWindow.gameObject.SetActive(true);
        _currentWindow.Open();
    }

    public void Close<T>(T window) where T : Window
    {
        _currentWindow.Close();
        _currentWindow.gameObject.SetActive(false);
    }
}
