using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Background : SceneObject<Background>, IService
{
    private Image _image;

    public override void Init()
    {
        base.Init();
        _image = GetComponent<Image>();
    }

    public override void Clear()
    {
        base.Clear();
        _image.sprite = null;
    }

    public IEnumerator Display(string imagePath)
    {
        Clear();
        Sprite imageSprite = Resources.Load<Sprite>(imagePath);
        _image.sprite = imageSprite;
        _image.enabled = true;
        yield return null;
    }

    public override IEnumerator Show()
    {
        _enabled = true;
        _image.enabled = true;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Show);
        }
    }

    public override IEnumerator Hide()
    {
        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Hide);
        }

        Clear();
        _image.enabled = false;
    }
}
