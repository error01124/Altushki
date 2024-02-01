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
        Clear();
        _imageSprite = Resources.Load<Sprite>(imagePath);
        return this;
    }

    public override IEnumerator Show()
    {
        _enabled = true;
        _image.sprite = _imageSprite;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Show);
            Debug.Log("Background Show Finish");
            Debug.Log("Background AnimationEnded - " + _animationEnded);
        }
    }

    //remove
    protected override void OnClicked()
    {
        Debug.Log(">>> Backgroudn Enabled = " + _enabled);
        StopAnimation();
    } 
}
