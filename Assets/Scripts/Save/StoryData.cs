using System.Collections.Generic;

public class StoryData : IService
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
