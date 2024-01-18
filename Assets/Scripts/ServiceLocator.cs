using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    public static ServiceLocator Instance => _instance;

    private static ServiceLocator _instance;

    private Dictionary<Type, IService> _services;

    public ServiceLocator() 
    {
        _services = new Dictionary<Type, IService>();
    }

    public static void Init()
    {
        _instance = new ServiceLocator();
    }

    public T Get<T>() where T : IService
    {
        Type type = typeof(T);

        if (!_services.ContainsKey(type))
        {
            Debug.LogError($"There is no such service {type.ToString()}");
        }

        return (T) _services[typeof(T)];
    }

    public T Register<T>(T service) where T : IService
    {
        Type type = typeof(T);

        if (_services.ContainsKey(type))
        {
            Debug.LogError($"Service is already registered {nameof(type)}");
        }
        else
        {
            _services[type] = service;
        }

        return service;
    }

    public void Unregister<T>() where T : IService
    {
        Type type = typeof(T);

        if (!_services.ContainsKey(type))
        {
            Debug.LogError($"There is no such service {nameof(type)}");
            return;
        }

        _services.Remove(type);
    }
}
