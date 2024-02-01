using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character : SceneObject<Character>, IService
{
    private Sprite _imageSprite;
    private Image _image;

    public override void Init()
    {
        base.Init();
        _image = GetComponent<Image>();
    }

    public Character Setup(string imagePath)
    {
        Clear();
        _imageSprite = Resources.Load<Sprite>(imagePath);
        return this;
    }

    public override IEnumerator Show()
    {
        _enabled = true;
        Debug.Log("Character Show");
        _image.sprite = _imageSprite;
        _image.enabled = true;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Show);
        }

        Debug.Log("Character Show Finish");
    }

    public override IEnumerator Hide()
    {
        Clear();
        _image.enabled = false;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Hide); 
        }
    }
}
