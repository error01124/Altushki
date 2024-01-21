using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour, IService
{
    [SerializeField] private Image _leftCharacterImage;
    [SerializeField] private Image _centerCharacterImage;
    [SerializeField] private Image _rightCharacterImage;

    public void Init()
    {

    }

    public void Show(string path, EnumPosition position)
    {
        Hide(position);
        Image characterImage = GetCharacterImage(position);
        characterImage.enabled = true;
        Sprite characterSprite = Resources.Load<Sprite>(path);
        characterImage.sprite = characterSprite;
        Animator animator = characterImage.gameObject.GetComponent<Animator>();
        animator.SetTrigger("Show");
    }

    public void HideEveryone()
    {
        Hide(EnumPosition.Left); 
        Hide(EnumPosition.Center);
        Hide(EnumPosition.Right);
    }

    public void Hide(EnumPosition position)
    {
        Image characterImage = GetCharacterImage(position);
        characterImage.enabled = false;
    }

    private Image GetCharacterImage(EnumPosition position)
    {
        switch (position)
        {
            case EnumPosition.Left:
                return _leftCharacterImage;
            case EnumPosition.Center:
                return _centerCharacterImage;
            case EnumPosition.Right:
                return _rightCharacterImage;
        }

        return null;
    }
}
