using System;
using System.Collections.Generic;

[Serializable]
public class StoryData
{
    private Dictionary<string, object> _sceneObjectsResults;

    public StoryData()
    {
        _sceneObjectsResults = new Dictionary<string, object>();
    }

    public object GetSceneObjectResult(string name)
    {
        return _sceneObjectsResults[name];
    }
}
