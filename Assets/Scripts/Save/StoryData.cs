using System;
using System.Collections.Generic;

[Serializable]
public class StoryData
{
    public List<int> SavedSceneObjectsIds => _savedSceneObjectsIds;
    public Dictionary<string, object> SceneObjectsResults => _sceneObjectsResults;

    private List<int> _savedSceneObjectsIds;
    private Dictionary<string, object> _sceneObjectsResults;

    public StoryData()
    {
        _savedSceneObjectsIds = new List<int>();
        _sceneObjectsResults = new Dictionary<string, object>();
    }
}
