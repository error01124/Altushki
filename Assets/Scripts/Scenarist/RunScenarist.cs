public class RunScenarist : IScenarist
{
    public string GetChoiceName(ChoiceLabel choiceLabel)
    {
        return choiceLabel.Result.Name;
    }
}
