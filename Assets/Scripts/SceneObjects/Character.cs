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
        _imageSprite = Resources.Load<Sprite>(imagePath);
        return this;
    }

    public override IEnumerator Show()
    {
        _image.enabled = true;
        _image.sprite = _imageSprite;

        if (HasAnimation())
        {
            yield return StartCoroutine(PlayAnimation());
        }
    }

    public override IEnumerator Hide()
    {
        _image.enabled = false;
        return null;
    }
}
