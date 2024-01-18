using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiater : IService
{
    private Dictionary<string, GameObject> _cachedPrefabs;
    private Canvas _canvas;

    public PrefabInstantiater(Canvas canvas)
    {
        _canvas = canvas;
        _cachedPrefabs = new Dictionary<string, GameObject>();
    }

    public T InstantiateUI<T>(string path) where T : MonoBehaviour
    { 
        T script = Instantiate<T>(path);
        script.transform.SetParent(_canvas.transform);
        return script;
    }

    public T Instantiate<T>(string path) where T : MonoBehaviour
    {
        GameObject prefab = LoadPrefab(path);
        return InstantiatePrefab<T>(prefab);
    }

    public IEnumerable<T> InstantiateMany<T>(string path, int count) where T : MonoBehaviour
    {
        GameObject prefab = LoadPrefab(path);

        for (int i = 0; i < count; i++)
        {
            yield return InstantiatePrefab<T>(prefab);
        }
    }

    private T InstantiatePrefab<T>(GameObject obj) where T : MonoBehaviour
    {
        obj = GameObject.Instantiate(obj);
        T script = obj.GetComponent<T>();
        return script;
    }

    private GameObject LoadPrefab(string path)
    {
        Debug.Log(nameof(LoadPrefab) + " " + path);

        if (_cachedPrefabs.ContainsKey(path))
        {
            return _cachedPrefabs[path];
        }

        GameObject obj = Resources.Load<GameObject>(path);
        return obj;
    }
}
