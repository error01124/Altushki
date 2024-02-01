using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(Button))]
public abstract class SceneObject<T> : MonoBehaviour where T : SceneObject<T>
{
    public string Name => _name;

    protected Animator _animator;
    protected EnumAnimation _animation;
    protected string _name;
    protected bool _animationEnded;
    protected Keybinds _keybinds;
    protected bool _enabled;

    public virtual void Init()
    { 
        _animator = GetComponent<Animator>();
        _keybinds = ServiceLocator.Instance.Get<Keybinds>();
    }

    public virtual void Clear()
    {
        _animation = EnumAnimation.None;
        _animationEnded = false;
        _name = string.Empty;
    }

    private void Update()
    {
        if (_enabled)
        {
            if (_keybinds.Interact())
            {
                OnClicked();
            }
        }
    }

    public virtual IEnumerator Show()
    {
        _enabled = true;

        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Show);
        }
    }

    public virtual IEnumerator Hide()
    {
        if (HasAnimation())
        {
            yield return PlayAnimation(EnumAnimationSuffix.Hide);
        }

        Clear();
        gameObject.SetActive(false);
    }

    public T With(EnumAnimation animation)
    {
        _animation = animation;
        return (T)this;
    }

    public bool HasAnimation()
    {
        return _animation != EnumAnimation.None;
    }

    public virtual IEnumerator PlayAnimation(EnumAnimationSuffix animationSuffix)
    {
        _animator.SetTrigger(GetAnimationTrigger(_animation) + animationSuffix.ToString());
        Debug.Log("Animation start");
        Debug.Log("AnimationEnded - " + _animationEnded);
        yield return new WaitUntil(() => _animationEnded);
        Debug.Log("Animation end");
    }

    public void StopAnimation()
    {
        Debug.Log("Stop animation");
        _animator.SetTrigger("Stop");
        _animationEnded = true;
    }

    public void OnAnimationEnded()
    {
        Debug.Log("Animation ended");
        _animationEnded = true;
    }

    protected string GetAnimationTrigger(EnumAnimation animation)
    {
        return animation.ToString();
    }

    protected virtual void OnClicked()
    {
        if (HasAnimation())
        {
            StopAnimation();
        }
    }
}