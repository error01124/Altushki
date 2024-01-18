using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : IService
{
    private Dictionary<Type, string> _windowsPaths;
    private Window _currentWindow;
    private PrefabInstantiater _prefabInstantiater;
    private PrefabsPaths _prefabsPaths;

    public WindowManager()
    {
        _prefabInstantiater = ServiceLocator.Instance.Get<PrefabInstantiater>();
        _prefabsPaths = ServiceLocator.Instance.Get<PrefabsPaths>();
        _windowsPaths = new Dictionary<Type, string>();

        _windowsPaths[typeof(StartMenuWindow)] = _prefabsPaths.StartMenu;
    }

    public void Show<T>() where T : Window
    {
        T window = _prefabInstantiater.InstantiateUI<T>(_windowsPaths[typeof(T)]);
        _currentWindow = window;
    }

    public void HideCurrent()
    {
        GameObject.Destroy(_currentWindow.gameObject);
    }
}
