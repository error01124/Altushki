using System;
using UnityEngine;

[Serializable]
public class Emotion
{
    public EmotionType Type => _type;
    public Sprite CharacterSprite => _characterSprite;

    [SerializeField] private EmotionType _type;
    [SerializeField] private Sprite _characterSprite;
}
