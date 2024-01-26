using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Background : SceneObject<Background>, IService
{
    private Image _image;
    private Sprite _imageSprite;

    public override void Init()
    {
        base.Init();
        _image = GetComponent<Image>();
    }

    public Background Setup(string imagePath)
    {
        _imageSprite = Resources.Load<Sprite>(imagePath);
        return this;
    }

    public override IEnumerator Show()
    {
        _image.sprite = _imageSprite;

        if (HasAnimation())
        {
            yield return StartCoroutine(PlayAnimation());
        }
    }

    public override IEnumerator Hide()
    {
        return null;
    }
}
