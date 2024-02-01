public class SaveScenarist : IScenarist
{
    private StoryData _storyData;

    public SaveScenarist()
    {
        _storyData = ServiceLocator.Instance.Get<StoryData>();
    }

    public string GetChoiceName(ChoiceWindow choiceLabel)
    {
        return (string) _storyData.GetSceneObjectResult(choiceLabel.Name);
    }
}
