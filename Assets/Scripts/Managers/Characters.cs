using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour, IService
{
    [SerializeField] private Character _leftCharacter;
    [SerializeField] private Character _centerCharacter;
    [SerializeField] private Character _rightCharacter;

    public void Init()
    {
        foreach (var character in GetCharacters())
        {
            character.Init();
        }
    }

    public Character Setup(string imagePath, EnumPosition position)
    {
        Character character = GetCharacterByPosition(position);
        return character.Setup(imagePath);
    }

    public IEnumerable<Character> GetCharacters()
    {
        yield return _leftCharacter;
        yield return _centerCharacter;
        yield return _rightCharacter;
    }

    public Character GetCharacterByPosition(EnumPosition position)
    {
        switch (position)
        {
            case EnumPosition.Left:
                return _leftCharacter;
            case EnumPosition.Center:
                return _centerCharacter;
            case EnumPosition.Right:
                return _rightCharacter;
        }

        return null;
    }
}
