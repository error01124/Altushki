using System.Collections;
using UnityEngine;

public class StartStoryline : Storyline
{
    public StartStoryline(Scenarist scenarist) : base(scenarist)
    {

    }

    public override IEnumerator InitScene()
    {
        yield return Show(_background.Setup("Sprites/Postal1").With(EnumAnimation.Blackout));
        yield return Show(_characters.Setup("Sprites/Characters/Lena/Calm", EnumPosition.Left).With(EnumAnimation.Blackout));
        yield return Show(_dialog.Setup("Лена", "За твои деяния тебя ожидает единственный закономерный исход. Улизнуть не удастся.").With(EnumAnimation.Blackout));
        yield return Hide(_characters.GetCharacter(EnumPosition.Left));
        yield return Hide(_dialog);
        yield return Hide(_background.With(EnumAnimation.Blackout));
        yield return Show(_background.Setup("Sprites/Postal2").With(EnumAnimation.Blackout));
        yield return Show(_characters.Setup("Sprites/Characters/Lena/Calm", EnumPosition.Left).With(EnumAnimation.Blackout));
        yield return Show(_dialog.Setup("Лена", "Прощай").With(EnumAnimation.Blackout));
        yield return Hide(_dialog);
        yield return Hide(_characters.GetCharacter(EnumPosition.Left));
        yield return Hide(_background);

        switch (_scenarist.GetChoiceName(_choiceLabel))
        {
            case "1":
                Debug.Log("1");
                break;
        }
    }
}
