using System.Collections.Generic;

public class Scenarist
{
    public EnumMode Mode => _mode;

    private SaveData _saveData;
    private EnumMode _mode;

    public Scenarist(SaveData saveData)
    {
        _saveData = saveData;
        SetMode(EnumMode.Runtime);
    }

    public void SetMode(EnumMode mode)
    {
        _mode = mode;
    }

    public string GetChoiceName(ChoiceLabel choiceLabel)
    {
        if (_mode == EnumMode.FromSave)
        {
            return (string) _saveData.StoryData.SceneObjectsResults[choiceLabel.Name];
        }

        _saveData.StoryData.SceneObjectsResults.Add(choiceLabel.Name, choiceLabel.Result.Name);
        return choiceLabel.Result.Name;
    }

    public bool IsSceneObjectSaved(int id)
    {
        List<int> ids = _saveData.StoryData.SavedSceneObjectsIds;
        return ids.Contains(id);
    }

    public enum EnumMode
    {
        FromSave,
        Runtime
    }
}
