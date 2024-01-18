using System.IO;
using UnityEngine;

public class ResourceLocator : IService
{
    public string JoinPaths(params string[] paths)
    {
        string path = paths[0];
        
        for (int i = 1; i < paths.Length; i++)
        {
            path += "/" + paths[i];
        }

        return path;
    }

    public string GetAbsolutePath(string path)
    {
        return JoinPaths(Application.dataPath, path);
    }
}
