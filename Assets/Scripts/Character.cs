using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private List<Emotion> _emotionsList;

    private SpriteRenderer _spriteRenderer;
    private Dictionary<EmotionType, Emotion> _emotionsDictionary;

    private void Start()
    {
        _emotionsDictionary = new Dictionary<EmotionType, Emotion>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < _emotionsList.Count; i++)
        {
            _emotionsDictionary[_emotionsList[i].Type] = _emotionsList[i];
        }

        ChangeEmotion(EmotionType.Calm);
    }

    public void ChangeEmotion(EmotionType type)
    {
        _spriteRenderer.sprite = _emotionsDictionary[type].CharacterSprite;
    }
}
