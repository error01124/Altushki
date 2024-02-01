public class RunScenarist : IScenarist
{
    public string GetChoiceName(ChoiceWindow choiceLabel)
    {
        return choiceLabel.Result.Name;
    }
}
