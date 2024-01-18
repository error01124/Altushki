using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour, IService
{
    public Window CurrentWindow => _currentWindow; 
    public StartMenuWindow StartMenuWindow => _startMenuWindow;

    [SerializeField] private StartMenuWindow _startMenuWindow;

    private Window _currentWindow;

    public void Init()
    { 
        
    }

    public void Show<T>(T window) where T : Window
    {
        _currentWindow = window;
        _currentWindow.gameObject.SetActive(true);
    }

    public void Hide<T>(T window) where T : Window
    {
        _currentWindow.gameObject.SetActive(false);
    }
}
