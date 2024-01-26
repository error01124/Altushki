public class SaveScenarist : IScenarist
{
    private StoryData _storyData;

    public SaveScenarist()
    {
        _storyData = ServiceLocator.Instance.Get<StoryData>();
    }

    public string GetChoiceName(ChoiceLabel choiceLabel)
    {
        return (string) _storyData.GetSceneObjectResult(choiceLabel.Name);
    }
}
