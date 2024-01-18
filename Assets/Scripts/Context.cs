using System;
using UnityEngine;

public class Context : MonoBehaviour, IService
{
    public event Action<float> Updated;

    private void Update()
    {
        Updated?.Invoke(Time.deltaTime);
    }
}
